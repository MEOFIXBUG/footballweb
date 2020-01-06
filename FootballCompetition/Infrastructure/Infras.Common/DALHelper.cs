using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infras.Common
{
    public static class DALHelpers
    {
        internal static string defaulConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"] == null ? "" : ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;



        /// <summary>
        /// Author:    
        /// Get connection string by db type
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static string GetConnectionString(DBName dbName)
        {
            if (dbName == DBName.Default)
            {
                return defaulConnectionString;
            }
            return defaulConnectionString;
        }

        //public static IEnumerable<T> Query<T>(string sql, DynamicParameters param = null, DBName dbName = DBName.Default)
        //{
        //    using (SqlConnection conn = new SqlConnection(GetConnectionString(dbName)))
        //    {
        //        conn.Open();
        //        if (param == null)
        //        {
        //            param = new DynamicParameters();
        //            sql = sql + defaultParam;
        //        }
        //        else
        //        {
        //            sql = sql.Replace(" ", defaultParam + ",");
        //        }
        //        return conn.Query<T>(sql, param);
        //    }
        //}


        public static int Execute(string sql, DynamicParameters param = null, DBName dbName = DBName.Default)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString(dbName)))
            {
                conn.Open();
                if (param == null)
                {
                    param = new DynamicParameters();
                }

                return conn.Execute(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// 
        /// Thực thi lệnh query insert, update, delete
        /// </summary>
        /// <param name="StoredName"></param> chỉ đưa vào tên stored
        /// <param name="userInfo"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ExecuteByStored(string StoredName, DynamicParameters param = null, DBName dbType = DBName.Default)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString(dbType)))
            {
                conn.Open();
                if (param == null)
                {
                    param = new DynamicParameters();
                }
                return conn.Execute(StoredName, param, commandType: CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// Haott
        /// Thực thi lệnh query chỉ trả về một cột và 1 dòng.
        /// </summary>
        /// <param name="StoredName"></param> chỉ đưa vào tên stored
        /// <param name="userInfo"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object ExecuteScalarByStore(string StoredName, DynamicParameters param = null, DBName dbName = DBName.Default)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString(dbName)))
            {
                conn.Open();
                if (param == null)
                {
                    param = new DynamicParameters();
                }
                return conn.ExecuteScalar(StoredName, param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Sử dụng lấy dữ liệu dạng danh sách
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="userInfo"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IEnumerable<T> QueryByStored<T>(string StoredName, DynamicParameters param = null, DBName dbName = DBName.Default)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString(dbName)))
            {
                conn.Open();
                if (param == null)
                {
                    param = new DynamicParameters();
                }
                List<string> para = new List<string>();
                para.AddRange(param.ParameterNames);
                StoredName = StoredName.Trim() + " ";
                for (int i = 0; i < para.Count; i++)
                {
                    if (i > 0) StoredName += ",";
                    StoredName += "@" + para[i];
                }
                return conn.Query<T>(StoredName, param);
            }
        }
        public static IEnumerable<T> Query<T>(string sql, DynamicParameters param = null, DBName dbName = DBName.Default)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString(dbName)))
            {
                conn.Open();
                if (param == null)
                {
                    param = new DynamicParameters();
                }
                List<string> para = new List<string>();
                para.AddRange(param.ParameterNames);
                sql = sql.Trim() + " ";
                for (int i = 0; i < para.Count; i++)
                {
                    if (i > 0) sql += ",";
                    sql += "@" + para[i];
                }
                return conn.Query<T>(sql, param);
            }
        }

    }
}
