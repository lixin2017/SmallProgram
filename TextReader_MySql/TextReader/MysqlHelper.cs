using System;
using System.Collections;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Data;

namespace TextReader
{
    public class MysqlHelper
    {
        private static string _ConnectionString = ConfigurationManager.ConnectionStrings["mysql"].ToString();
        public static string ConnectionString
        {
            get { return _ConnectionString; }
        }
        private static MySqlConnection conn = new MySqlConnection(ConnectionString);
        public static  void MysqlOpen()
        {           
            conn.Open();
        }

        public static void MysqlClose()
        {
            conn.Close();
        }
        public static void InsertData(string path,int page)
        {
            if (!string.IsNullOrEmpty(path))
            {
                string filename = path.Substring(path.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
                filename = filename.Substring(0, filename.IndexOf('.'));
                path = path.Replace("\\", "\\\\");
                try
                {
                    //MySqlConnection conn = new MySqlConnection(ConnectionString);
                    //conn.Open();
                    string sql = "SELECT * FROM article where filename='" + filename + "'";
                    MySqlCommand com = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = com.ExecuteReader();
                    if (!reader.Read())
                    {
                        string sql1 = "INSERT INTO article VALUES('" + filename + "','" + path + "','" + page + "')";
                        com = new MySqlCommand(sql1, conn);
                        reader.Close();
                        int i = com.ExecuteNonQuery();
                    }
                    //conn.Close();
                }
                catch
                {

                }
            }
            
        }

        public static DataSet SelectData()
        {            
            try
            {
                //MySqlConnection conn = new MySqlConnection(ConnectionString);
                //conn.Open();
                string sql = "SELECT * FROM article";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                DataSet filedata = new DataSet();
                adapter.Fill(filedata);               
                return filedata;
                //conn.Close();
            }
            catch
            {
                return null;
            }
           
        }

        public static DataSet FileRead(string filename)
        {
            try
            {
                //MySqlConnection conn = new MySqlConnection(ConnectionString);
                //conn.Open();
                string sql = "SELECT * FROM article WHERE article.filename='" + filename + "'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                DataSet filedata = new DataSet();
                adapter.Fill(filedata);
                return filedata;
                //conn.Close();
            }
            catch
            {
                return null;
            }
            
        }
        public static bool IsConnected()
        {
            try
            {                
                //MySqlConnection conn = new MySqlConnection(ConnectionString);
                //conn.Open();
                string sql = "SELECT*FROM article";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();                
                if (reader!=null)
                {
                    return true;
                }
                return false;
                //conn.Close();
            }
            catch (MySqlException ex)
            {
                return false;
            }
           
        }

        public static void UpdatePage(string filename,int page)
        {    
            try
            {
                string str = filename.Substring(filename.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
                str = str.Substring(0, str.IndexOf('.'));
                //MySqlConnection conn = new MySqlConnection(ConnectionString);
                //conn.Open();
                string sql = "UPDATE article SET page='" + page + "'WHERE filename='" + str + "'";
                MySqlCommand com = new MySqlCommand(sql, conn);
                com.ExecuteNonQuery();
                //conn.Close();
            }
            catch
            {
                
            }
        }

        public static void DeleteData()
        {
            try
            {
                //MySqlConnection conn = new MySqlConnection(ConnectionString);
                //conn.Open();
                string sql = "DELETE FROM article";
                MySqlCommand com = new MySqlCommand(sql, conn);
                com.ExecuteNonQuery();
                //conn.Close();
            }
            catch
            {
                
            }
        }
        public static  List<string> DataReaderTest()
        {
            string connectionString2 = "Host=localhost;DataBase=tpi;Username=root;Password=123456";
            MySqlConnection myconn = new MySqlConnection(connectionString2);
            myconn.Open();
            MySqlCommand com = new MySqlCommand();
            com.Connection = myconn;
            com.CommandText = "SELECT * FROM userloginlog";
            MySqlDataReader reader = com.ExecuteReader();
            List<string> list = new List<string>();
            while (reader.Read())
            {            
                list.Add(reader.GetString(1)+"   "+reader["IP"]+"   "+reader[4]);
            }
            return list;
            myconn.Close();
        }

        public static DataSet DataSetTest()
        {
            try
            {
                string constring = ConfigurationManager.ConnectionStrings["mysql"].ToString();
                MySqlConnection myconn = new MySqlConnection(constring);
                string sql = "SELECT * FROM userloginlog";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(sql, myconn);
                MySqlCommandBuilder cmdbld = new MySqlCommandBuilder(myAdapter);
                DataSet myData = new DataSet();
                myAdapter.Fill(myData);
                return myData;
            }
            catch
            {
                return null;
            }
            
        }
    }
    }

