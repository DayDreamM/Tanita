using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using Tanita.DAL;
using Tanita.model;
using Tanita.Utill;
using Tanita.Views;

namespace Tanita.windows
{
    /// <summary>
    /// testActivity.xaml 的交互逻辑
    /// </summary>
    public partial class TestActivity : Page
    {
        DbUtill dbUtill = new DbUtill();
        CheckDataUtill checkDataUtill = new CheckDataUtill();
        RequestUtill requestUtill = new RequestUtill();
        Report rp = new Report();
        public MainWindow mainWindow { get; set; }
        public string GradeId { get; set; }
        public string ClassId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityId { get; set; }
        public TestActivity()
        {
            InitializeComponent();
        }
        public TestActivity(string activityName,string activityId)
        {
            InitializeComponent();
            TestActivityInit(activityName,activityId);
        }
        //初始化
        private void TestActivityInit(string activityName,string activityId)
        {
            DataTable dt = dbUtill.GetGradeinfo();
            IniiTableMenu(dt);
            GradeId = dbUtill.GetGradeNO1();
            DataTable dt2 = dbUtill.GetClassInfo(GradeId);
            ClassId = dbUtill.GetClassNO1(GradeId);
            IniiTableClass(dt2);
            ActivityTitle.Text = activityName + "  学生体测数据";
            List<Student> students = dbUtill.GetStudentListByClassIdAndActivityId(ClassId, activityId);
            StdDataGrid.ItemsSource = students;
        }
        //关闭
        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ContentBack();
        }
        //初始化年级列表
        private void IniiTableMenu(DataTable dt)
        {
            Color white = (Color)ColorConverter.ConvertFromString("#FFFFFF");
            Color black = (Color)ColorConverter.ConvertFromString("#000000");
            Color gray = (Color)ColorConverter.ConvertFromString("#CCCCCC");
            Thickness thickness = new Thickness(2);
            Thickness thickness2 = new Thickness(8, 0, 0, 0);
            int[] gen = new int[dt.Rows.Count];
            for (int i = 0; i < gen.Length; i++)
            {
                if(i == 0)
                {
                    System.Windows.Controls.Button button = new System.Windows.Controls.Button
                    {
                        Background = new SolidColorBrush(white),
                        Foreground = new SolidColorBrush(black)
                    };
                    button.BorderThickness = thickness;
                    button.BorderBrush = new SolidColorBrush(black);
                    button.Uid = dt.Rows[i][1].ToString();
                    button.Content = dt.Rows[i][2].ToString();
                    button.Name = "button" + i;
                    button.Margin = thickness2;
                    gradepanel.Children.Add(button);
                    button.Click += Button_Click;
                    button.FontSize = 12;
                }
                else
                {
                    System.Windows.Controls.Button button = new System.Windows.Controls.Button
                    {
                        Background = new SolidColorBrush(white),
                        Foreground = new SolidColorBrush(black)
                    };
                    button.BorderThickness = thickness;
                    button.BorderBrush = new SolidColorBrush(gray);
                    button.Uid = dt.Rows[i][1].ToString();
                    button.Content = dt.Rows[i][2].ToString();
                    button.Name = "button" + i;
                    button.Margin = thickness2;
                    gradepanel.Children.Add(button);
                    button.Click += Button_Click;
                    button.FontSize = 12;
                }
            }
        }
         public  void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;
            foreach (System.Windows.Controls.Button button in gradepanel.Children)
            {
                 if(btn.Uid == button.Uid)
                {
                    Color color = (Color)ColorConverter.ConvertFromString("#FFFFFF");
                    Color color2 = (Color)ColorConverter.ConvertFromString("#000000");
                    button.Background = new SolidColorBrush(color);
                    button.Foreground = new SolidColorBrush(color2);
                    Thickness thickness = new Thickness(2);
                    button.BorderThickness = thickness;
                    button.BorderBrush = new SolidColorBrush(color2);
                    string classIndex = btn.Uid.ToString();
                    DataTable dt = dbUtill.GetClassInfo(classIndex);
                    panel.Children.Clear();
                    IniiTableClass(dt);
                }
                else
                {
                    Color color = (Color)ColorConverter.ConvertFromString("#FFFFFF");
                    Color color2 = (Color)ColorConverter.ConvertFromString("#000000");
                    Color color3 = (Color)ColorConverter.ConvertFromString("#CCCCCC");
                    button.Background = new SolidColorBrush(color);
                    button.Foreground = new SolidColorBrush(color2);
                    Thickness thickness = new Thickness(2);
                    button.BorderThickness = thickness;
                    button.BorderBrush = new SolidColorBrush(color3);
                }
                GradeId = btn.Uid;
            }
        }
        //开始检测按钮
        private void Test_btn_Click(object sender, RoutedEventArgs e)
        {
            int index = StdDataGrid.SelectedIndex;
            if (GradeId == null || ClassId == null)
            {
                System.Windows.MessageBox.Show("请先选择要检测的班级","错误");
            }
            else
            {
                Test test = new Test(GradeId,ClassId,Uid,index)
                {
                    activityId = Uid
                };
                test.ShowDialog();
                if (test.DialogResult == false)
                {
                   List<Student> students = dbUtill.GetStudentListByClassIdAndActivityId(ClassId, ActivityId);
                   StdDataGrid.ItemsSource = students;
                }
            }
        }
        //班级
        private void IniiTableClass(DataTable dt)
        {
            Color color = (Color)ColorConverter.ConvertFromString("#FFFFFF");
            Color color2 = (Color)ColorConverter.ConvertFromString("#000000");
            Color color3 = (Color)ColorConverter.ConvertFromString("#CCCCCC");
            panel.Children.Clear();
            int[] gen = new int[dt.Rows.Count];
            for (int i = 0; i < gen.Length; i++)
            {
                if (i == 0)
                {
                    System.Windows.Controls.Button button = new System.Windows.Controls.Button
                    {
                        Background = new SolidColorBrush(color),
                        Foreground = new SolidColorBrush(color2),
                        BorderBrush = new SolidColorBrush(color2),
                        BorderThickness = new Thickness(2),
                        Uid = dt.Rows[i][0].ToString(),
                        Content = dt.Rows[i][1].ToString(),
                        Margin = new Thickness(8, 0, 0, 0)
                    };
                    panel.Children.Add(button);
                    button.Click += Button_Click1;
                    button.FontSize = 12;
                }
                else
                {
                    System.Windows.Controls.Button button = new System.Windows.Controls.Button
                    {
                        Background = new SolidColorBrush(color),
                        Foreground = new SolidColorBrush(color2),
                        BorderBrush = new SolidColorBrush(color3),
                        BorderThickness = new Thickness(2),
                        Uid = dt.Rows[i][0].ToString(),
                        Content = dt.Rows[i][1].ToString(),
                        Margin = new Thickness(8, 0, 0, 0),
                    };
                    panel.Children.Add(button);
                    button.Click += Button_Click1;
                    button.FontSize = 12;
                }
            }
            }
        //班级显示学生名单
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;
            foreach (System.Windows.Controls.Button button in panel.Children)
            {
                if (btn.Uid == button.Uid)
                {
                    ClassId = btn.Uid;
                    string classId = btn.Uid;
                    List<Student> students = dbUtill.GetStudentListByClassIdAndActivityId(classId, ActivityId);
                    StdDataGrid.ItemsSource = students;
                }
                else
                {
                    Color color = (Color)ColorConverter.ConvertFromString("#FFFFFF");
                    Color color2 = (Color)ColorConverter.ConvertFromString("#000000");
                    Color color3 = (Color)ColorConverter.ConvertFromString("#CCCCCC");
                    button.Background = new SolidColorBrush(color);
                    button.Foreground = new SolidColorBrush(color2);
                    button.BorderBrush = new SolidColorBrush(color3);
                    Thickness thickness = new Thickness(2);
                    button.BorderThickness = thickness;
                }
            }
        }
        //打印报告
        private void PrintCheck_Click(object sender, RoutedEventArgs e)
        {
            if (StdDataGrid.SelectedItem is Student student)
            {
                Report rp = new Report();
                string studentId = student.studentId;
                string activityId = Uid;
                StudentCheckData checkData = dbUtill.GetStudentCheckInfo(activityId, studentId);
                if (checkData != null)
                {
                    if (checkData.jsonContent!=null)
                    {
                        rp.checkData = checkData;
                        rp.PrintService();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("体测数据不存在", "错误");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("体测数据不存在", "错误");
                }
            }
        }
        //详情
        private void CheckInfo_Click(object sender, RoutedEventArgs e)
        {
            if (StdDataGrid.SelectedItem is Student student)
            {
                StudentDetail studentDetail = new StudentDetail();
                string studentId = student.studentId;
                string activityId = Uid;
                StudentCheckData checkData = dbUtill.GetStudentCheckInfo(activityId, studentId);
                if (checkData.jsonContent == null || checkData.jsonContent.Equals(""))
                {
                    System.Windows.MessageBox.Show("体测数据未存在", "错误");
                }
                else
                {
                    Dictionary<string, string> dic = checkDataUtill.GetDicByBackStr(checkData.jsonContent);
                    Student student1 = dbUtill.GetStudentNameAndBirthDayByID(studentId);
                    studentDetail.name.Text = student1.studentName;
                    studentDetail.age.Text = dic["AG"];
                    studentDetail.date.Text = checkData.checkDate;
                    studentDetail.birthday.Text = student1.birthday.Split(' ')[0];
                    if (dic["GE"].Equals("0"))
                    {
                        studentDetail.sex.Text = "男";
                    }
                    else
                    {
                        studentDetail.sex.Text = "女";
                    }
                    studentDetail.weight.Text = dic["Wk"];
                    studentDetail.Pt.Text = dic["Pt"];
                    studentDetail.FW.Text = dic["FW"];
                    studentDetail.fW.Text = dic["fW"];
                    studentDetail.wW.Text = dic["wW"];
                    studentDetail.MW.Text = dic["MW"];
                    studentDetail.mW.Text = dic["mW"];
                    studentDetail.height.Text = dic["Hm"];
                    studentDetail.BMI.Text = dic["MI"];
                    studentDetail.rB.Text = dic["rB"];
                    studentDetail.bW.Text = dic["bW"];
                    studentDetail.ShowDialog();
                }
            }
        }
        //批量导入身高
        private void BatchInputHeight_Click(object sender, RoutedEventArgs e)
        {
            string acitivityId = Uid;
            CheckDataUtill checkDataUtill = new CheckDataUtill();
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "请选择批量导入文件",
                Filter = "excel文件(*.xlsx)|*.xlsx|所有文件(*.*)|*.*"
            };
            if (ofd.ShowDialog()== DialogResult.OK)
            {
                string path = Path.GetFullPath(ofd.FileName);
                DataTable dt = checkDataUtill.DBExcelToDataTable(path, "Sheet1");
                int errcount = 0;
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string studentId = dt.Rows[i][0].ToString();
                        string height = dt.Rows[i][2].ToString();
                        double height_int = double.Parse(height);
                        int count = dbUtill.UpdateStudentHeight(studentId, height_int);
                        if (count < 1)
                        {
                            System.Windows.MessageBox.Show("导入失败,不存在学号为" + studentId + "的学生", "错误");
                            errcount = ++errcount;
                        }
                    }
                        if (errcount == 0)
                        {
                            System.Windows.MessageBox.Show("全部导入成功", "成功");
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("导入失败"+errcount.ToString()+"条", "错误");
                        }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("批量导入失败,因为"+ex.Message, "错误");
                }
            }
        }
        //批量导出报告
        private void BatchOutputReport_Click(object sender, RoutedEventArgs e)
        {
            MyCustomControlLibrary.MMessageBox.ShowLoading(MyCustomControlLibrary.MMessageBox.LoadType.Circle, "正在打印", new Point(0, 0), new Size(0, 0), "&#xe62e;", System.Windows.Controls.Orientation.Vertical, "#ffffff", 3);
            string activityId = Uid;
            Report rp = new Report();
            List<StudentCheckData> studentCheckDatas = dbUtill.GetStudentCheckDatasByActivityId(activityId);
            foreach(StudentCheckData studentCheckData in studentCheckDatas)
            {
                if (studentCheckData.jsonContent.Trim().Length>1)
                {
                    rp.checkData = studentCheckData;
                    rp.PrintServiceNotPreview();
                }
            }
            MyCustomControlLibrary.MMessageBox.MClosed();
            System.Windows.MessageBox.Show("打印完毕","提示");
        }
       //上传
        private void UploadBtn_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("是否上传检测记录?", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                string activityId = Uid;
                List<StudentCheckData> studentCheckDatas = dbUtill.GetUploadDatasByActivityId(activityId);
                if (studentCheckDatas.Count > 0)
                {
                    MyCustomControlLibrary.MMessageBox.ShowLoading(MyCustomControlLibrary.MMessageBox.LoadType.Circle, "正在上传", new Point(0, 0), new Size(0, 0), "&#xe62e;", System.Windows.Controls.Orientation.Vertical, "#ffffff", 3);
                    string url = dbUtill.GetImageUrl();
                    string uploadUrl = ConfigurationManager.AppSettings["UploadUrl"];
                    string schoolId = ConfigurationManager.AppSettings["schoolId"];
                    Activity activity = dbUtill.GetActivitieInfoByID(ActivityId);
                    foreach (StudentCheckData student in studentCheckDatas)
                    {
                        string res = dbUtill.GetStudentInfoByStudentId(student.studentId);
                        string studentName = dbUtill.GetStudentNameByID(student.studentId);
                        string[] resArray = res.Split(',');
                        string gradeName = resArray[1];
                        string className = resArray[0];
                        string path = @url + "\\" + ActivityName + "\\" + gradeName + "\\" + className + "\\" + student.studentId + "_" + studentName + ".jpg";
                        if (!File.Exists(path))
                        {
                            ImageInfo imageInfo = CommonUtill.ImageFileInfo(path);
                            if (student.jsonContent.Trim().Length > 1)
                            {
                                string param = "{\"student_educode\":\"" + student.NID + "\",\"activity_code\":\"" + activityId + "\",\"student_number\":\"" + student.studentId + "\",\"image\":[{\"image_size\":" + imageInfo.image_size + ",\"image_code\":" + imageInfo.image_code + ",\"fileName\":" + imageInfo.fileName + ",\"image_time\":" + imageInfo.image_time + "}],\"target_code\":\"7\", \"school_code\": \"" + schoolId + "\",\"datas\": { \"age\": " + student.studentAge + ",\"height\": " + student.studentHeight + ",\"checkDate\": \"" + student.checkDate + "\",\"checkResult\":\"" + student.jsonContent + "\"}},\"activityName\": \"" + activity.ActivityName + "\",\"activityDate\": \"" + activity.activityDate + "\"}";
                                string Res = requestUtill.PostRequestClient(uploadUrl, param);
                            }
                        }
                        else
                        {
                            rp.checkData = student;
                            rp.CreateMC780_boy();
                            ImageInfo imageInfo = CommonUtill.ImageFileInfo(path);
                            if (student.jsonContent.Trim().Length > 1)
                            {
                                string param = "{\"student_educode\":\"" + student.NID + "\",\"activity_code\":\"" + activityId + "\",\"student_number\":\"" + student.studentId + "\",\"image\":[{\"image_size\":" + imageInfo.image_size + ",\"image_code\":" + imageInfo.image_code + ",\"fileName\":" + imageInfo.fileName + ",\"image_time\":" + imageInfo.image_time + "}],\"target_code\":\"7\", \"school_code\": \"" + schoolId + "\",\"datas\": { \"age\": " + student.studentAge + ",\"height\": " + student.studentHeight + ",\"checkDate\": \"" + student.checkDate + "\",\"checkResult\":\"" + student.jsonContent + "\"}},\"activityName\": \"" + activity.ActivityName + "\",\"activityDate\": \"" + activity.activityDate + "\"}";
                                string Res = requestUtill.PostRequestClient(uploadUrl, param);
                                Console.WriteLine(Res);
                            }
                        }
                    }
                    MyCustomControlLibrary.MMessageBox.MClosed();
                    System.Windows.MessageBox.Show("上传完毕","提示");
                }
                else
                {
                    System.Windows.MessageBox.Show("该活动不存在已检测的数据，请检测后再试","提示");
                }
            }
        }
        //批量导出检测报告
        private void BatchSaveReport_Click(object sender, RoutedEventArgs e)
        {
            string activityId = Uid;
            string url = dbUtill.GetImageUrl();
            List<StudentCheckData> studentCheckDatas = dbUtill.GetStudentCheckDatasByActivityId(activityId);
            if (studentCheckDatas.Count > 0)
            {
                MyCustomControlLibrary.MMessageBox.ShowLoading(MyCustomControlLibrary.MMessageBox.LoadType.Circle, "正在导出", new Point(0, 0), new Size(0, 0), "&#xe62e;", System.Windows.Controls.Orientation.Vertical, "#ffffff", 3);
                foreach (StudentCheckData studentCheckData in studentCheckDatas)
                {
                        rp.checkData = studentCheckData;
                        rp.CreateMC780_boy();
                }
                MyCustomControlLibrary.MMessageBox.MClosed();
                System.Windows.MessageBox.Show("导出完毕,已保存至" + url,"提示");
            }
            else
            {
                MyCustomControlLibrary.MMessageBox.MClosed();
                System.Windows.MessageBox.Show("提示", "无可用导出的检测报告");
            }
        }
        //删除检测结果
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Student student = StdDataGrid.SelectedItem as Student;
            StudentCheckData checkData = dbUtill.GetStudentCheckInfo(Uid,student.studentId);
            if (checkData.jsonContent != null)
            {
                if (System.Windows.MessageBox.Show("是否删除检测记录?", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    int i = dbUtill.DeleteCheckData(Uid, student.studentId);
                    if (i > 0)
                    {
                        gradepanel.Children.Clear();
                        TestActivityInit(ActivityName, ActivityId);
                        System.Windows.MessageBox.Show("删除成功", "提示");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("删除失败", "提示");
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("该学生不存在检测结果","提示");
            }
        }
        //修改检测结果
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (StdDataGrid.SelectedItem is Student student)
            {
                StudentDetail studentDetail = new StudentDetail();
                string studentId = student.studentId;
                string activityId = Uid;
                StudentCheckData checkData = dbUtill.GetStudentCheckInfo(activityId, studentId);
                if (checkData.jsonContent == null || checkData.jsonContent.Equals(""))
                {
                    System.Windows.MessageBox.Show("体测数据不存在", "错误");
                }
                else
                {
                    EditStudentCheckDetail EditStudentDetail = new EditStudentCheckDetail(activityId, studentId)
                    {
                        activityId = activityId,
                        studentId = studentId
                    };
                    EditStudentDetail.ShowDialog();
                }
            }
        }

        private void DownLorddBtn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                FileInfo sourceFile = new FileInfo(@".\model\学生身高批量导入模板.xlsx");
                sourceFile.CopyTo(folder.SelectedPath + @"\学生身高批量导入模板.xlsx");
                System.Windows.MessageBox.Show("下载完成，已保存至" + folder.SelectedPath + @"\学生身高批量导入模板.xlsx","提示");
            }
        }
    }
}
