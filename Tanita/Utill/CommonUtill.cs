using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Tanita.model;

namespace Tanita.Utill
{
    class CommonUtill
    {
        public string GetAgeByBirthday(string birthday)
        {
            int year = int.Parse(birthday.Substring(0, 4));
            int now = int.Parse(DateTime.Now.Year.ToString());
            string age = (now - year).ToString();
            return age;
        }
        public DataTable GetDataTableByStudentList(List<Student> students,string activityId)
        {
            //构建表
            DataTable table = new DataTable("measure_data_student");
            DataColumn dc1 = new DataColumn("ID", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("activityId", Type.GetType("System.String"));
            DataColumn dc3 = new DataColumn("studentId", Type.GetType("System.String"));
            table.Columns.Add(dc1);
            table.Columns.Add(dc2);
            table.Columns.Add(dc3);
            foreach(Student student in students)
            {
                DataRow row = table.NewRow();
                row["ID"] = Guid.NewGuid().ToString();
                row["activityId"] = activityId;
                row["studentId"] = student.studentId;
                table.Rows.Add(row);
            }
            return table;
        }
        public string GetHmac(IDictionary<string,string> requestData,string key)
        {
            StringBuilder param = new StringBuilder("");
            foreach(KeyValuePair<string,string> pair in requestData){
                param.Append(pair.Key + "=" + pair.Value+"&");
            }
            param.Append("key="+key);
            string hmac = GetMD5(param.ToString());
            return hmac;
        }
        //获取字符串MD5值
        public static string GetMD5(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = Encoding.ASCII.GetBytes(str);
            byte[] MD5Buffer = md5.ComputeHash(buffer);
            string strNew = "";
            for(int i = 0; i < MD5Buffer.Length; i++)
            {
                strNew += MD5Buffer[i].ToString("x2");
            }
            return strNew;
        }
        //获取文件MD5值
        private static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }
        //文件工具
        public static ImageInfo ImageFileInfo(string path)
        {
            ImageInfo info = new ImageInfo();
            string md5 = GetMD5HashFromFile(path);
            FileInfo fileInfo = new FileInfo(path);
            double length = Convert.ToDouble(fileInfo.Length);
            info.image_size = length;
            info.image_code = md5;
            info.fileName = fileInfo.Name;
            info.image_time = fileInfo.CreationTime.ToString("d");
            return info;
        }
        //根据日期计算学年工具
        public static string GetYearSemester(string activityName)
        {
            return activityName.Substring(0, 9)+"学年";
        }
    }
}
