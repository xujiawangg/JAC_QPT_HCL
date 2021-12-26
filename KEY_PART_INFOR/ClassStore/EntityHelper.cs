using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HfutIe 
{
    class EntityHelper<TEntity> where TEntity :new()
    {
        #region 将datatable转换成实体对象
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static TEntity GetEntity(DataTable dt)
        {
            TEntity entity = new TEntity();
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (PropertyInfo prop in props)
                    {
                        if (dt.Columns.Contains(prop.Name) && !(dt.Rows[0][prop.Name] is DBNull))
                        {
                            prop.SetValue(entity, dt.Rows[0][prop.Name] );
                        }
                    }
                }
            }

            return entity;
        }
        #region datatable列数小于entity属性数，即通过datatable可生成entity部分数据

        /// <summary>
        /// 可用于：多个datatable分别向同一个entity赋值，从而得到整个entity数据
        /// </summary>
        /// <param name="dt">从dt中获取数据，赋值给entity，entity得到部分数据</param>
        /// <returns></returns>
        public static TEntity GetPartEntity(TEntity entity, DataTable dt)
        {
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (PropertyInfo prop in props)
                    {
                        if (dt.Columns.Contains(prop.Name) && !(dt.Rows[0][prop.Name] is DBNull))
                        {
                            prop.SetValue(entity, dt.Rows[0][prop.Name] );
                        }
                    }
                }
            }

            return entity;
        }
        /// <summary>
        ///  可用于：多个datarow分别向同一个entity赋值，从而得到整个entity数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dr">从dt中获取数据，赋值给entity，entity得到部分数据</param>
        /// <returns></returns>
        public static TEntity GetPartEntity(TEntity entity, DataRow dr)
        {
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            if (dr != null)
            {
                foreach (PropertyInfo prop in props)
                {
                    if (dr.Table.Columns.Contains(prop.Name) && !(dr[prop.Name] is DBNull))
                    {
                        prop.SetValue(entity, dr[prop.Name] );
                    }
                }
            }

            return entity;
        }
        /// <summary>
        /// 一个datatable向entity赋值，生成的entity数据可能不完整
        /// </summary>
        /// <param name="dt">数据来源</param>
        /// <returns></returns>
        public static TEntity GetPartEntity(DataTable dt)
        {
            TEntity entity = new TEntity();
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (PropertyInfo prop in props)
                    {
                        if (dt.Columns.Contains(prop.Name) && !(dt.Rows[0][prop.Name] is DBNull))
                        {
                            prop.SetValue(entity, dt.Rows[0][prop.Name] );
                        }
                    }
                }
            }
            return entity;
        }
        /// <summary>
        /// 一个datarow向entity赋值，生成的entity数据可能不完整
        /// </summary>
        /// <param name="dr">数据来源</param>
        /// <returns></returns>
        public static TEntity GetPartEntity(DataRow dr)
        {
            TEntity entity = new TEntity();
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            if (dr != null)
            {
                foreach (PropertyInfo prop in props)
                {
                    if (dr.Table.Columns.Contains(prop.Name) && !(dr[prop.Name] is DBNull))
                    {
                        prop.SetValue(entity, dr[prop.Name] );
                    }
                }
            }
            return entity;
        }
        #endregion
        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<TEntity> GetEntityList(DataTable dt)
        {
            List<TEntity> entitys = new List<TEntity>();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TEntity entity = new TEntity();
                        Type type = entity.GetType();
                        PropertyInfo[] props = type.GetProperties();
                        foreach (PropertyInfo prop in props)
                        {
                            if (dt.Columns.Contains(prop.Name) && !(dt.Rows[i][prop.Name] is DBNull))
                            {
                                prop.SetValue(entity, dt.Rows[i][prop.Name] );
                            }
                        }
                        entitys.Add(entity);
                    }
                }
            }
            return entitys;
        }
        #endregion

        /// <summary>
        ///  将对象S的属性值赋给Tentity中相同的属性
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static TEntity Mapper<S>(S s)
        {
            TEntity d = Activator.CreateInstance<TEntity>();
            try
            {
                var sType = s.GetType();
                var dType = typeof(TEntity);
                foreach (PropertyInfo sP in sType.GetProperties())
                {
                    foreach (PropertyInfo dP in dType.GetProperties())
                    {
                        if (dP.Name == sP.Name)
                        {
                            dP.SetValue(d, sP.GetValue(s));
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return d;
        }
        public static TEntity Mapper<S>(S s, TEntity d)
        {
            try
            {
                var sType = s.GetType();
                var dType = d.GetType();
                foreach (PropertyInfo sP in sType.GetProperties())
                {
                    foreach (PropertyInfo dP in dType.GetProperties())
                    {
                        if (dP.Name == sP.Name)
                        {
                            dP.SetValue(d, sP.GetValue(s));
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return d;
        }
        /// <summary>
        /// 讲datatable 中的值赋值给对象对应的属性中
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TEntity Mapper(DataTable dt, TEntity entity)
        {
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (PropertyInfo prop in props)
                    {
                        bool b = dt.Columns.Contains(prop.Name);
                        if (b)
                        {
                            if (!(dt.Rows[0][prop.Name] is DBNull))
                            {
                                prop.SetValue(entity, dt.Rows[0][prop.Name]);
                            }
                        }

                    }
                }
            }
            return entity;
        }
    }
}
