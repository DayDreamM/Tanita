using System.Data.OleDb;
using System.Data;
using Tanita.model;
using System.Collections.Generic;
using System;
using Tanita.Utill;
using System.Configuration;
using System.Text;

namespace Tanita
{
    class DbUtill
    {
        CheckDataUtill checkDataUtill = new CheckDataUtill();
        CommonUtill commonUtill = new CommonUtill();
        string conStr = ConfigurationManager.AppSettings["conStr"];
        //获取体侧活动数据
        public List<Activity> GetActivities()
        {
            List<Activity> activities = new List<Activity>();
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = "select * from measure_activity where isFile=0";
            //获取表的内容
            OleDbCommand oleDb = new OleDbCommand(sql, conn);
            conn.Open();
            OleDbDataReader reader;
            reader = oleDb.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Activity activity = new Activity
                    {
                        id = reader[0].ToString(),
                        ActivityName = reader[1].ToString(),
                        activityDate = reader[3].ToString(),
                        activityInfo = reader[0].ToString() + "," + reader[1].ToString()
                    };
                    activities.Add(activity);
                }
                return activities;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //查询体侧活动数据
        public List<Activity> QueryActivities(string keyword,string startDate,string endDate)
        {
            List<Activity> activities = new List<Activity>();
            OleDbConnection conn = new OleDbConnection(conStr);
            StringBuilder sql = new StringBuilder("select * from measure_activity where isFile= 1 ");
            if (!keyword.Equals(""))
            {
                sql.Append(" and activityName like '%" + keyword + "%'");
            }
            if (!startDate.Equals(""))
            {
                sql.Append(string.Format(" and activityDate >= #{0}# ", startDate));
            }
            if (!endDate.Equals(""))
            {
                sql.Append(string.Format(" and activityDate <= #{0}# ", endDate));
            }
            //获取表的内容
            OleDbCommand oleDb = new OleDbCommand(sql.ToString(), conn);
            conn.Open();
            OleDbDataReader reader;
            reader = oleDb.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Activity activity = new Activity
                    {
                        id = reader[0].ToString(),
                        ActivityName = reader[1].ToString(),
                        activityDate = reader[3].ToString(),
                        activityInfo = reader[0].ToString() + "," + reader[1].ToString()
                    };
                    activities.Add(activity);
                }
                return activities;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //获取体侧活动名称
        public string GetActivitieNameByID(string activityId)
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = "select activityName from measure_activity where isFile=0 and id =@activityId";
            //获取表的内容
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.AddWithValue("@activityId", activityId);
            conn.Open();
            string res = (string)cmd.ExecuteScalar();
             conn.Close();
            return res;
        }
        //获取体侧活动名称和时间
        public Activity GetActivitieInfoByID(string activityId)
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = "select * from measure_activity where isFile = 0 and id = @activityId";
            //获取表的内容
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.AddWithValue("@activityId", activityId);
            conn.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            Activity activity = new Activity();
            try
            {
                while (reader.Read())
                {
                    activity.ActivityName = reader.GetValue(1).ToString();
                    activity. activityDate = reader.GetValue(2).ToString();
                }
                return activity;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //获取教师ID
        public string GetTeacherIDByName(string memberName)
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = string.Format("select * from measure_member where  memberName = '{0}' ", memberName);
            string s = "";
            //获取表的内容
            OleDbCommand oleDb = new OleDbCommand(sql, conn);
            conn.Open();
            OleDbDataReader reader = oleDb.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    s = reader.GetValue(0).ToString();
                }
                return s;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //获取归档数据
        public List<Activity> GetIsFiles()
        {
            List<Activity> activities = new List<Activity>();
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = "select * from measure_activity where isFile=1";
            //获取表的内容
            OleDbCommand oleDb = new OleDbCommand(sql, conn);
            conn.Open();
            OleDbDataReader reader;
            reader = oleDb.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Activity activity = new Activity
                    {
                        id = reader[0].ToString(),
                        ActivityName = reader[1].ToString(),
                        activityDate = reader[3].ToString(),
                        activityInfo = reader[0].ToString() + "," + reader[1].ToString()
                    };
                    activities.Add(activity);
                }
                return activities;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //体测活动归档
        public int IsFileActivity(string id)
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = string.Format("update measure_activity set isFile = 1 where ID='{0}'", id);
            conn.Open();
            OleDbCommand command = new OleDbCommand(sql, conn);
            int i = command.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        //体侧活动取消归档
        public int CancelIsFileActivity(string id)
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = string.Format("update measure_activity set isFile = 0 where ID='{0}'", id);
            conn.Open();
            OleDbCommand command = new OleDbCommand(sql, conn);
            int i = command.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        //新增体测活动
        public void AddActivity(Activity activity)
        {
            string uuid = Guid.NewGuid().ToString();
            List<Student> students = GetstuNameList();
            DataTable table = commonUtill.GetDataTableByStudentList(students, uuid);
            string insert = string.Format("insert into measure_activity(ID,activityName,isFile,activityDate) values('{0}','{1}','{2}','{3}')", uuid, activity.ActivityName, activity.isFile, activity.activityDate);
            string insert2 = " insert into measure_data_student(ID,activityId,studentId) values(@ID,@activityId,@studentId)";
            try
            {
                OleDbConnection conn = new OleDbConnection(conStr);
                conn.Open();
                OleDbCommand odc = new OleDbCommand(insert, conn);
                OleDbCommand odc2 = new OleDbCommand(insert2,conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter("select * from measure_data_student", conn);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                OleDbParameter ID = new OleDbParameter("ID", OleDbType.VarWChar, 255);
                OleDbParameter activityId = new OleDbParameter("activityId", OleDbType.VarWChar, 255);
                OleDbParameter studentId = new OleDbParameter("studentId", OleDbType.VarWChar, 255);
                adapter.Fill(table);
                adapter.SelectCommand.Parameters.Add(ID);
                adapter.SelectCommand.Parameters.Add(activityId);
                adapter.SelectCommand.Parameters.Add(studentId);
                adapter.UpdateCommand = builder.GetUpdateCommand();
                if(table != null)
                {
                    adapter.Update(table);
                }
                odc.ExecuteNonQuery();
                conn.Close();
            }catch (Exception)
            {

            }
        }
        //删除体侧活动
        public int DeleteActivity(string id)
        {
            string deleteSql = string.Format("delete from measure_activity where ID = '{0}'", id);
            string deleteSql2 = string.Format("delete from measure_data_student where activityId = '{0}' ",id);
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            OleDbCommand odc = new OleDbCommand(deleteSql, conn);
            OleDbCommand odc2 = new OleDbCommand(deleteSql2, conn);
            int i = odc.ExecuteNonQuery();
            int i2 = odc2.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        //删除检测记录
        public int DeleteCheckData(string activityId,string studentId)
        {
            string deleteSql = string.Format("update  measure_data_student set  jsonContent = null where activityId = '{0}' and studentId = '{1}'", activityId,studentId);
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            OleDbCommand odc = new OleDbCommand(deleteSql, conn);
            int i = odc.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        //获取年级信息
        public DataTable GetGradeinfo()
        {
            string query = " select * from Grade ";
            OleDbConnection conn = new OleDbConnection(conStr);
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }
        //获取年级信息
        public string GetGradeNO1()
        {
            string query = " select  a.gradeIndex from Grade as a inner join Class as b on a.gradeIndex = b.gradeId order by b.gradeId";
            OleDbConnection conn = new OleDbConnection(conStr);
            OleDbCommand cmd = new OleDbCommand(query, conn);
            conn.Open();
            string res = (string)cmd.ExecuteScalar();
            conn.Close();
            return res;
        }
        //获取年级名称
        public string getGradeNameByGradeId(string gradeId = "01")
        {
            using (OleDbConnection conn = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;//为cmd的Connetion属性赋值
                    cmd.CommandText = "select  gradeName  from  Grade  where gradeIndex = @id";  //查询语句@id为参数
                    cmd.Parameters.AddWithValue("@id", gradeId);//传参
                    string res = cmd.ExecuteScalar().ToString();//获取你需要的结果
                    return res;
                }
            }
        }
        //获取班级名称
        public string getClassNameByClassId(string ClassId = "1")
        {
            using (OleDbConnection conn = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;//为cmd的Connetion属性赋值
                    cmd.CommandText = "select  className  from  Class  where ID = @id";  //查询语句@id为参数
                    cmd.Parameters.AddWithValue("@id", ClassId);//传参
                    string res = cmd.ExecuteScalar().ToString();
                    return res;
                }
            }
        }
        //获取当前学年班级信息
        public DataTable GetClassInfo(string gradeId)
        {
            string currentterm = GetCerrentTerm();
            string query = " select * from Class where gradeId = '" + gradeId + "' and startYear = '"+currentterm+"'";
            OleDbConnection conn = new OleDbConnection(conStr);
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }
        public string GetClassNO1(string gradeId)
        {
            string query = " select * from Class where gradeId = '" + gradeId + "' order by classIndex";
            OleDbConnection conn = new OleDbConnection(conStr);
            OleDbCommand cmd = new OleDbCommand(query, conn);
            conn.Open();
            string res = (string)cmd.ExecuteScalar();
            conn.Close();
            return res;
        }
        //获取学生信息
        public List<Student> GetstuNameList()
        {
            List<Student> list = new List<Student>();
            string query = " select * from Member ";
            OleDbConnection conn = new OleDbConnection(conStr);
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Student stn = new Student();
                stn.studentId = dt.Rows[i][1].ToString();
                stn.studentName = dt.Rows[i][2].ToString();
                stn.age = dt.Rows[i][3].ToString();
                list.Add(stn);
            }
            conn.Close();
            return list;
        }
        public List<Student> GetCheckerListByClassId(string classId)
        {
            List<Student> list = new List<Student>();
            string query = "select  * from  b_student_class  as a left join Member as b on a.studentId = b.ID where a.classId =@classId order by a.studentId";
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand(query,conn);
            cmd.Parameters.AddWithValue("@classId", classId);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Student stn = new Student
                {
                    studentId = reader.GetValue(6).ToString(),
                    studentName = reader.GetValue(7).ToString(),
                    age = reader.GetValue(8).ToString()
                };
                string sex = reader.GetValue(9).ToString();
                if (sex.Equals("男"))
                {
                    stn.imagePath = "../Resources/checkernan.png";
                    stn.sex_string = "男";
                }
                else
                {
                    stn.imagePath = "../Resources/checkernv.png";
                    stn.sex_string = "女";
                }
                stn.height = reader.GetValue(10).ToString();
                list.Add(stn);
            }
            conn.Close();
            return list;
        }
        public List<Student> GetCheckerListByClassIdOnTest(string classId,string activityId)
        {
            List<Student> list = new List<Student>();
            string query = " select  * from  (b_student_class  as a left join Member as b on a.studentId = b.ID)  left join measure_data_student as c on a.studentId = c.studentId where a.classId =@classId and c.activityId  = @activityId and c.jsonContent is null order by a.studentId";
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@classId", classId);
            cmd.Parameters.AddWithValue(@"activityId", activityId);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Student stn = new Student
                {
                    studentId = reader.GetValue(6).ToString(),
                    studentName = reader.GetValue(7).ToString(),
                    age = reader.GetValue(8).ToString()
                };
                string sex = reader.GetValue(9).ToString();
                if (sex.Equals("男"))
                {
                    stn.imagePath = "../Resources/checkernan.png";
                    stn.sex_string = "男";
                }
                else
                {
                    stn.imagePath = "../Resources/checkernv.png";
                    stn.sex_string = "女";
                }
                stn.height = reader.GetValue(10).ToString();
                list.Add(stn);
            }
            conn.Close();
            return list;
        }
        //已检测人员数量
        public int GetCountByClassIdOnTest(string classId, string activityId)
        {
            string query = " select  count(*) from  (b_student_class  as a left join Member as b on a.studentId = b.ID)  left join measure_data_student as c on a.studentId = c.studentId where a.classId =@classId and c.activityId  = @activityId and c.jsonContent is not null";
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@classId", classId);
            cmd.Parameters.AddWithValue(@"activityId", activityId);
            int i = (int)cmd.ExecuteScalar();
            conn.Close();
            return i;
        }
        public List<Student> GetStudentListByClassIdAndActivityId(string classId, string activityId)
        {
            List<Student> list = new List<Student>();
            string query = " select  * from  (b_student_class  as a left join Member as b on a.studentId = b.ID)  left join measure_data_student as c on a.studentId = c.studentId where a.classId =@classId and c.activityId  = @activityId order by  iif(c.jsonContent is not null,1),a.studentId";
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@classId", classId);
            cmd.Parameters.AddWithValue(@"activityId", activityId);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Student stn = new Student
                {
                    studentId = reader.GetValue(6).ToString(),
                    studentName = reader.GetValue(7).ToString(),
                    age = reader.GetValue(8).ToString()
                };
                string sex = reader.GetValue(9).ToString();
                if (sex.Equals("男"))
                {
                    stn.imagePath = "../Resources/checkernan.png";
                    stn.sex_string = "男";
                }
                else
                {
                    stn.imagePath = "../Resources/checkernv.png";
                    stn.sex_string = "女";
                }
                stn.height = reader.GetValue(10).ToString();
                string isCheck = reader.GetValue(18).ToString();
                if (isCheck == null || isCheck.Equals(""))
                {
                    stn.isCheck = "未检测";
                }
                else
                {
                    stn.isCheck = "已检测";
                }
                list.Add(stn);
            }
            conn.Close();
            return list;
        }
        //
        public int GetActivityCheckerCount(string activityId)
        {
            using(OleDbConnection conn = new OleDbConnection(conStr))
            {
                conn.Open();
                using(OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select count(*) from measure_data_student where activityId = '"+activityId+"' and jsonContent is not null";
                     int i = (int)cmd.ExecuteScalar();
                    return i;
                }
            }
        }
        public string GetStudentInfoByStudentId(string studentId)
        {
            string query = string.Format(" select * from (b_student_class as a left join Class as  b  on a.classId = b.ID) left join Grade as c on b.gradeId = c.gradeIndex where  a.studentId = '{0}'", studentId);
            OleDbConnection conn = new OleDbConnection(conStr);
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string res = dt.Rows[0][6].ToString() + "," +dt.Rows[0][12].ToString();
                conn.Close();
                return res;
            }
            else
            {
                conn.Close();
                return null;
            }
        }
        //获取活动下全部检测数据
        public List<StudentCheckData> GetStudentCheckDatasByActivityId(string activityId)
        {
            List<StudentCheckData> checkDatalist = new List<StudentCheckData>();
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = string.Format("select * from measure_data_student where activityId='{0}' and jsonContent is not null", activityId);
            OleDbCommand command = new OleDbCommand(sql, conn);
            conn.Open();
            OleDbDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    StudentCheckData checkData = new StudentCheckData
                    {
                        Id = reader.GetValue(0).ToString(),
                        activityId = reader.GetValue(1).ToString(),
                        studentId = reader.GetValue(2).ToString(),
                        jsonContent = reader.GetValue(6).ToString()
                    };
                    checkDatalist.Add(checkData);
                }
                return checkDatalist;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //获取活动下全部上传数据
        public List<StudentCheckData> GetUploadDatasByActivityId(string activityId)
        {
            List<StudentCheckData> checkDatalist = new List<StudentCheckData>();
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = string.Format("select * from measure_data_student as a left join  Member as b on a.studentId = b.id  where activityId='{0}' and a.jsonContent is not null", activityId);
            OleDbCommand command = new OleDbCommand(sql, conn);
            conn.Open();
            OleDbDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    StudentCheckData checkData = new StudentCheckData
                    {
                        Id = reader.GetValue(0).ToString(),
                        activityId = reader.GetValue(1).ToString(),
                        studentId = reader.GetValue(2).ToString(),
                        jsonContent = reader.GetValue(6).ToString(),
                        NID = reader.GetValue(9).ToString()
                    };
                    checkDatalist.Add(checkData);
                }
                return checkDatalist;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //获取教师检测数据
        public List<TeacherCheckData> GetTeacherCheckDataList(string memberName,string time1,string time2,string membersex,string isUpload)
        {
            List<TeacherCheckData> teachers = new List<TeacherCheckData>();
            StringBuilder sql = new StringBuilder("select * from measure_data_member where 1=1 ");
            OleDbConnection conn = new OleDbConnection(conStr);
            if (!memberName.Equals(""))
            {
                sql.Append(string.Format(" and memberId = '{0}' ", memberName));
            }
            if (!time1.Equals(""))
            {
                sql.Append(string.Format(" and checkDate >= #{0}# ", time1));
            }
            if (!time2.Equals(""))
            {
                sql.Append(string.Format(" and checkDate <= #{0}# ", time2));
            }
            if (!membersex.Equals(""))
            {
                sql.Append(string.Format(" and memberSex = '{0}' ", membersex));
            }
            if (!isUpload.Equals(""))
            {
                sql.Append(string.Format(" and isUpload = '{0}' ", isUpload));
            }
            sql.Append(" and jsonContent is not null");
            OleDbCommand command = new OleDbCommand(sql.ToString(), conn);
            conn.Open();
            OleDbDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    TeacherCheckData checkData = new TeacherCheckData
                    {
                        Id = reader.GetValue(0).ToString(),
                        memberId = reader.GetValue(1).ToString(),
                        memberName = reader.GetValue(2).ToString(),
                        memberAge = reader.GetValue(3).ToString(),
                        memberHeight = double.Parse(reader.GetValue(5).ToString()),
                        JsonContent = reader.GetValue(6).ToString(),
                        checkDate = reader.GetValue(7).ToString()
                    };
                    teachers.Add(checkData);
                }
                return teachers;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //通过活动ID,学生ID获得体侧数据
        public StudentCheckData GetStudentCheckInfo(string activityId, string studentId)
        {
            StudentCheckData checkData = new StudentCheckData();
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = "select * from measure_data_student where activityId=@activityId and studentId=@studentId and jsonContent is not null";
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@activityId", activityId);
            cmd.Parameters.AddWithValue("@studentId", studentId);
            OleDbDataReader reader = cmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    checkData.Id = reader.GetValue(0).ToString();
                    checkData.activityId = reader.GetValue(1).ToString();
                    checkData.studentId = reader.GetValue(2).ToString();
                    checkData.checkDate = reader.GetValue(5).ToString();
                    checkData.jsonContent = reader.GetValue(6).ToString();
                }
                return checkData;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //通过教师ID获取体侧数据
        public List<TeacherCheckData> GetTeacherCheckDatasByMemberId(string memberId)
        {
            List<TeacherCheckData> checkDatalist = new List<TeacherCheckData>();
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = string.Format("select * from measure_data_member where memberId='{0}'", memberId);
            OleDbCommand command = new OleDbCommand(sql, conn);
            conn.Open();
            OleDbDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    TeacherCheckData checkData = new TeacherCheckData();
                    string backStr = reader.GetValue(6).ToString();
                    string errMsg = "";
                    checkDataUtill.GetTeacherCheckDataByBackStr(backStr, out checkData, out errMsg);
                    checkData.Id = reader.GetValue(0).ToString();
                    checkData.memberId = reader.GetValue(1).ToString();
                    checkData.memberName = reader.GetValue(2).ToString();
                    checkData.memberBirthday = reader.GetValue(3).ToString();
                    checkData.memberAge = commonUtill.GetAgeByBirthday(reader.GetValue(3).ToString());
                    checkData.memberHeight = int.Parse(reader.GetValue(5).ToString());
                    checkData.JsonContent = reader.GetValue(6).ToString();
                    checkData.checkDate = reader.GetValue(7).ToString();
                    checkDatalist.Add(checkData);
                }
                return checkDatalist;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //通过ID获取教师信息
        public Teacher GetTeacherByMemberId(string memberId)
        {
            Teacher teacher = new Teacher();
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = string.Format("select * from measure_member where Id='{0}'", memberId);
            OleDbCommand command = new OleDbCommand(sql, conn);
            conn.Open();
            OleDbDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    int sex = int.Parse(reader[3].ToString());
                    string sex_s = "";
                    if (sex == 0)
                    {
                        sex_s = "男";
                    }
                    else
                    {
                        sex_s = "女";
                    }
                    teacher.memberId = reader[0].ToString();
                    teacher.memberName = reader[1].ToString();
                    teacher.memberSex = sex_s;
                    teacher. memberAge = reader[2].ToString().Split(' ')[0];
                    teacher.memberHeight = reader[4].ToString();
                }
                return teacher;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //获取评价
        public Comment GetComment()
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = "select * from comment";
            OleDbCommand command = new OleDbCommand(sql, conn);
            conn.Open();
            OleDbDataReader reader = command.ExecuteReader();
            try
            {
                Comment comment = new Comment();
                while (reader.Read())
                {
                    comment.low = reader.GetValue(1).ToString();
                    comment.mid = reader.GetValue(2).ToString();
                    comment.hight = reader.GetValue(3).ToString();
                }
                return comment;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //修改评价
        public int EditComment(string low,string mid,string hight)
        {
            using (OleDbConnection conn = new OleDbConnection(conStr))
            {
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "update comment set BMILow = '" + low + "',BMIMid='" + mid + "',BMIHight='" + hight + "'";
                    int i = cmd.ExecuteNonQuery();
                    return i;
                }
            }
        }
        //删除被体侧人以及体侧记录
        public bool DeleteMemberByID(string memberId)
        {
            bool flag = false;
            OleDbConnection conn = new OleDbConnection(conStr);
            OleDbCommand cmd = new OleDbCommand();
            OleDbTransaction transaction = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                transaction = conn.BeginTransaction();
                cmd.Transaction = transaction;
                cmd.Connection = conn;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format(" delete from measure_member  where Id = '{0}' ", memberId);
                if (cmd.ExecuteNonQuery() < 0)
                {
                    throw new Exception();
                }
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format(" delete from measure_data_member  where memberId = '{0}' ", memberId);
                if (cmd.ExecuteNonQuery() < 0)
                {
                    throw new Exception();
                }
                transaction.Commit();
                flag = true;
            }
            catch
            {
                flag = false;
                transaction.Rollback();
            }
            finally
            {
                // 关闭数据库
                if (conn.State == ConnectionState.Open)
                { conn.Close(); }
                conn.Dispose();
                cmd.Dispose();
                transaction.Dispose();
            }
            return flag;
        }
        //保存学生体侧数据
        public bool SavaStudentCheckData(StudentCheckData data)
        {
            OleDbConnection myConn = new OleDbConnection(conStr);
            myConn.Open();
            string strInsert = string.Format(" update measure_data_student  set   checkDate = '{0}', jsonContent = '{1}' where activityId = '{2}' and studentId = '{3}'",data.checkDate,data.jsonContent,data.activityId,data.studentId);
            OleDbCommand cmd = new OleDbCommand(strInsert, myConn);
            int i = cmd.ExecuteNonQuery();
            myConn.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //保存教师体侧数据
        public bool SavaTeacherCheckData(TeacherCheckData data)
        {
            OleDbConnection myConn = new OleDbConnection(conStr);
            myConn.Open();
            string strInsert = string.Format(" INSERT INTO measure_data_member ( Id,memberId ,memberName ,memberAge,memberSex,memberHeight,JsonContent,checkDate,isUpload) VALUES ('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}') ", data.Id, data.memberId, data.memberName, data.memberAge,data.memberSex,data.memberHeight,data.JsonContent,data.checkDate,data.isUpload);
            OleDbCommand inst = new OleDbCommand(strInsert, myConn);
            int i = inst.ExecuteNonQuery();
            myConn.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // 通过学生ID获取学生姓名
        public string GetStudentNameByID(string studentsId)
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = string.Format("select Member.TrueName from  Member where ID='{0}'", studentsId);
            OleDbCommand command = new OleDbCommand(sql, conn);
            conn.Open();
            string res = (string)command.ExecuteScalar();
            return res;
        }
        //通过学生ID获取学生姓名和出生日期
        public Student GetStudentNameAndBirthDayByID(string studentsId)
        {
            Student student = new Student();
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = string.Format("select * from  Member where ID='{0}'", studentsId);
            OleDbCommand command = new OleDbCommand(sql, conn);
            conn.Open();
            OleDbDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    student.studentName = reader.GetValue(2).ToString();
                    student.birthday = reader.GetValue(6).ToString();
                }
                return student;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //插入学生身高
        public int UpdateStudentHeight(string studentId, double height)
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = "update  Member set Height =@height where ID =@studentId";
            conn.Open();
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.AddWithValue("@height", height);
            cmd.Parameters.AddWithValue("@studentId", studentId);
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        //获取学生所有体侧数据
        public List<StudentCheckData> GetCheckerInfo(string studentId)
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = "select * from measure_data_student as a left join Member as b on a.studentId = b.ID where  studentId='"+studentId+"' order by a.checkDate desc";
            List<StudentCheckData> checkDatalist = new List<StudentCheckData>();
            OleDbCommand command = new OleDbCommand(sql, conn);
            conn.Open();
            OleDbDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (!reader.GetValue(6).ToString().Equals(""))
                    {
                        StudentCheckData checkData = new StudentCheckData();
                        string errMsg = "";
                        string backStr = reader.GetValue(6).ToString();
                        checkDataUtill.GetCheckDataByBackStr(backStr, out checkData, out errMsg);
                        checkData.Id = reader.GetValue(0).ToString();
                        checkData.studentName = reader.GetValue(11).ToString();
                        checkData.birthday = reader.GetValue(15).ToString().Split(' ')[0];
                        checkData.activityId = reader.GetValue(1).ToString();
                        checkData.studentId = reader.GetValue(2).ToString();
                        checkData.checkDate = reader.GetValue(5).ToString();
                        checkData.jsonContent = reader.GetValue(6).ToString();
                        checkData.studentId = reader.GetValue(10).ToString();
                        checkDatalist.Add(checkData);
                    }
                }
                return checkDatalist;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //更新年级
        public void UpdateGrade(List<Grade> grades)
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            foreach (Grade grade in grades)
            {
                string checkSql = string.Format("select count(*) from Grade where ID =  '{0}'",grade.id);
                OleDbCommand checkCommand = new OleDbCommand(checkSql, conn);
                int i  = (int)checkCommand.ExecuteScalar();
                if ( i > 0)
                {
                    string sql = string.Format("update Grade  set  gradeIndex = '{0}',gradeName = '{1}'  where ID = '{2}'",grade.gradeIndex, grade.gradeName,grade.id);
                    OleDbCommand command = new OleDbCommand(sql, conn);
                    command.ExecuteNonQuery();
                }
                else
                {
                    string sql = string.Format("insert into Grade(ID,gradeIndex,gradeName) values ('{0}','{1}','{2}')", grade.id, grade.gradeIndex, grade.gradeName);
                    OleDbCommand command = new OleDbCommand(sql, conn);
                    command.ExecuteNonQuery();
                }

            }
            conn.Close();
        }
        //更新班级
        public void UpdateClass(List<Class> classes)
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            foreach (Class class1 in classes)
            {
                if (class1.startGrade.Equals("null") == false)
                {
                    string checkSql = string.Format("select count(*) from Class where ID = '{0}'",class1.id);
                    OleDbCommand checkCommand = new OleDbCommand(checkSql, conn);
                    int i = (int)checkCommand.ExecuteScalar();
                    if (i > 0)
                    {
                        string sql = string.Format("update Class  set  className = '{0}',gradeId = '{1}',classIndex='{2}'  where ID = '{3}'", class1.className, class1.startGrade,class1.classIndex,class1.id);
                        OleDbCommand command = new OleDbCommand(sql, conn);
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        string sql = string.Format("insert into Class(ID,className,gradeId,classIndex,startYear) values ('{0}','{1}','{2}','{3}','{4}')", class1.id, class1.className, class1.startGrade,class1.classIndex,class1.startYear);
                        OleDbCommand command = new OleDbCommand(sql, conn);
                        command.ExecuteNonQuery();
                    }
                }
            }
            conn.Close();
        }
        //插入学生数据
        public void InsertStudentInfo(List<StudentInfo> infos)
        {
            string currentterm = GetCerrentTerm();
            OleDbConnection conn = new OleDbConnection(conStr);
            OleDbCommand cmd = new OleDbCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            try
            {
                foreach (StudentInfo info in infos)
                {
                    string sql = "select count(*) from Member where ID ='"+info.studentCode+"'";
                    cmd.CommandText = sql;
                    int i = (int)cmd.ExecuteScalar();
                    if (i > 0)
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = string.Format("update  Member set TrueName='{0}',Age='{1}',Sex='{2}',Birthday='{3}'  where ID = '{4}'", info.name, info.age, info.sex,info.birthDay,info.studentCode);
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = string.Format("select  a.ID from Class as a  left join  Grade as b on a.gradeId = b.gradeIndex where a.className = '{0}' and b.gradeName= '{1}' and a.startYear='{2}'", info.className, info.gradeName, currentterm);
                        string classId = (string)cmd.ExecuteScalar();
                        cmd.CommandText = string.Format("update b_student_class set classId='{0}' where studentId ='{1}'",classId,info.studentCode);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = string.Format("insert into  Member(NID,ID,TrueName,Age,Sex,Birthday) values ('{0}','{1}','{2}','{3}','{4}','{5}')", info.eduId, info.studentCode, info.name, info.age, info.sex,info.birthDay);
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = string.Format("select  a.ID from Class as a  left join  Grade as b on a.gradeId = b.gradeIndex where a.className = '{0}' and b.gradeName= '{1}' and a.startYear='{2}'", info.className, info.gradeName,currentterm);
                        string classId = (string)cmd.ExecuteScalar();
                        cmd.CommandText = string.Format("insert into b_student_class (studentId,classId) values ('{0}','{1}')", info.studentCode,classId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

            }
            conn.Close();
        }
        //获取教师数据
        public List<Teacher> GetTeacherList()
        {
            List<Teacher> teachers = new List<Teacher>();
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = "select * from  measure_member";
            //获取表的内容
            OleDbCommand oleDb = new OleDbCommand(sql, conn);
            conn.Open();
            OleDbDataReader reader;
            reader = oleDb.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    int sex = int.Parse(reader[3].ToString());
                    string sex_s = "";
                    if (sex == 0)
                    {
                        sex_s = "男";
                    }
                    else
                    {
                        sex_s = "女";
                    }
                    Teacher teacher = new Teacher
                    {
                        memberId = reader[0].ToString(),
                        memberName = reader[1].ToString(),
                        memberSex = sex_s,
                        memberAge = reader[2].ToString().Split(' ')[0],
                        memberHeight = reader[4].ToString(),
                        memberIndex = reader[5].ToString()
                    };
                    teachers.Add(teacher);
                }
                return teachers;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //新增教师
        public bool AddMember(string memberName, string birthday, int membersex, double memberheight)
        {
            string uuid = Guid.NewGuid().ToString();
            OleDbConnection myConn = new OleDbConnection(conStr);
            myConn.Open();
            string strInsert = string.Format(" INSERT INTO measure_member ( Id,memberName, birthday ,memberSex,memberHeight ) VALUES ('{0}','{1}','{2}',{3},{4}) ", uuid, memberName, birthday, membersex, memberheight);
            OleDbCommand cmd = new OleDbCommand(strInsert, myConn);
            int i = cmd.ExecuteNonQuery();
            myConn.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateTerm(Term term)
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            string deleteSql = "delete from b_term";
            OleDbCommand cmd1 = new OleDbCommand(deleteSql, conn);
            cmd1.ExecuteNonQuery();
            string sql= string.Format(" INSERT INTO b_term ( termId,termName,startTime,endTime,isCurTerm) VALUES ('{0}','{1}','{2}',{3},{4}) ",term.termId,term.termName,term.startTime,term.endTime,"1");
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //编辑教师
        public bool EditMember(string birthday, int membersex, double memberheight,string memberId)
        {
            string uuid = Guid.NewGuid().ToString();
            OleDbConnection myConn = new OleDbConnection(conStr);
            myConn.Open();
            string strInsert = string.Format(" update measure_member set  birthday = '{0}',memberSex = {1} ,memberHeight = {2} where Id = '{3}' ",birthday, membersex, memberheight,memberId);
            OleDbCommand cmd = new OleDbCommand(strInsert, myConn);
            int i = cmd.ExecuteNonQuery();
            myConn.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //查询教师
        public List<Teacher> QueryMember(string keyword)
        {
            List<Teacher> teachers = new List<Teacher>();
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = "select * from  measure_member where memberName like '%" + keyword + "%'";
            //获取表的内容
            OleDbCommand oleDb = new OleDbCommand(sql, conn);
            conn.Open();
            OleDbDataReader reader;
            reader = oleDb.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    int sex = int.Parse(reader[3].ToString());
                    string sex_s = "";
                    if (sex == 0)
                    {
                        sex_s = "男";
                    }
                    else
                    {
                        sex_s = "女";
                    }
                    Teacher teacher = new Teacher
                    {
                        memberId = reader[0].ToString(),
                        memberName = reader[1].ToString(),
                        memberSex = sex_s,
                        memberAge = reader[2].ToString().Split(' ')[0],
                        memberHeight = reader[4].ToString(),
                        memberIndex = reader[5].ToString()
                    };
                    teachers.Add(teacher);
                }
                return teachers;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //查询学生
        public List<Student> QueryStudent(string keyword)
        {
            List<Student> students = new List<Student>();
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = "select * from  Member where TrueName like '%" + keyword + "%' order by Member.ID";
            //获取表的内容
            OleDbCommand oleDb = new OleDbCommand(sql, conn);
            conn.Open();
            OleDbDataReader reader;
            reader = oleDb.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Student student = new Student()
                    {
                        studentId = reader.GetValue(1).ToString(),
                        studentName = reader.GetValue(2).ToString(),
                        age = reader.GetValue(3).ToString()
                    };
                    string sex = reader.GetValue(4).ToString();
                    if (sex.Equals("男"))
                    {
                        student.imagePath = "../Resources/checkernan.png";
                        student.sex_string = "男";
                    }
                    else
                    {
                        student.imagePath = "../Resources/checkernv.png";
                        student.sex_string = "女";
                    }
                    students.Add(student);
                }
                return students;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //
        public bool UpdateActivityName(string activityId, string activityName)
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            conn.Open();
            string sql = string.Format("update measure_activity set activityName = '{0}'  where ID='{1}'", activityName, activityId);
            OleDbCommand command = new OleDbCommand(sql, conn);
            int i = command.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //获取图片保存路径
        public string GetImageUrl()
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = "select * from device_setting";
            string s = "";
            //获取表的内容
            OleDbCommand oleDb = new OleDbCommand(sql, conn);
            conn.Open();
            OleDbDataReader reader;
            reader = oleDb.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    s = reader.GetValue(1).ToString();
                }
                return s;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        //获取当前学期
        public string GetCerrentTerm()
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = "select termName from b_term where isCurTerm = '1'";
            //获取表的内容
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            conn.Open();
            string res = (string)cmd.ExecuteScalar();
            return res;
        }
        //获取图片保存路径
        public int UpdateImageUrl(string url)
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = string.Format("update  device_setting  set imageUrl = '{0}'  where ID=  1",url);
            //获取表的内容
            OleDbCommand oleDb = new OleDbCommand(sql, conn);
            conn.Open();
            int i = oleDb.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        public int QueryStudentCount()
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = "select count(*) from Member";
            //获取表的内容
            OleDbCommand cmd= new OleDbCommand(sql, conn);
            conn.Open();
            int i = (int)cmd.ExecuteScalar();
            conn.Close();
            return i;
        }
        //修改体侧数据
        public int EditStudentCheckInfo(string activityId, string studentId,string jsonContent)
        {
            OleDbConnection conn = new OleDbConnection(conStr);
            string sql = string.Format("update measure_data_student set jsonContent='{0}' where activityId='{1}' and studentId='{2}'",jsonContent,activityId,studentId);
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        //查询同步时间
        public string GetSynchronizeTime()
        {
            using (OleDbConnection conn = new OleDbConnection(conStr))
            {
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select synchronizeTime from b_synchronize_record";
                    string time = (string)cmd.ExecuteScalar();
                    return time;
                }
            }
        }
        public int SaveSynchronizeTime()
        {
            using (OleDbConnection conn = new OleDbConnection(conStr))
            {
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "update b_synchronize_record set synchronizeTime ='" + DateTime.Now.ToString() + "' where id = 1";
                    int i = cmd.ExecuteNonQuery();
                    conn.Close();
                    return i;
                }
            }
        }
    }
}
