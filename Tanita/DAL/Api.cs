using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows;
using Tanita.model;

namespace Tanita.DAL
{
    class Api
    {
        string conStr = ConfigurationManager.AppSettings["conStr"];
        public bool Login(string username,string password)
        {
            bool flag = true;
            try
            {
                OleDbConnection conn = new OleDbConnection(conStr);
                string sql = "select count(*) from [user]  where username =@username and password = @password";
                conn.Open();
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                int i = (int)cmd.ExecuteScalar();
                if (i > 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("数据库连接错误"+e.Message, "错误");
            }
            return flag;
        }
        public int EditPassword(string txtnewPassword,string username)
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = string.Format("update [user] set [password]='{0}' where [ID]=1", txtnewPassword);
            conn.Open();
            OleDbCommand command = new OleDbCommand(sql, conn);
            int i =  command.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        public User GetUser()
        {
            User user = new User();
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = " select * from userRegister ";
            conn.Open();
            OleDbCommand command = new OleDbCommand(sql, conn);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                user.username = reader.GetValue(1).ToString();
                user.password = reader.GetValue(2).ToString();
                user.status = reader.GetValue(3).ToString();
            }
            conn.Close();
            return user;
        }
        public int RemamberPassword(User user)
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = string.Format("update userRegister set username ='{0}',[password]='{1}' ,status = '{2}' where [ID]=1",user.username,user.password,user.status);
            conn.Open();
            OleDbCommand command = new OleDbCommand(sql, conn);
            int i = command.ExecuteNonQuery();
            conn.Close();
            return i;
        }
    }
}
