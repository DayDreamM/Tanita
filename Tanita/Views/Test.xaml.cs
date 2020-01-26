using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Tanita.model;
using Tanita.Utill;

namespace Tanita.Views
{
    /// <summary>
    /// test.xaml 的交互逻辑
    /// </summary>
    public partial class Test : MetroWindow
    {
        public string activityId{ get; set; }
        public  static  string stnId { get; set; }
        public string ClassId { get; set; }
        public int ItemCount { get; set; }
        DbUtill dbUtill = new DbUtill();
        MeasureCommand mc = new MeasureCommand();
        public Test()
        {
            InitializeComponent();
            PortConfig.serialPort1.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);
        }
        public Test(string GradeId,string ClassId,string activityId,int index)
        {
            InitializeComponent();
            Init();
            List<Student> students = dbUtill.GetCheckerListByClassIdOnTest(ClassId,activityId);
            int i = dbUtill.GetCountByClassIdOnTest(ClassId, activityId);
            string gradeName = dbUtill.getGradeNameByGradeId(GradeId);
            string className = dbUtill.getClassNameByClassId(ClassId);
            stnPanel.ItemsSource = students;
            int count = stnPanel.Items.Count;
            if (count > 0)
            {
                if (index > 0 && index < count)
                {
                    stnPanel.SelectedIndex = index;
                }
                else
                {
                    stnPanel.SelectedIndex = 0;
                }
            }
            PortConfig.serialPort1.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);
            display_class.Text = "  "+gradeName + className+" "+"待检测学生";
            ItemCount = stnPanel.Items.Count;
        }
        //体侧
        private void CheckBtn_Click(object sender, RoutedEventArgs e)
        {
            if (stu_height.Text.Length < 1)
            {
                popup5.IsOpen = true;
            }
            else
            {
                Student student = stnPanel.SelectedItem as Student;
                string sex = (student.sex + 1).ToString();
                string height = "";
                string age = "";
                if (student.age.Length > 2)
                {
                    age = student.age;
                }
                else
                {
                    age = "0" + student.age;
                }
                if (stu_height.Text != null & !stu_height.Text.Equals(""))
                {
                    if (stu_height.Text.IndexOf('.') > -1)
                    {
                        height = stu_height.Text;
                    }
                    else
                    {
                        height = stu_height.Text + ".0";
                    }
                    mc.Measure(PortConfig.serialPort1, Port.modelId, sex, age, height);
                }
            }
        }
        //接收数据
        private void Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (PortConfig.serialPort1.IsOpen)
            {
                string backStr = PortConfig.serialPort1.ReadExisting();
                if (backStr.Length < 30)
                {
                    if (backStr.Trim() == "@")
                    {
                        Dispatcher.Invoke(new Action(() =>
                        {
                            CheckBtn.Background = System.Windows.Media.Brushes.Orange;
                            CheckBtn.Content = "正在设置数据";
                        }));
                    }
                    else if (backStr.Trim() == "S6")
                    {
                        Dispatcher.Invoke(new Action(() =>
                        {
                            CheckBtn.Content = "检测中......";
                        }));
                    }
                }
                if (backStr.Length > 30)
                {
                    if (backStr.Substring(0, 1) == "{")
                    {
                        StudentCheckData data = new StudentCheckData
                        {
                            activityId = activityId,
                            studentId = stnId,
                            checkDate = DateTime.Now.ToString(),
                            jsonContent = backStr
                        };
                        bool  isSave = dbUtill.SavaStudentCheckData(data);
                        if (isSave)
                        {
                            MessageBox.Show("检测完毕,请离开体测仪", "提示");
                        }
                        else
                        {
                            MessageBox.Show("保存失败,请离开体测仪", "提示");
                        }
                        Dispatcher.Invoke(new Action(() =>
                        {
                            CheckBtn.Content = "开始检测";
                        }));
                    }
                }
            }
            else
            {
                MessageBox.Show("串口未打开", "提示");
            }
        }
        //上一页按钮
        private void LastBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = stnPanel.SelectedIndex;
            if (index - 1 != -1)
            {
                stnPanel.SelectedIndex = index - 1;
            }
            else
            {
                MessageBox.Show("当前为第一项","提示");
            }
        }
        //下一页按钮
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = stnPanel.SelectedIndex;
            if (index + 1 < ItemCount)
            {
                stnPanel.SelectedIndex = index + 1;
            }
            else
            {
                MessageBox.Show("当前为最后一项","提示");
            }
        }
        //选择
        private void StnPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             Student student = stnPanel.SelectedItem as Student;
             display_name.Text = student.studentName;
            string sex_s = null;
            if(student.sex_string.Equals("男"))
            {
                sex_s = "男";
                display.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/nan.png"));
            }
            else
            {
                sex_s = "女";
                display.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/nv.png"));
            }
            display_sex.Text = sex_s + " " + student.age+"岁";
            display_age.Text = student.studentId;
            stnId = student.studentId;
            stu_height.Text = student.height;
        }
        private void Init()
        {
            if (PortConfig.IsOpen)
            {
                CheckBtn.Background = System.Windows.Media.Brushes.Green;
                CheckBtn.Foreground = System.Windows.Media.Brushes.White;
                CheckBtn.Content = "开始检测";
                CheckBtn.IsEnabled = true;
            }
        }
    }
}
