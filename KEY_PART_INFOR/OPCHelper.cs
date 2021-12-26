using HfutIE.Entity;
using Opc;
using Opc.Da;
using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HfutIe
{
    public static class OPCHelper<aForm> where aForm : IOPC
    {
        #region 全局变量

        public static Opc.Da.Server OpcGroupClassm_server = null;//定义数据存取服务器

        public static Opc.IDiscovery OpcGroupClassm_discovery = new OpcCom.ServerEnumerator();//定义枚举基于COM服务器的接口，用来搜索所有的此类服务器    
        public static Opc.Da.Subscription OpcChangeGroup = null;//定义组对象（订阅者）
        public static Opc.Da.SubscriptionState ChangeGroupClassstate = null;//定义组（订阅者）状态，相当于OPC规范中组的参数 

        public static Opc.Da.Subscription OpcUnChangeGroup = null;//定义组对象（订阅者）
        public static Opc.Da.SubscriptionState UnChangeGroupClassstate = null;//定义组（订阅者）状态，相当于OPC规范中组的参数
        public static string OpcServerName;//OPC服务器名称
        public static Dictionary<string, CONTROL_ADDRESS> dicAddress = new Dictionary<string, CONTROL_ADDRESS>();//地址字典(Key：AddressName；值：地址)
        public static Dictionary<string, CONTROL_ADDRESS> dicChgAddr = new Dictionary<string, CONTROL_ADDRESS>();//地址字典(Key：AddressName；值：地址)
        public static Dictionary<string, CONTROL_ADDRESS> dicUnChgAddr = new Dictionary<string, CONTROL_ADDRESS>();//地址字典(Key：AddressName；值：地址)
        public static List<CONTROL_ADDRESS> addrList = new List<CONTROL_ADDRESS>();
        public static aForm aform;
        #endregion

       static OPCItems MyItemsMA;
        static OPCItem[] MyItemMA;
        public static OPCGroup MyGroupMA;

        public static void InitAddr(List<CONTROL_ADDRESS> addrList1)
        {
            try
            {

            addrList = addrList1;
            foreach (var addr in addrList1)
            {
                dicAddress.Add(addr.ADDRESS_PATH, addr);

                if (addr.RESERVE01 == "1")
                {
                    dicChgAddr.Add(addr.ADDRESS_PATH, addr);
                }
                else
                {
                    dicUnChgAddr.Add(addr.ADDRESS_PATH, addr);
                }
            }

            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog(ex.Message);
            }
        }

        /// <summary>
        /// OPC连接
        /// </summary>
        /// <param name="a"></param>
        /// <param name="remoteIP"></param>
        /// <param name="addrList"></param>
        public static void OPC_Connect(aForm a, string remoteIP, List<CONTROL_ADDRESS> addrList)
        {
            try
            {
                InitAddr(addrList);
                #region 连接OPCServer
                //if (!OpcGroupClassm_server.IsConnected) { return; }
                aform = a;
                OpcServerName = remoteIP + ".KEPware.KEPServerEx.V6";

                //remoteIP = "127.0.0.1";
                //OpcServerName = "KEPware.KEPServerEx.V6";

                Opc.Server[] servers = OpcGroupClassm_discovery.GetAvailableServers(Specification.COM_DA_20, remoteIP, null);//查询服务器
                if (servers != null)
                {
                    foreach (Opc.Da.Server server in servers)
                    {
                        //server即为需要连接的OPC数据存取服务器
                        if (String.Compare(server.Name, OpcServerName, true) == 0)//true表示忽略大小写
                        {
                            OpcGroupClassm_server = server;//建立连接
                            break;
                        }
                    }
                }
                if (OpcGroupClassm_server != null) OpcGroupClassm_server.Connect();//非空连接服务器
                #endregion

                #region UnChangeGroup
                UnChangeGroupClassstate = new Opc.Da.SubscriptionState();//组（订阅者）状态，相当于OPC规范中组的参数
                UnChangeGroupClassstate.Name = "DataUnChangeGroup";//组名
                UnChangeGroupClassstate.ServerHandle = null;//服务器给该组分配的句柄。
                UnChangeGroupClassstate.ClientHandle = Guid.NewGuid().ToString();//客户端给该组分配的句柄
                UnChangeGroupClassstate.Active = true;//激活该组
                UnChangeGroupClassstate.UpdateRate = 100;//刷新频率为1秒
                UnChangeGroupClassstate.Deadband = 0;// 死区值，设为0时，服务器端该组内任何数据变化都通知组
                UnChangeGroupClassstate.Locale = null;//不设置地区值
                OpcUnChangeGroup = (Opc.Da.Subscription)OpcGroupClassm_server.CreateSubscription(UnChangeGroupClassstate);//添加组                                                                                                   //添加监控地址
                try
                {
                    List<Item> items = new List<Item>();
                    foreach (var addr in dicUnChgAddr)
                    {
                        var item = new Item()
                        {
                            ClientHandle = Guid.NewGuid().ToString(),//客户端给该数据项分配的句柄。
                            ItemName = addr.Key //该数据项在服务器中的名字
                        };
                        items.Add(item);
                    }
                    OpcUnChangeGroup.AddItems(items.ToArray());
                }
                catch (Exception ex)
                {
                    Log.GetInstance.WriteLog("初始化监控地址失败：" + ex.Message);
                }
                #endregion

                #region OpcChangeGroup
                ChangeGroupClassstate = new Opc.Da.SubscriptionState();//组（订阅者）状态，相当于OPC规范中组的参数
                ChangeGroupClassstate.Name = "DataChangeGroup";//组名
                ChangeGroupClassstate.ServerHandle = null;//服务器给该组分配的句柄。
                ChangeGroupClassstate.ClientHandle = Guid.NewGuid().ToString();//客户端给该组分配的句柄
                ChangeGroupClassstate.Active = true;//激活该组
                ChangeGroupClassstate.UpdateRate = 100;//刷新频率为1秒
                ChangeGroupClassstate.Deadband = 0;// 死区值，设为0时，服务器端该组内任何数据变化都通知组
                ChangeGroupClassstate.Locale = null;//不设置地区值
                OpcChangeGroup = (Opc.Da.Subscription)OpcGroupClassm_server.CreateSubscription(ChangeGroupClassstate);//添加组
                //添加监控地址
                try
                {
                    List<Item> items = new List<Item>();
                    foreach (var addr in addrList)
                    //foreach (var addr in dicChgAddr)
                    {
                        var item = new Item()
                        {
                            ClientHandle = Guid.NewGuid().ToString(),//客户端给该数据项分配的句柄。
                            ItemName = addr.ADDRESS_PATH //该数据项在服务器中的名字
                            //ItemName = addr.Key
                        };
                        items.Add(item);
                    }
                    if (items.Count > 0)
                    {
                        OpcChangeGroup.AddItems(items.ToArray());
                    }
                }
                catch (Exception ex)
                {
                    Log.GetInstance.WriteLog("初始化监控地址失败：" + ex.Message);

                }
                OpcChangeGroup.DataChanged += new DataChangedEventHandler(OnDataChange);
                #endregion
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog("初始化监控地址失败：" + ex.Message);
            }
        }

        private static void OnDataChange(object subscriptionHandle, object requestHandle, ItemValueResult[] values)
        {
            try
            {
                foreach (var item in values)
                {

                    if (dicAddress.Keys.Contains(item.ItemName))
                    {
                        if (item.Quality.GetCode() == 192)
                        {
                            aform.DataChange(item);
                        }              
                    }
                }
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog(ex.Message);
            }

        }

        /// <summary>
        /// 断开OPC连接
        /// </summary>
        public static void OPC_Disconnect()
        {
            if (OpcGroupClassm_server != null && OpcGroupClassm_server.IsConnected)
            {
                SubscriptionCollection SubscriptionGroups = OpcGroupClassm_server.Subscriptions;
                foreach (Subscription OpcGroupClasssubscription in SubscriptionGroups)
                {
                    //取消回调事件
                    OpcGroupClasssubscription.DataChanged -= new Opc.Da.DataChangedEventHandler(OnDataChange);
                    //移除组内item
                    OpcGroupClasssubscription.RemoveItems(OpcGroupClasssubscription.Items);
                    //结束：释放各资源
                    OpcGroupClassm_server.CancelSubscription(OpcGroupClasssubscription);//m_server前文已说明，通知服务器要求删除组。
                    OpcGroupClasssubscription.Dispose();//强制.NET资源回收站回收该subscription的所有资源。
                }
                OpcGroupClassm_server.Disconnect();//断开服务器连接
            }
        }

        /// <summary>
        /// 根据OPCItem的名称，获取其所属的OPCGroup
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public static Subscription getOpcGroupByItemName(string itemName)
        {
            if (OpcGroupClassm_server != null && OpcGroupClassm_server.IsConnected)
            {
                var SubscriptionGroups = OpcGroupClassm_server.Subscriptions;
                foreach (Subscription OpcGroup in SubscriptionGroups)
                {
                    foreach (Item item in OpcGroup.Items)
                    {
                        if (item.ItemName == itemName)
                        {
                            return OpcGroup;
                        }
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// 获取单个OPCitem的连接状态是否good
        /// </summary>
        /// <param name="OpcItems"></param>
        /// <returns></returns>
        public static int getOpcItemQuality(string itemName)
        {
            try
            {
                //获取Group Subscription，多个Item必须属于同一个Group
                Subscription OpcGroupClasssubscription = getOpcGroupByItemName(itemName);
                if (OpcGroupClasssubscription == null) return 0;

                Item[] item = new Item[1];
                for (int j = 0; j < OpcGroupClasssubscription.Items.Length; j++)
                {
                    if (itemName == OpcGroupClasssubscription.Items[j].ItemName)
                    {
                        item[0] = OpcGroupClasssubscription.Items[j];
                        break;
                    }
                }
                ItemValueResult[] values = OpcGroupClasssubscription.Read(item);
                return values[0].Quality.GetCode();
            }
            catch
            {
                return 0;
            }
        }


        /// <summary>
        /// 根据ItemName找到OPC中Item项
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public static Item FindOpcItem(string itemName)
        {
            Item opcItem = null;
            var group = getOpcGroupByItemName(itemName);
            foreach (var item in group.Items)
            {
                if (itemName == item.ItemName)
                {
                    opcItem = item;
                    break;
                }
            }
            return opcItem;
        }

        /// <summary>
        /// 根据ItemName数组找到OPC中Item项的数组
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public static List<Item> FindOpcItems(List<string> itemNames)
        {
           
                List<Item> opcItems = new List<Item>();
                foreach (var itemName in itemNames)
                {
                    try
                    {
                        var group = getOpcGroupByItemName(itemName);
                        foreach (var item in group.Items)
                        {
                            if (itemName == item.ItemName)
                            {
                                opcItems.Add(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    Log.GetInstance.WriteLog("FindOpcItems：" + ex.Message);
                    }
                   
                }
                return opcItems;
           
           
        }



        /// <summary>
        /// OPC同步写一个数据
        /// </summary>
        /// <param name="opcitem">地址Name</param>
        /// <param name="WriteVaule">待写的地址Value</param>
        /// <returns>是否写成功</returns>
        public static bool SynWriteOpcItem(string itemName, string writeValue)
        {
            try
            {
                ItemValue[] itemvalues = new ItemValue[1];
                var group = getOpcGroupByItemName(itemName);
                foreach (var item in group.Items)
                {
                    if (itemName == item.ItemName)
                    {
                        itemvalues[0] = new ItemValue(item);
                        itemvalues[0].Value = writeValue;
                        break;
                    }
                }
                group.Write(itemvalues);
                return true;
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog("同步写一个数据：" + ex.Message);


                //ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                //log.Fatal("写心跳信号失败！", ex);
                return false;
            }
        }

        /// <summary>
        /// OPC同步写多个数据,要求所有的Item必须在同一个组内
        /// </summary>
        /// <param name="writeItems">需要些数据的地址和字符串值对应的字典（Key：地址Name；Value：待写的地址Value）</param>
        /// <returns>是否写成功</returns>
        public static bool SynWriteOpcItems(Dictionary<string, string> dicItems)
        {
            try
            {

               
                var itemValues = new List<ItemValue>();
                var group = getOpcGroupByItemName(dicItems.ElementAt(0).Key);
                foreach (var writeItem in dicItems)
                {
                    foreach (var item in group.Items)
                    {
                        if (writeItem.Key == item.ItemName)
                        {
                            var itemValue = new ItemValue(item);
                            itemValue.Value = writeItem.Value;
                            itemValues.Add(itemValue);
                            break;
                        }
                    }
                }
                                      
                group.Write(itemValues.ToArray());               
                return true;
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog("同步写多个数据：" + ex.Message);
                //ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                //log.Fatal("OPC写数据失败！", ex);
                return false;
            }
        }
        /// <summary>
        /// OPC同步读一个数据
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public static string SynReadOpcItem(string itemName)
        {
            try
            {
                var item = FindOpcItem(itemName);
                Item[] readItems = { item };
                var group = getOpcGroupByItemName(itemName);
                ItemValueResult[] itemValues = group.Read(readItems);
                ItemValueResult itemValue = itemValues[0];
                if (itemValue.Quality == Quality.Bad)
                    return null;
                else
                    return itemValue.Value.ToString();
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog("同步读一个数据：" + ex.Message);
                return null;

            }
        }


        /// <summary>
        /// OPC同步读多个数据，要求所有的Item必须在同一个组内
        /// </summary>
        /// <param name="itemNames"></param>
        /// <returns></returns>
        public static Dictionary<string, string> SynReadOpcItems(List<string> itemNames)
        {
            try
            {
                var itemValues = new Dictionary<string, string>();

                var items = FindOpcItems(itemNames);
                Item[] readItems = items.ToArray();

                var group = getOpcGroupByItemName(itemNames[0]);
                ItemValueResult[] itemValueResults = group.Read(readItems);

                foreach (var result in itemValueResults)
                {
                    itemValues.Add(result.ItemName, result.Value.ToString());
                }

                return itemValues;
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog("同步读多个数据：" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// OPC同步读多个数据，要求所有的Item必须在同一个组内
        /// </summary>
        /// <param name="itemNames"></param>
        /// <returns></returns>
        public static Dictionary<string, string> SynReadOpcItems_Byte(List<string> itemNames)
        {
            try
            {
                var itemValues = new Dictionary<string, string>();

                var items = FindOpcItems(itemNames);
                Item[] readItems = items.ToArray();

                var group = getOpcGroupByItemName(itemNames[0]);
                ItemValueResult[] itemValueResults = group.Read(readItems);

                foreach (var result in itemValueResults)
                {
                    if (result.Value == null)
                    {
                        continue;
                    }
                    #region 将读取到的byte array转化为字符串
                    byte[] replyDataBytes = (byte[])result.Value;
                    string  result_string = "";
                    for (int j = 0; j < replyDataBytes.Length; j++)
                    {
                        result_string += (char)replyDataBytes[j];
                    }
                    itemValues.Add(result.ItemName, result_string);
                    #endregion
                }

                return itemValues;
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog("同步读多个数据：" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// OPC同步写多个数据,要求所有的Item必须在同一个组内
        /// </summary>
        /// <param name="writeItems">需要些数据的地址和字符串值对应的字典（Key：地址Name；Value：待写的地址Value）</param>
        /// <returns>是否写成功</returns>
        public static bool SynWriteOpcItems_Byte(Dictionary<string, string> dicItems)
        {
            try
            {
                var itemValues = new List<ItemValue>();
                var group = getOpcGroupByItemName(dicItems.ElementAt(0).Key);
                foreach (var writeItem in dicItems)
                {
                    foreach (var item in group.Items)
                    {
                        if (writeItem.Key == item.ItemName)
                        {
                            var itemValue = new ItemValue(item);
                            Encoding encoding = Encoding.UTF8;
                            Log.GetInstance.WriteLog("PLC写入" + writeItem.Key + "/" + writeItem.Value);
                            byte[] newByteArray = encoding.GetBytes(writeItem.Value);//将字符串转换成byte数组
                            itemValue.Value = newByteArray;
                            itemValues.Add(itemValue);
                            break;
                        }
                    }
                }

                group.Write(itemValues.ToArray());
                return true;
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog("同步写多个数据：" + ex.Message);
                //ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                //log.Fatal("OPC写数据失败！", ex);
                return false;
            }
        }
        public static List<string> SynReadOpcItems2(List<string> itemNames)//同步读多个数据生成动态数组
        {
            try
            {
                List<string> itemValues = new List<string>();
                var items = FindOpcItems(itemNames);
                Item[] readItems = items.ToArray();

                var group = getOpcGroupByItemName(itemNames[0]);
                ItemValueResult[] itemValueResults = group.Read(readItems);

                foreach (var result in itemValueResults)
                {
                    itemValues.Add(result.Value.ToString());
                }

                return itemValues;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
    public enum AddressCategory
    {
        order_code,//订单号
        pro_born_code,//产品出生证
        pro_modle_no,//产品型号
        pro_ID,//物料代号
        wc_isOK,
        pro_isOK,
        isProcess,
        is_hot_test_isOK,
        is_hot_test,
        hot_device_no,
        pro_isProcess,
        credit_card,
        request_plan,
        comm_isOK,
        online_success,
        online_fail,
        plan_send_success,
        allet_arrive,
        equip_startWork,
        equip_endWork,
        work_complete,
        read_barCode,
        read_checkData,
        pallet_leave,
        quality_check = 25,
        queue_update,
        assemble_offline,
        assemble_offline_feedbac,
        storage,
        readRFID_success,
        readRFID_fail,
        writeRFID_success,
        writeRFID_fail,
        repair_online,
        repair_offline,
        light,
        unlight,
        material_andon,
        feed_material_andon,
        material_consume,
        quality_andon,
        feed_quality_andon,
        hot_test_online,
        hot_test_offline,
        light_data,
        is_leaveOK,
        isHavePro,
        read_crankshaftInfor,
        read_pistonRodInfor,
        read_cylinderHeadInfor,
        barcode,
        barcode_isOK,
        hand_model,
        auto_model,
        quip_working,
        equip_idle,
        equip_breakdown,
        mes_comm_isOK,
        equip_start,
        eqm_alarm,
        op_no,
        data,
        engine_block,
        cylinder_head,
        crankshaft,
        piston,
        connecting_rod,
        wait_material_time,//进料等待时间
        work_time,//加工时间
        TrayOutTime,//托盘流出时间
        release_time,//放行等待时间
        other_andon,
        feed_other_andon,
        coating_test_online,
        coating_test_offline,
        coating_program_no,
        mark_success,
        mark_fail,
        read_barCode_feedback,
        workblack_code,
        request_plan_feedback,
        barcode1,
        barcode1_isOK,
        barcode2,
        barcode2_isOK,
        barcode3,
        barcode3_isOK,
        barcode4,
        barcode4_isOK,
        barcode5,
        barcode5_isOK,
        barcode6,
        barcode6_isOK,
        barcode7,
        barcode7_isOK1,
        barcode8,
        barcode8_isOK2,
        barcode9_isOK3,
        wait_time,//进料时间
    }
}
