using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Tanita.model;
using Tanita.Utill;

namespace Tanita.Views
{
    /// <summary>
    /// TestActivityPage.xaml 的交互逻辑
    /// </summary>
    public partial class TestActivityPage : Page
    {
        DbUtill dbUtill = new DbUtill();
        CheckDataUtill checkDataUtill = new CheckDataUtill();
        public IsFileWin isFileWin { get; set; }
        public string GradeId { get; set; }
        public string ClassId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityId { get; set; }
        public TestActivityPage()
        {
            InitializeComponent();
        }
        public TestActivityPage(string activityName, string activityId)
        {
            InitializeComponent();
            TestActivityInit(activityName, activityId);
        }
        //初始化
        private void TestActivityInit(string activityName, string activityId)
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
            isFileWin.ContentBack();
        }
        //年级初始化
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
                if (i == 0)
                {
                    Button button = new Button
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
                    Button button = new Button
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
        //年级
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            foreach (Button button in gradepanel.Children)
            {
                if (btn.Uid == button.Uid)
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
                    Button button = new Button
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
                    Button button = new Button
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
        //显示 学生名单
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            foreach (Button button in panel.Children)
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
            Student student = StdDataGrid.SelectedItem as Student;
            if (student != null)
            {
                Report rp = new Report();
                string studentId = student.studentId;
                string activityId = Uid;
                StudentCheckData checkData = dbUtill.GetStudentCheckInfo(activityId, studentId);
                if (checkData != null)
                {
                    if (checkData.jsonContent != null)
                    {
                        rp.checkData = checkData;
                        rp.PrintService();
                    }
                    else
                    {
                        MessageBox.Show("体侧数据不存在", "错误");
                    }
                }
                else
                {
                    MessageBox.Show("体检报告不存在", "错误");
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
                    MessageBox.Show("该学生体侧数据未存在", "错误");
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
    }
}
