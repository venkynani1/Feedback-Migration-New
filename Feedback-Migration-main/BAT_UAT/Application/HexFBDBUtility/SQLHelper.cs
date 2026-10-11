/// ***********************************************************************************************
/// File name: SQLHelper.cs
/// Description : 
/// Created date :18 Apr 2012
/// Created by :  Sureshkumar D, Hexaware Technologies Ltd.
///
///
/// Change History
/// -----------------------------------------------------------------------------------------------
/// Date            Author              Reviewed date      Reviewed by      Notes
/// -----------------------------------------------------------------------------------------------
/// 
/// *************************************************************************************************
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Hex.Vars.FB.HexFBModule;

namespace Hex.Vars.FB.HexFBDBUtility
{

    public abstract class SqlHelper
    {

        //Database connection strings
         public static readonly string ConnectionString= ConfigurationSettings.AppSettings["ConnectionString"];
         public static readonly string ExcelConnectString = ConfigurationSettings.AppSettings["ExcelConnectString"];
         public static readonly string UserName = ConfigurationSettings.AppSettings["UserName"];
         public static readonly string Password = ConfigurationSettings.AppSettings["Password"];

        // Hashtable to store cached parameters
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
       
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message);
                conn.Close();
                throw;
            }
        }

        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        public static DataTable getvalue(string connectionstring, CommandType cmd,string cmdtext,params SqlParameter[] commandParameters)
        {
           
            SqlCommand cmds = new SqlCommand();
            
             SqlConnection conn = new SqlConnection(ConnectionString);
             try
             {
                 PrepareCommand(cmds, conn, null, cmd, cmdtext, commandParameters);
                 SqlDataAdapter adp = new SqlDataAdapter(cmds);
                 DataTable gettable = new DataTable();
                 adp.Fill(gettable);
                 return gettable;
             }
             catch (Exception ex)
             {
                 System.Diagnostics.Debug.Write(ex.Message);
                 conn.Close();
                 throw;
             }
        }
       
        public static DataSet getvalue1(string connectionstring, CommandType cmd, string cmdtext, params SqlParameter[] commandParameters)
        {

            SqlCommand cmds = new SqlCommand();

            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                PrepareCommand(cmds, conn, null, cmd, cmdtext, commandParameters);
                SqlDataAdapter adp = new SqlDataAdapter(cmds);
                DataSet gettable = new DataSet();
                adp.Fill(gettable);
                return gettable;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message);
                conn.Close();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable getvalues(string connection, CommandType cmd, string cmdtext, params SqlParameter[] commandParameters)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.CommandType = cmd;
            cmds.CommandText = cmdtext;
          
            //cmds.Parameters[0].Value = value;
            
            
             SqlConnection conn = new SqlConnection(ConnectionString);
             try
             {
                 conn.Open();
                 DataTable dt = new DataTable();
                 SqlParameter[] cmdParms = new SqlParameter[10];
                 if (commandParameters != null)
                 {
                     Hex.Vars.FB.HexFBModule.Sessioninfo ss=new Hex.Vars.FB.HexFBModule.Sessioninfo();
                     foreach (SqlParameter parm in commandParameters)
                         cmds.Parameters.Add(parm);
                     
                   
                 }
                 SqlDataAdapter da = new SqlDataAdapter(cmds);
                 da.Fill(dt);
                 return dt;
             }
             catch (Exception ex)
             {
                 System.Diagnostics.Debug.Write(ex.Message);
                 conn.Close();
                 throw;
             }
        }
    }
}
