using EntityHelper;
using HfutIe.ClassStore;
using HfutIe.DataAccess.DbProvider;
using HfutIE.DataAccess;
using HfutIE.Entity;
using HfutIE.Repository;
using KEY_PART_INFOR;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HfutIe.ClassStore
{
    /// <summary>
    /// 修改版20190403by_sy
    /// 修改内容(1-同步基本信息方法抽象，2-工厂模式新增删除实体集合方法(Delete(List<T> entity)),3-获得两个相同实体不同集合的差集方法(ListExcept))
    /// </summary>
    static class SQLiteOperate
    {
        #region 仓储 
        static RepositoryFactory<BASE_USER> BASE_USERRepositoryFactory = new RepositoryFactory<BASE_USER>();//人员基本表
        static RepositoryFactory<CONTROL_ADDRESS> CONTROL_ADDRESSRepositoryFactory = new RepositoryFactory<CONTROL_ADDRESS>();//控制地址基本表
        static RepositoryFactory<STOPPER> StopperRepositoryFactory = new RepositoryFactory<STOPPER>();//停止器基本表
        static RepositoryFactory<STATION> StationRepositoryFactory = new RepositoryFactory<STATION>();//工位基本表
        static RepositoryFactory<P_KEY_PART_INFOR> P_KEY_PART_INFORRepositoryFactory = new RepositoryFactory<P_KEY_PART_INFOR>();//安全件采集过程表
        static RepositoryFactory<DOC_KEY_PART_INFOR> DOC_KEY_PART_INFORRepositoryFactory = new RepositoryFactory<DOC_KEY_PART_INFOR>();//安全件采集档案表
        static RepositoryFactory<P_PRODUCT_SERIAL> P_PRODUCT_SERIALRepositoryFactory = new RepositoryFactory<P_PRODUCT_SERIAL>();//产品流水表
        #endregion

        #region 创建表
        /// <summary>
        /// 创建表
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int CreateTable<T>(T entity)
        {
            try
            {
                RepositoryFactory<BaseEntity> sqliteRepositoryFactory = new RepositoryFactory<BaseEntity>();
                Type type = entity.GetType();
                PropertyInfo[] props = type.GetProperties();
                StringBuilder sb = new StringBuilder();
                bool is_first = true;
                sb.Append("CREATE TABLE ");
                sb.Append(type.Name.ToString());
                sb.Append(" ( ");
                foreach (PropertyInfo prop in props)
                {
                    if (is_first)
                    {
                        sb.Append(prop.Name.ToString() + " " + prop.GetSqliteType().ToString() + " PRIMARY KEY");
                        is_first = false;
                    }
                    else
                        sb.Append(", " + prop.Name.ToString() + " " + prop.GetSqliteType().ToString());
                }
                sb.Append(" );");
                return sqliteRepositoryFactory.Repository(DatabaseType.SQLite).ExecuteBySql(sb);
            }
            catch(Exception ex)
            {
                return -1;
            }
        }
        #endregion

        #region 获得sqlite类型
        /// <summary>
        /// 获得sqlite类型
        /// </summary>
        /// <param name="prop">属性</param>
        /// <returns></returns>
        public static string GetSqliteType(this PropertyInfo prop)
        {
            string type = prop.PropertyType.Name.ToString();
            //string typeadd = "";
            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                //如果为可空类型，获取它的基类型
                type = prop.PropertyType.GetGenericArguments()[0].Name.ToString();
            }
            //else typeadd = " NOT NULL";//为非空类型加上后缀
            switch (type)
            {
                case "String":
                    type = "TEXT";
                    break;
                case "Int32":
                    type = "INT";
                    break;
                case "DateTime":
                    type = "DATETIME";
                    break;
                case "float":
                    type = "REAL";
                    break;
                case "double":
                    type = "REAL";
                    break;
                case "decimal":
                    type = "REAL";
                    break;
                case "string?":
                    type = "TEXT";
                    break;
            }
            return type;//+ typeadd;
        }
        #endregion

        #region 创建本地SQLite数据库，默认在Debug中，修改AppConfig中对应数据库连接语句
        /// <summary>
        /// 创建本地SQLite数据库，默认在Debug中，修改AppConfig中对应数据库连接语句
        /// </summary>
        public static void CreateSQLiteDataBase()
        {
            string strpath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase+ "MyDatabase.db";//获取本地SQLite数据库路径。
            if (!File.Exists(strpath))//如果数据库不存在，则创建本地数据库，并创建或更新SQLite数据库连接语句
            {
                SQLiteConnection.CreateFile("MyDatabase.db");//创建文件名称为MyDatabase.db的SQLite数据库
                string newName = "";
                string newConString = "";
                string newProviderName = "";
                newName = "HfutIEFramework_SQLite";
                newConString += "data source=" + strpath + ";version=3;";//数据源
                newProviderName = "System.Data.SQLite";
                UpdateConnectionStringsConfig(newName, newConString, newProviderName);
            }
        }
        #endregion

        #region 更新数据库连接字符串
        private static void UpdateConnectionStringsConfig(string newName, string newConString, string newProviderName)
        {
            bool isModified = false;
            // 记录该连接串是否已经存在
            // 如果要更改的连接串已经存在
            if (ConfigurationManager.ConnectionStrings[newName] != null)
            {
                isModified = true;
            }
            // 新建一个连接字符串实例
            ConnectionStringSettings mySettings = new ConnectionStringSettings(newName, newConString, newProviderName);
            // 打开可执行的配置文件*.exe.config
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // 如果连接串已存在，首先删除它
            if (isModified)
            {
                config.ConnectionStrings.ConnectionStrings.Remove(newName);
            }
            // 将新的连接串添加到配置文件中.
            config.ConnectionStrings.ConnectionStrings.Add(mySettings);
            // 保存对配置文件所作的更改
            config.Save(ConfigurationSaveMode.Modified);
            // 强制重新载入配置文件的connectionStrings配置节
            ConfigurationManager.RefreshSection("connectionStrings");
        }
        #endregion

        #region 原同步基本方法(已弃用)
        /// <summary>
        /// 同步人员基本信息
        /// </summary>
        public static void SynchronizeUserInfo()
        {
            string checkexidt = "select * from sqlite_master where type = 'table' and name = '"+ typeof(BASE_USER).Name.ToString() + "'";
            DataTable dt = BASE_USERRepositoryFactory.Repository(DatabaseType.SQLite).FindTableBySql(checkexidt);//查询本地数据库中是否存在人员信息表
            if (dt.Rows.Count != 0)//SQLite中已存在人员表，则同步人员信息
            {
                List<BASE_USER> userlist_oracle = BASE_USERRepositoryFactory.Repository().FindList();//服务器人员数据
                List<BASE_USER> userlist_sqlite = BASE_USERRepositoryFactory.Repository(DatabaseType.SQLite).FindList();//本地缓存人员信息
                List<BASE_USER> list_add = userlist_oracle.FindAll(s=> !userlist_sqlite.Select(x=>x.USERID).ToList().Contains(s.USERID)).ToList();//待同步新增人员信息
                List<BASE_USER> list_delete = userlist_sqlite.FindAll(s => !userlist_oracle.Select(x => x.USERID).ToList().Contains(s.USERID)).ToList();//待删除无效人员信息
                foreach (var item_add in list_add)
                {
                    BASE_USERRepositoryFactory.Repository(DatabaseType.SQLite).Insert(item_add);
                }
                foreach (var item_delete in list_delete)
                {
                    BASE_USERRepositoryFactory.Repository(DatabaseType.SQLite).Delete(item_delete);
                }
            }
            else
            {
                CreateTable(new BASE_USER());//创建人员表
                List<BASE_USER> userlist_oracle = BASE_USERRepositoryFactory.Repository().FindList();//服务器人员数据
                BASE_USERRepositoryFactory.Repository(DatabaseType.SQLite).Insert(userlist_oracle);//全部插入本地缓存
            }
        }
        /// <summary>
        /// 同步控制地址信息
        /// </summary>
        public static void SynchronizeControlAddress()
        {
            string checkexidt = "select * from sqlite_master where type = 'table' and name = '" + typeof(CONTROL_ADDRESS).Name.ToString() + "'";
            DataTable dt = CONTROL_ADDRESSRepositoryFactory.Repository(DatabaseType.SQLite).FindTableBySql(checkexidt);//查询本地数据库中是否存在控制地址信息表
            if (dt.Rows.Count != 0)//SQLite中已存在控制地址表，则同步控制地址信息
            {
                List<CONTROL_ADDRESS> addresslist_oracle = CONTROL_ADDRESSRepositoryFactory.Repository().FindList();//服务器控制地址数据
                List<CONTROL_ADDRESS> addresslist_sqlite = CONTROL_ADDRESSRepositoryFactory.Repository(DatabaseType.SQLite).FindList();//本地缓存控制地址信息
                List<CONTROL_ADDRESS> list_add = addresslist_oracle.FindAll(s => !addresslist_sqlite.Select(x => x.CONTROL_ADDRESS_KEY).ToList().Contains(s.CONTROL_ADDRESS_KEY)).ToList();//待同步新增人员信息
                List<CONTROL_ADDRESS> list_delete = addresslist_sqlite.FindAll(s => !addresslist_oracle.Select(x => x.CONTROL_ADDRESS_KEY).ToList().Contains(s.CONTROL_ADDRESS_KEY)).ToList();//待删除无效人员信息
                foreach (var item_add in list_add)
                {
                    CONTROL_ADDRESSRepositoryFactory.Repository(DatabaseType.SQLite).Insert(item_add);
                }
                foreach (var item_delete in list_delete)
                {
                    CONTROL_ADDRESSRepositoryFactory.Repository(DatabaseType.SQLite).Delete(item_delete);
                }
            }
            else
            {
                CreateTable(new CONTROL_ADDRESS());//创建控制地址表
                List<CONTROL_ADDRESS> addresslist_oracle = CONTROL_ADDRESSRepositoryFactory.Repository().FindList();//服务器控制地址数据
                CONTROL_ADDRESSRepositoryFactory.Repository(DatabaseType.SQLite).Insert(addresslist_oracle);//全部插入本地缓存
            }
        }
        /// <summary>
        /// 同步停止器信息
        /// </summary>
        public static void SynchronizeStopper()
        {
            string checkexidt = "select * from sqlite_master where type = 'table' and name = '" + typeof(STOPPER).Name.ToString() + "'";
            DataTable dt = StopperRepositoryFactory.Repository(DatabaseType.SQLite).FindTableBySql(checkexidt);//查询本地数据库中是否存在停止器信息表
            if (dt.Rows.Count != 0)//SQLite中已存在停止器表，则同步停止器信息
            {
                List<STOPPER> stopperlist_oracle = StopperRepositoryFactory.Repository().FindList();//服务器停止器数据
                List<STOPPER> stopperlist_sqlite = StopperRepositoryFactory.Repository(DatabaseType.SQLite).FindList();//本地缓存停止器信息
                List<STOPPER> list_add = stopperlist_oracle.FindAll(s => !stopperlist_sqlite.Select(x => x.STOPPER_KEY).ToList().Contains(s.STOPPER_KEY)).ToList();//待同步新增停止器信息
                List<STOPPER> list_delete = stopperlist_sqlite.FindAll(s => !stopperlist_oracle.Select(x => x.STOPPER_KEY).ToList().Contains(s.STOPPER_KEY)).ToList();//待删除无效停止器信息
                foreach (var item_add in list_add)
                {
                    StopperRepositoryFactory.Repository(DatabaseType.SQLite).Insert(item_add);
                }
                foreach (var item_delete in list_delete)
                {
                    StopperRepositoryFactory.Repository(DatabaseType.SQLite).Delete(item_delete);
                }
            }
            else
            {
                CreateTable(new STOPPER());//创建停止器表
                List<STOPPER> stopperlist_oracle = StopperRepositoryFactory.Repository().FindList();//服务器停止器数据
                StopperRepositoryFactory.Repository(DatabaseType.SQLite).Insert(stopperlist_oracle);//全部插入本地缓存
            }
        }
        /// <summary>
        /// 同步工位信息
        /// </summary>
        public static void SynchronizeStation()
        {
            string checkexidt = "select * from sqlite_master where type = 'table' and name = '" + typeof(STATION).Name.ToString() + "'";
            DataTable dt = StationRepositoryFactory.Repository(DatabaseType.SQLite).FindTableBySql(checkexidt);//查询本地数据库中是否存在工位信息表
            if (dt.Rows.Count != 0)//SQLite中已存在工位表，则同步工位信息
            {
                List<STATION> stationlist_oracle = StationRepositoryFactory.Repository().FindList();//服务器工位数据
                List<STATION> stationlist_sqlite = StationRepositoryFactory.Repository(DatabaseType.SQLite).FindList();//本地缓存工位信息
                List<STATION> list_add = stationlist_oracle.FindAll(s => !stationlist_sqlite.Select(x => x.STATION_KEY).ToList().Contains(s.STATION_KEY)).ToList();//待同步新增工位信息
                List<STATION> list_delete = stationlist_sqlite.FindAll(s => !stationlist_oracle.Select(x => x.STATION_KEY).ToList().Contains(s.STATION_KEY)).ToList();//待删除无效工位信息
                foreach (var item_add in list_add)
                {
                    StationRepositoryFactory.Repository(DatabaseType.SQLite).Insert(item_add);
                }
                foreach (var item_delete in list_delete)
                {
                    StationRepositoryFactory.Repository(DatabaseType.SQLite).Delete(item_delete);
                }
            }
            else
            {
                CreateTable(new STATION());//创建工位表
                List<STATION> stationlist_oracle = StationRepositoryFactory.Repository().FindList();//服务器工位数据
                StationRepositoryFactory.Repository(DatabaseType.SQLite).Insert(stationlist_oracle);//全部插入本地缓存
            }
        }
        #endregion

        #region 同步安全件信息
        /// <summary>
        /// 同步缓存安全件信息
        /// </summary>
        public static void SynchronizeKeyPartInfor()
        {
            Repository<P_KEY_PART_INFOR> tran = new Repository<P_KEY_PART_INFOR>();
            DbTransaction isOpenTrans = tran.BeginTrans();
            int Isok = 0;
            List<P_KEY_PART_INFOR> AddList_P = new List<P_KEY_PART_INFOR>();//待插入总集合(过程表)
            List<DOC_KEY_PART_INFOR> AddList_DOC = new List<DOC_KEY_PART_INFOR>();//待插入总集合(档案表)
            List<P_KEY_PART_INFOR> UpdateList = new List<P_KEY_PART_INFOR>();//待更新总集合(本地缓存已同步数据)
            try
            {
                string checkexidt = "select * from sqlite_master where type = 'table' and name = '" + typeof(P_KEY_PART_INFOR).Name.ToString() + "'";
                DataTable dt = P_KEY_PART_INFORRepositoryFactory.Repository(DatabaseType.SQLite).FindTableBySql(checkexidt);//查询本地数据库中是否存在安全件采集过程表
                if (dt.Rows.Count != 0)//SQLite中已存在安全件采集过程表，则同步安全件绑定信息(待定)，清除一周前已同步信息
                {
                    /*说明*/
                    /*P_KEY_PART_INFOR中RESERVE1(预留字段1)作为本地缓存待同步信息0-未同步；1-已同步*/
                    /*P_KEY_PART_INFOR中RESERVE1(预留字段2)作为区分直接插入数据还是需校验插入数据0-需校验；1-直接插入*/
                    /*两个字段只在本地缓存时使用*/
                    List<P_KEY_PART_INFOR> alllist = P_KEY_PART_INFORRepositoryFactory.Repository(DatabaseType.SQLite).FindList().Where(s=>s.RESERVE1=="0").ToList();//获取全部未同步数据
                    #region 处理未同步直接插入数据
                    List<P_KEY_PART_INFOR> addlist_p = alllist.Where(s => s.RESERVE2 == "1").ToList();//查找未同步直接插入数据
                    foreach(var itemadd_p in addlist_p)//预留字段1，预留字段2置为null
                    {
                        itemadd_p.RESERVE1 = null;
                        itemadd_p.RESERVE2 = null;
                    }
                    AddList_P.AddRange(addlist_p);
                    List<DOC_KEY_PART_INFOR> addlist_doc = EntityHelper.EntityHelper.ListMapper<P_KEY_PART_INFOR, DOC_KEY_PART_INFOR>(addlist_p).ToList();
                    AddList_DOC.AddRange(addlist_doc);
                    foreach (var item_add in addlist_p)
                    {
                        item_add.RESERVE1 = "1";//同步状态修改为已同步
                    }
                    UpdateList.AddRange(addlist_p);
                    #endregion

                    #region 处理未同步待校验信息(本地信息中只有产品出生证、工位信息和物料条码信息),此时计划已下发，计划对应安全件信息已生成
                    List<P_KEY_PART_INFOR> checkist = alllist.Where(s =>s.RESERVE2 == "0").ToList();//查找未同步待校验信息
                    if (checkist.Count != 0)
                    {
                        string StationCode = checkist.FirstOrDefault().STATION_CODE;
                        BasicInfoDto basicinfor = GetData.GetFactoryInforByWccode(StationCode);//获取基本信息(StationCodeList);
                        STATION staion = StationRepositoryFactory.Repository().FindList().Where(s => s.STATION_CODE == StationCode).FirstOrDefault();
                        List<STATION> StationList = StationRepositoryFactory.Repository().FindList().Where(s => s.WORK_CENTER_KEY == staion.WORK_CENTER_KEY).ToList();
                        List<P_KEY_PART_C> key_part_clist = KeyPartGetData.GetKeyPartConfig(StationList.Select(s => s.STATION_CODE).ToList());//该工位对应的所有待采集安全件
                        List<P_PRODUCT_SERIAL> allseriallist = P_PRODUCT_SERIALRepositoryFactory.Repository().FindList();//所有产品出生证流水
                        foreach (var item in checkist)
                        {
                            P_KEY_PART_INFOR p_key_part_infor = new P_KEY_PART_INFOR();
                            P_PRODUCT_SERIAL productserial = allseriallist.Find(s => s.PRODUCT_BORN_CODE == item.PRODUCT_BORN_CODE);
                            if (productserial == null)//若产品出生证未找到相关信息，则直接视为无效数据，更新同步状态
                            {
                                item.RESERVE1 = "1";
                                UpdateList.Add(item);
                                continue;
                            }
                            P_KEY_PART_C key_part_centity = key_part_clist.Find(s => s.MES_PLAN_CODE == productserial.MES_PLAN_CODE && s.PART_CODE == item.PART_BARCODE);
                            if (key_part_centity != null)//采集到的物料条码找到了对应的待采集信息
                            {
                                p_key_part_infor = item;
                                p_key_part_infor = EntityHelper.EntityHelper.EntityMapper(basicinfor, p_key_part_infor);//基本信息补充(公司、工厂、车间、加工中心等)
                                p_key_part_infor.EQUIP_KEY = basicinfor.EQUIPMENT_KEY;//设备主键
                                p_key_part_infor.EQUIP_CODE = basicinfor.EQUIPMENT_CODE;//设备编号
                                p_key_part_infor.EQUIP_NAME = basicinfor.EQUIPMENT_NAME;//设备名称
                                p_key_part_infor.KEY_PART_C_KEY = key_part_centity.KEY_PART_C_KEY;//安全件主键
                                p_key_part_infor.MES_PLAN_KEY = key_part_centity.MES_PLAN_KEY;//计划主键
                                p_key_part_infor.MES_PLAN_CODE = key_part_centity.MES_PLAN_CODE;//计划编号
                                p_key_part_infor.PRODUCT_KEY = key_part_centity.PRODUCT_KEY;//产品主键
                                p_key_part_infor.PRODUCT_CODE= key_part_centity.PRODUCT_CODE;//产品编号
                                p_key_part_infor.PRODUCT_NAME = key_part_centity.PRODUCT_NAME;//产品名称
                                p_key_part_infor.PART_KEY = key_part_centity.PART_KEY;//物料主键
                                p_key_part_infor.PART_CODE = key_part_centity.PART_CODE;//物料编号
                                p_key_part_infor.PART_NAME = key_part_centity.PART_NAME;//物料名称
                                p_key_part_infor.PRODUCT_BATCH_NO = productserial.PART_ABB;//产品简码
                                p_key_part_infor.PRODUCT_MODEL_NO = productserial.PART_MODEL_NO;//产品型号
                                p_key_part_infor.PRODUCT_STRUCT_NO = productserial.PART_STRUCT_NO;//产品结构号
                                p_key_part_infor.PRODUCT_BATCH_NO = productserial.PART_BATCH_NO;//产品批次号
                                p_key_part_infor.PRODUCT_TYPE = productserial.PART_TYPE;//产品类型
                                p_key_part_infor.PRODUCT_SERIAL_NO = productserial.PRODUCT_SERIAL_NO;//产品序列号
                                p_key_part_infor.PLAN_NO = productserial.PLAN_NO;
                                p_key_part_infor.QUANTITY = 1;
                                p_key_part_infor.RESERVE1 = null;
                                p_key_part_infor.RESERVE2 = null;

                                DOC_KEY_PART_INFOR doc_key_part_infor = p_key_part_infor.EntityMapper<P_KEY_PART_INFOR, DOC_KEY_PART_INFOR>();
                                //判断过程表中是否已经存在此条记录
                                List<P_KEY_PART_INFOR> p_key_part_inforlist = P_KEY_PART_INFORRepositoryFactory.Repository().FindList().Where(s => s.MES_PLAN_CODE == productserial.MES_PLAN_CODE && s.PRODUCT_BORN_CODE == item.PRODUCT_BORN_CODE && s.STATION_KEY == item.STATION_KEY && s.PART_KEY == p_key_part_infor.PART_KEY && s.PART_BARCODE == p_key_part_infor.PART_BARCODE).ToList();
                                //判断档案表中是否已经存在此条记录
                                List<DOC_KEY_PART_INFOR> doc_key_part_inforlist = DOC_KEY_PART_INFORRepositoryFactory.Repository().FindList().Where(s => s.MES_PLAN_CODE == productserial.MES_PLAN_CODE && s.PRODUCT_BORN_CODE == item.PRODUCT_BORN_CODE && s.STATION_KEY == item.STATION_KEY && s.PART_KEY == p_key_part_infor.PART_KEY && s.PART_BARCODE == p_key_part_infor.PART_BARCODE).ToList();
                                if (doc_key_part_inforlist.Count > 0) //数据库中有同计划、同产品、同零部件的记录
                                {
                                    doc_key_part_infor.KEY_PART_INFOR_KEY = doc_key_part_inforlist[0].KEY_PART_INFOR_KEY.ToString();//如果存在记录，则将本条记录的key赋值为从数据库中查到的key，从而更新记录
                                    Isok=DOC_KEY_PART_INFORRepositoryFactory.Repository().Update(doc_key_part_infor, isOpenTrans);//更新记录
                                }
                                else//数据库中没有相应记录
                                {
                                    //DOC_KEY_PART_INFORRepositoryFactory.Repository().Insert(doc_key_part_infor, isOpenTrans);//插入记录
                                    AddList_DOC.Add(doc_key_part_infor);
                                }
                                if (p_key_part_inforlist.Count > 0)//数据库中有同计划、同产品、同零部件的记录
                                {
                                    p_key_part_infor.KEY_PART_INFOR_KEY = p_key_part_inforlist[0].KEY_PART_INFOR_KEY.ToString();//如果存在记录，则将本条记录的key赋值为从数据库中查到的key，从而更新记录
                                    Isok = P_KEY_PART_INFORRepositoryFactory.Repository().Update(p_key_part_infor, isOpenTrans);//更新记录
                                }
                                else//数据库中没有相应记录
                                {
                                    //P_KEY_PART_INFORRepositoryFactory.Repository().Insert(p_key_part_infor, isOpenTrans);//插入记录
                                    AddList_P.Add(p_key_part_infor);
                                }
                            }
                            item.RESERVE1 = "1";//该信息已进行过同步处理
                            UpdateList.Add(item);
                        }
                    }
                    #endregion

                    Isok = P_KEY_PART_INFORRepositoryFactory.Repository().Insert(AddList_P, isOpenTrans);//将数据插入服务器数据库(过程数据)
                    Isok = DOC_KEY_PART_INFORRepositoryFactory.Repository().Insert(AddList_DOC, isOpenTrans);//将数据插入服务器数据库(档案数据)
                    if (Isok == 1)
                    {
                        P_KEY_PART_INFORRepositoryFactory.Repository(DatabaseType.SQLite).Update(UpdateList);//更新本地数据库同步状态
                    }

                    #region 清除一周前已同步安全件绑定信息
                    List<P_KEY_PART_INFOR> deletelist= alllist.Where(s => s.RESERVE1 == "1" && s.CREATEDATE<ServerTime.Now.AddDays(-7)).ToList();//查找一周以前已同步数据
                    P_KEY_PART_INFORRepositoryFactory.Repository(DatabaseType.SQLite).Delete(deletelist);//删除该部分信息
                    #endregion
                }
                else
                {
                    CreateTable(new P_KEY_PART_INFOR());//创建安全件采集过程表
                }
                isOpenTrans.Commit();//事务提交
            }
            catch(Exception ex)
            {
                isOpenTrans.Rollback();//事务回滚
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("同步本地安全件数据时出现错误：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        #endregion

        #region 往SQLite数据库同步实体集
        /// <summary>
        ///  往SQLite数据库同步实体集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitylist"></param>
        public static bool SynchronizeList<T>(T entity) where T : new()
        {
            RepositoryFactory<T> T_RepositoryFactory = new RepositoryFactory<T>();//定义泛型仓储
            bool success = false;
            try
            {
                CreateSQLiteDataBase();
                List<T> WebList = T_RepositoryFactory.Repository().FindList();//获取服务器数据
                string checkexidt = "select * from sqlite_master where type = 'table' and name = '" + typeof(T).Name.ToString() + "'";
                DataTable dt = P_KEY_PART_INFORRepositoryFactory.Repository(DatabaseType.SQLite).FindTableBySql(checkexidt);//查询本地数据库中是否存在表
                if (dt.Rows.Count != 0)//本地存在同步表
                {
                    List<T> LocalList = T_RepositoryFactory.Repository(DatabaseType.SQLite).FindList();//获取本地数据
                    List<T> list_insert = ListExcept<T>(WebList, LocalList);//获取服务器新增修改(待插入)数据
                    List<T> list_delete = ListExcept<T>(LocalList, WebList);//获取服务器删除或修改数据
                    int a = T_RepositoryFactory.Repository(DatabaseType.SQLite).Delete(list_delete);//本地删除服务器已删除或修改数据
                    int b= T_RepositoryFactory.Repository(DatabaseType.SQLite).Insert(list_insert);//本地插入服务器新增或修改数据
                    success = true;
                }
                else//本地不存在同步表
                {
                    CreateTable(new T());//创建该表
                    T_RepositoryFactory.Repository(DatabaseType.SQLite).Insert(WebList);//同步服务器所有数据
                    success = true;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return success;
        }
        #endregion

        #region 实现两个实体集的差集
        /// <summary>
        /// 实现两个实体集的差集
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="FirstList"></param>
        /// <param name="SecondList"></param>
        /// <returns></returns>
        public static List<Entity> ListExcept<Entity>(List<Entity> FirstList, List<Entity> SecondList) where Entity : new()
        {
            if (FirstList == null)
            {
                return null;
            }
            List<Entity> list = new List<Entity>();
            Type target_type = typeof(Entity);
            PropertyInfo[] target_props = target_type.GetProperties();
            foreach (Entity firstEntity in FirstList)
            {
                bool is_different = true;//判断第一个集合当前遍历数据是否能与第二个集合任意一条数据匹配上(即是否需要加入差集集合中，true-是，false-否)
                foreach (Entity secondEntity in SecondList)
                {
                    bool matched = true;//来自不同集合的两条数据比较时，相同字段出现数据不一致则跳出循环，遍历第二个集合结束时用于判断第一个集合被比对数据在第二个集合中是否存在完全相同的一条数据，true-找到匹配数据，false-未找到匹配数据
                    foreach (PropertyInfo target_prop in target_props)
                    {
                        if (target_prop.GetValue(firstEntity) != null && target_prop.GetValue(secondEntity) != null)//字段值均不为null时
                        {
                            if (!target_prop.GetValue(firstEntity).Equals(target_prop.GetValue(secondEntity)))//字段值不相等，跳出最小遍历，此时未找到匹配数据
                            {
                                matched = false;
                                break;
                            }
                        }
                        else
                        {
                            if (!(target_prop.GetValue(firstEntity) == null && target_prop.GetValue(secondEntity) == null))//字段值存在null但并不都为null，直接判断为不一致，跳出最小遍历，此时未找到匹配数据
                            {
                                matched = false;
                                break;
                            }
                        }
                    }
                    if (matched)//找到匹配数据
                    {
                        is_different = false;
                        break;//跳出第二个遍历
                    }
                }
                if (is_different)//未找到匹配数据，添加到差集集合中
                {
                    list.Add(firstEntity);
                }
            }
            return list;
        }
        #endregion
    }
}