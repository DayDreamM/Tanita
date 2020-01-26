using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Tanita.model;
using Tanita.Utill;

namespace Tanita.DAL
{
    class RequestUtill
    {
        DbUtill dbUtill = new DbUtill();
        CommonUtill commonUtill = new CommonUtill();
        public string PostRequestClient(string url, string json)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            var byteData = Encoding.UTF8.GetBytes(json);
            var length = byteData.Length;
            request.ContentLength = length;
            var writer = request.GetRequestStream();
            writer.Write(byteData, 0, length);
            writer.Close();
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
            return responseString.ToString();
        }
        public string PostRequestClientByHeader(string url, string json)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);    //创建一个请求示例 
            var byteData = Encoding.UTF8.GetBytes(json);
            request.Headers["info"] = byteData.ToString(); //添加头
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            string str = streamReader.ReadToEnd();
            return str;
        }
        public string GetRequestClient(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
           req.Method = "GET";
            WebResponse webResponse = req.GetResponse();
            Stream stream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string str = reader.ReadToEnd();   //url返回的值  
            reader.Close();
            webResponse.Close();
            return str;
        }
        public bool SyncSystemData()
        {
            bool flag = false;
            string schoolId = ConfigurationManager.AppSettings["schoolId"];
            string GradeUrl = ConfigurationManager.AppSettings["GradeUrl"];
            string ClassUrl = ConfigurationManager.AppSettings["ClassUrl"];
            string StudentIdsUrl = ConfigurationManager.AppSettings["StudentIdsUrl"];
            string StudentInfoUrl = ConfigurationManager.AppSettings["StudentsInfoUrl"];
            string TermUrl = ConfigurationManager.AppSettings["TermInfoUrl"];
            string key = ConfigurationManager.AppSettings["key"];
            string registerAppId = ConfigurationManager.AppSettings["requestAppId"];
            List<Grade> grades = new List<Grade>();
            List<Class> classes = new List<Class>();
            string s = "";
            IDictionary<string, string> pairs = new Dictionary<string, string>
            {
                { "schoolId", schoolId }
            };
            string hmac = commonUtill.GetHmac(pairs, key);
            string garderesult = PostRequestClient(GradeUrl, "{ \"data\":{ \"schoolId\":\"" + schoolId + "\"}}");
            string classresult = PostRequestClient(ClassUrl, "{ \"data\":{ \"schoolId\":\"" + schoolId + "\"}}");
            string studentIdsresult = PostRequestClient(StudentIdsUrl, "{\"data\":{\"schoolId\":\"" + schoolId + "\",\"hmac\":\"" + hmac + "\",\"registerAppId\":\"" + registerAppId + "\" } }");
            string termresult = GetRequestClient(TermUrl+"?schoolId="+schoolId);
            try
            {
                grades = JsonUtill.JsonToJArray(garderesult);
                classes = JsonUtill.ClassJsonToJArray(classresult);
                s = JsonUtill.StudentIdsJsonToJArray(studentIdsresult);
                Term term = JsonUtill.TermJsonParse(termresult);
                string studentInforesult = PostRequestClient(StudentInfoUrl, "{\"data\":{\"ids\":" + s + "}}");
                List<StudentInfo> studentInfos = JsonUtill.StudentInfoJsonParse(studentInforesult);
                dbUtill.UpdateTerm(term);
                dbUtill.UpdateGrade(grades);
                dbUtill.UpdateClass(classes);
                dbUtill.InsertStudentInfo(studentInfos);             
                flag = true;
            }
            catch (Exception)
            {
            }
            return flag;
        }
    }
}
