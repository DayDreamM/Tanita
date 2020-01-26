using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using Tanita.model;

namespace Tanita.Utill
{
    class JsonUtill
    {
        //获得json resultValue
        public static string ParseJsonToString(string json)
        {
            string result = "";
            try
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(json);
                result = jo["resultValue"].ToString();
            }
            catch (Exception)
            {

            }
            return result;
        }
        public static List<Grade> JsonToJArray(string json)
        {
                string result = ParseJsonToString(json);
                List<Grade> grades = new List<Grade>();
                JArray jArray = (JArray)JsonConvert.DeserializeObject(result);
                for (int i = 0; i < jArray.Count; i++)
                {
                    Grade grade = new Grade
                    {
                        id = jArray[i]["id"].ToString().Replace("\"", ""),
                        gradeIndex = jArray[i]["gradeIndex"].ToString().Replace("\"", ""),
                        gradeName = jArray[i]["gradeName"].ToString().Replace("\"", "")
                    };
                    grades.Add(grade);
                }
            return grades;
        }
        public static List<Class> ClassJsonToJArray(string json)
        {
            string result = ParseJsonToString(json);
            List<Class> classes = new List<Class>();
            JArray jArray = (JArray)JsonConvert.DeserializeObject(result);
            for(int i = 0; i < jArray.Count; i++)
            {
                Class class1 = new Class
                {
                    id = jArray[i]["id"].ToString().Replace("\"", ""),
                    startGrade = jArray[i]["startGrade"].ToString().Replace("\"", ""),
                    startYear = jArray[i]["startYear"].ToString().Replace("\"", ""),
                    classIndex = jArray[i]["classIndex"].ToString().Replace("\"", ""),
                    className = jArray[i]["className"].ToString().Replace("\"", "")
                };
                classes.Add(class1);
            }
            return classes;
        }
        public static List<StudentInfo> StudentInfoJsonParse(string json)
        {
            string result = ParseJsonToString(json);
            List<StudentInfo> studentInfos = new List<StudentInfo>();
            JArray jArray = (JArray)JsonConvert.DeserializeObject(result);
            for(int i = 0; i < jArray.Count; i++)
            {
                StudentInfo info = new StudentInfo
                {
                    eduId = jArray[i]["eduId"].ToString().Replace("\"", ""),
                    birthDay = jArray[i]["birthday"].ToString().Replace("\"",""),
                    id = jArray[i]["id"].ToString().Replace("\"", ""),
                    sex = jArray[i]["sex"].ToString().Replace("\"", ""),
                    gradeIndex = jArray[i]["gradeIndex"].ToString().Replace("\"", ""),
                    classIndex = jArray[i]["classIndex"].ToString().Replace("\"",""),
                    studentCode = jArray[i]["studentCode"].ToString().Replace("\"", ""),
                    age = jArray[i]["age"].ToString().Replace("\"", ""),
                    name = jArray[i]["name"].ToString().Replace("\"", ""),
                    certificateNo = jArray[i]["certificateNo"].ToString().Replace("\"", ""),
                    className = jArray[i]["className"].ToString().Replace("\"", ""),
                    gradeName = jArray[i]["grade"].ToString().Replace("\"", "")
                };
                studentInfos.Add(info);
            }
            return studentInfos;
        }
        public static string StudentIdsJsonToJArray(string json)
        {
            string result = ParseJsonToString(json);
            JObject jObject = (JObject)JsonConvert.DeserializeObject(result);
            string s = jObject["ids"].ToString();
            string res = s.Replace("\r", "").Replace("\n","");
            return res;
        }
        public static Term TermJsonParse(string json)
        {
            Term term = new Term();
            string result = ParseJsonToString(json);
            JObject jObject = (JObject)JsonConvert.DeserializeObject(result);
            term.termId = jObject["termId"].ToString().Replace("\"", "");
            term.termName = jObject["year"]["yearName"].ToString().Replace("\"", "");
            term.startTime = jObject["startDate"].ToString().Replace("\"", "");
            term.endTime = jObject["endDate"].ToString().Replace("\"", "");
            return term;
        }
    }
}