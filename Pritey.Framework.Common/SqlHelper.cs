using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Pritey.Framework.Common
{
    public class SqlHelper
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        /// <summary>
        /// Execute Procedure with Message and Value
        /// 12/8/2017 pritey
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="ParamValue"></param>
        /// <param name="ReturnTypeOutVal"></param>
        /// <returns></returns>
        public MEMBERS.SQLReturnMessageNValue ExecuteProcWithMessageNValue(string ProcedureName, object[,] ParamValue, int ReturnTypeOutVal)
        {
            MEMBERS.SQLReturnMessageNValue res = new MEMBERS.SQLReturnMessageNValue();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = ProcedureName;
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), (ParamValue[i, 1] == null ? null : (ParamValue[i, 1].ToString())));
                }
                cmd.Parameters.AddRange(param);
                cmd.Parameters.Add("OUTVAL", (ReturnTypeOutVal == 1 ? SqlDbType.UniqueIdentifier : SqlDbType.Int)).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("OUTMSG", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;
                if (con.State != ConnectionState.Open) { con.Open(); }
                cmd.ExecuteNonQuery();
                con.Close();
                res.Outval = cmd.Parameters["OUTVAL"].Value;
                res.Outmsg = cmd.Parameters["OUTMSG"].Value.ToString();
            }
            catch (Exception ex)
            {
                res.Outmsg = ex.Message;
                res.Outval = 0;
            }
            return res;
        }
       
        /// <summary>
        ///  Execute Procedure With Value
        ///  12/8/2017 pritey
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="ParamValue"></param>
        /// <param name="ReturnTypeOutVal"></param>
        /// <returns></returns>
        public MEMBERS.SQLReturnValue ExecuteProcedureWithValue(string procedureName, object[,] ParamValue, int ReturnTypeOutVal)
        {
            MEMBERS.SQLReturnValue res = new MEMBERS.SQLReturnValue();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = procedureName;
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), (ParamValue[i, 1] == null ? null : (ParamValue[i, 1].ToString() == "null" ? null : ParamValue[i, 1].ToString())));

                }
                cmd.Parameters.AddRange(param);
                cmd.Parameters.Add("OUTVAL", (ReturnTypeOutVal == 1 ? SqlDbType.UniqueIdentifier : ReturnTypeOutVal == 2 ? SqlDbType.Int : SqlDbType.NVarChar), -1).Direction = ParameterDirection.Output;
                if (con.State != ConnectionState.Open) { con.Open(); }
                cmd.ExecuteNonQuery();
                con.Close();


                res.Outval = cmd.Parameters["OUTVAL"].Value;
            }
            catch (Exception ex)
            {
                res.Outval = ex.Message;
            }
            return res;
        }

        /// <summary>
        /// Execute Procedure With Datatable
        /// 12/8/2017 pritey
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="dtData"></param>
        /// <param name="TableParamName"></param>
        /// <param name="ReturnTypeOutVal"></param>
        /// <returns></returns>
        public MEMBERS.SQLReturnValue ExecuteProcedureWithDatatable(string ProcedureName, DataTable dtData, string TableParamName, int ReturnTypeOutVal)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = ProcedureName;
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                if (dtData != null)
                {
                    SqlParameter ParamTb = new SqlParameter("@" + TableParamName, dtData);
                    ParamTb.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(ParamTb);
                }
                cmd.Parameters.Add("OUTVAL", (ReturnTypeOutVal == 1 ? SqlDbType.UniqueIdentifier : ReturnTypeOutVal == 2 ? SqlDbType.Int : SqlDbType.NVarChar), -1).Direction = ParameterDirection.Output;
                if (con.State != ConnectionState.Open) { con.Open(); }
                cmd.ExecuteNonQuery();
                con.Close();
                MEMBERS.SQLReturnValue res = new MEMBERS.SQLReturnValue();
                res.Outval = cmd.Parameters["OUTVAL"].Value;
                return res;
            }
            catch (Exception ee)
            {
                MEMBERS.SQLReturnValue res = new MEMBERS.SQLReturnValue();
                res.Outval = ee.Message;
                return res;
            }
        }
                                                
        /// <summary>
        /// Execute procedure with parameters
        /// 12/8/2017 pritey
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="ParamValue"></param>
        /// <param name="dtExamAnswer"></param>
        /// <param name="TableParamName"></param>
        /// <param name="ReturnTypeOutVal"></param>
        /// <returns></returns>
        public MEMBERS.SQLReturnValue ExecuteProcedureWithParam_Datatable(string ProcedureName, object[,] ParamValue, DataTable dtExamAnswer, string TableParamName, int ReturnTypeOutVal)
        {
            SqlCommand COMMAND = new SqlCommand();
            COMMAND.CommandText = ProcedureName;
            SqlConnection MYCON = new SqlConnection(con.ConnectionString);
            COMMAND.Connection = MYCON;
            COMMAND.CommandTimeout = 0;
            COMMAND.CommandType = CommandType.StoredProcedure;
            SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
            for (int i = 0; i < param.Length; i++)
            {
                param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), ParamValue[i, 1].ToString());
            }
            COMMAND.Parameters.AddRange(param);
            if (dtExamAnswer != null)
            {
                SqlParameter ParamTb = new SqlParameter("@" + TableParamName, dtExamAnswer);
                ParamTb.SqlDbType = SqlDbType.Structured;
                COMMAND.Parameters.Add(ParamTb);
            }

            COMMAND.Parameters.Add("OUTVAL", (ReturnTypeOutVal == 1 ? SqlDbType.UniqueIdentifier : ReturnTypeOutVal == 2 ? SqlDbType.Int : SqlDbType.NVarChar), -1).Direction = ParameterDirection.Output;

            if (MYCON.State != ConnectionState.Open) { MYCON.Open(); }
            COMMAND.ExecuteNonQuery();
            MYCON.Close();
            MEMBERS.SQLReturnValue res = new MEMBERS.SQLReturnValue();
            res.Outval = COMMAND.Parameters["OUTVAL"].Value;
            return res;
        }

        /// <summary>
        /// Call when proc returns select query 
        /// 12/8/2017 pritey
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ProceduerName"></param>
        /// <param name="ParamValue"></param>
        /// <returns></returns>
        public List<TEntity> Get_GetAll_Data<TEntity>(string ProceduerName, object[,] ParamValue) where TEntity : class
        {
            List<TEntity> l = null;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = ProceduerName;
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
            for (int i = 0; i < param.Length; i++)
            {
                param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), (ParamValue[i, 1] == null ? null : (ParamValue[i, 1].ToString() == "null" ? null : ParamValue[i, 1].ToString())));
            }
            cmd.Parameters.AddRange(param);
            if (con.State != ConnectionState.Open) { con.Open(); }
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                l = CopyDataReaderToEntity<TEntity>(rdr);
            }
            con.Close();
            return l;
        }

        public static List<TEntity> CopyDataReaderToEntity<TEntity>(IDataReader dataReader) where TEntity : class
        {
            List<TEntity> entities = new List<TEntity>();
            try
            {
                PropertyInfo[] properties = typeof(TEntity).GetProperties();
                while (dataReader.Read())
                {
                    TEntity tempEntity = Activator.CreateInstance<TEntity>();
                    foreach (PropertyInfo property in properties)
                    {
                        try
                        {
                            SetValue<TEntity>(property, tempEntity, dataReader[property.Name]);
                        }
                        catch { }
                    }
                    entities.Add(tempEntity);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            return entities;
        }

        private static TEntity SetValue<TEntity>(PropertyInfo property, TEntity entity, object propertyValue) where TEntity : class
        {
            if (property.CanRead)
            {
                //if (property.PropertyType.Name != "String" &&
                //    property.PropertyType.Name != "Single" &&
                //    property.PropertyType.Name != "Int32" &&
                //    property.PropertyType.Name != "Int64" &&
                //    property.PropertyType.Name != "Guid" &&
                //    property.PropertyType.Name != "DateTime" &&
                //    property.PropertyType.Name != "Decimal" &&
                //    property.PropertyType.Name != "Boolean" &&
                //    property.PropertyType.Name != "LocationType" &&
                //    property.PropertyType.Name != "AlertCategory" &&
                //    property.PropertyType.Name != "AlertType" &&
                //    property.PropertyType.Name != "AppLogType" &&
                //    property.PropertyType.Name != "RegisterVia" &&
                //    property.PropertyType.Name != "CoinContentType" &&
                //    property.PropertyType.Name != "CoinPatternType" &&
                //    property.PropertyType.Name != "MasterStatus" &&
                //    property.PropertyType.Name != "MockQuestionType")
                //    return entity;
                if (propertyValue == null)
                {
                    if (property.PropertyType.Name == "String")
                        propertyValue = "";
                    else
                        propertyValue = 0;
                }
                if (property.CanWrite)
                {
                    if (propertyValue != DBNull.Value)
                    {
                        if (property.PropertyType.Name == "Single")
                            property.SetValue(entity, Convert.ToSingle(propertyValue), null);
                        else if (property.PropertyType.Name == "Int32")
                            property.SetValue(entity, Convert.ToInt32(propertyValue), null);
                        else if (property.PropertyType.Name == "Int64")
                            property.SetValue(entity, Convert.ToInt64(propertyValue), null);
                        else { property.SetValue(entity, propertyValue, null); }
                        //else if (property.PropertyType.Name == "String")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "Boolean")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "DateTime")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "Guid")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "Decimal")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "LocationType")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "AlertCategory")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "AlertType")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "AppLogType")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "RegisterVia")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "CoinContentType")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "CoinPatternType")
                        //    property.SetValue(entity, propertyValue, null);
                        //else if (property.PropertyType.Name == "MockQuestionType")
                        //    property.SetValue(entity, propertyValue, null);
                    }
                }
            }
            return entity;
        }



        /// <summary>
        /// --------------------------- 18/7/2017-------------------- pritey
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ProceduerName"></param>
        /// <returns></returns>
        public List<TEntity> Get_GetAll_Data_Without_Param<TEntity>(string ProceduerName) where TEntity : class
        {
            List<TEntity> l = null;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = ProceduerName;
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //SqlParameter[] param = new SqlParameter[ParamValue.GetUpperBound(0) + 1];
            //for (int i = 0; i < param.Length; i++)
            //{
            //    param[i] = new SqlParameter("@" + ParamValue[i, 0].ToString(), (ParamValue[i, 1] == null ? null : (ParamValue[i, 1].ToString() == "null" ? null : ParamValue[i, 1].ToString())));
            //}
            //cmd.Parameters.AddRange(param);
            if (con.State != ConnectionState.Open) { con.Open(); }
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                l = CopyDataReaderToEntity<TEntity>(rdr);
            }
            con.Close();
            return l;
        }


        public DataSet ExecProc_With_DataSet(string ProcName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandText = ProcName;
            if (con.State != ConnectionState.Open) { con.Open(); }
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }
    }
}
