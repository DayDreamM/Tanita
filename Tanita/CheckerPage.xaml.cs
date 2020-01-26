using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Tanita.DAL;
using Tanita.model;
using Tanita.Utill;
using Tanita.Views;

namespace Tanita
{
    /// <summary>
    /// CheckerPage.xaml 的交互逻辑
    /// </summary>
    public partial class CheckerPage : Page
    {
        DbUtill dbUtill = new DbUtill();
        RequestUtill requestUtill = new RequestUtill();
        JsonUtill JsonUtill = new JsonUtill();
        CommonUtill commonUtill = new CommonUtill();
        public CheckerPage()
        {
            InitializeComponent();
            UIInit();
            LastTimeInit();
        }
        public void LastTimeInit()
        {
            lastTime.Text = "学生数据同步时间："+dbUtill.GetSynchronizeTime();
        }
        public void UIInit()
        {
            DataTable dt = dbUtill.GetGradeinfo();
            IniiTableMenu(dt);
            string GradeId = dbUtill.GetGradeNO1();
            DataTable dt2 = dbUtill.GetClassInfo(GradeId);
            string ClassId = dbUtill.GetClassNO1(GradeId);
            IniiTableClass(dt2);
            ListInit(ClassId);
        }
        //年级初始化
        private void IniiTableMenu(DataTable dt)
        {
            Color color = (Color)ColorConverter.ConvertFromString("#FFFFFF");
            Color color2 = (Color)ColorConverter.ConvertFromString("#000000");
            Color color3 = (Color)ColorConverter.ConvertFromString("#CCCCCC");
            gradepanel.Children.Clear();
            int[] gen = new int[dt.Rows.Count];
            for (int i = 0; i < gen.Length; i++)
            {
                if (i == 0)
                {
                    Button button = new Button
                    {
                        Background = new SolidColorBrush(color),
                        Foreground = new SolidColorBrush(color2)
                    };
                    Thickness thickness = new Thickness(2);
                    button.BorderThickness = thickness;
                    button.BorderBrush = new SolidColorBrush(color2);
                    Thickness thickness2 = new Thickness(8, 0, 0, 0);
                    button.Content = dt.Rows[i][2].ToString();
                    button.Uid = dt.Rows[i][1].ToString();
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
                        Background = new SolidColorBrush(color),
                        Foreground = new SolidColorBrush(color2),
                        BorderBrush = new SolidColorBrush(color3),
                    };
                    button.BorderThickness = new Thickness(2);
                    button.Margin = new Thickness(8, 0, 0, 0);
                    button.Content = dt.Rows[i][2].ToString();
                    button.Uid = dt.Rows[i][1].ToString();
                    button.Name = "button" + i;
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
                    string classIndex = btn.Uid;
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
                        Margin = new Thickness(8, 0, 0, 0),
                        Uid = dt.Rows[i][0].ToString(),
                        Content = dt.Rows[i][1].ToString()
                    };
                    panel.Children.Add(button);
                    button.Click += Button_Click1;
                    button.FontSize = 12;
                }
            }
        }
        // 班级按钮显示学生名单
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            foreach (Button button in panel.Children)
            {
                if (btn.Uid == button.Uid)
                {
                    string classId = button.Uid;
                    List<Student> students = dbUtill.GetCheckerListByClassId(classId);
                    itemsCtrl.ItemsSource = students;
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
        //学生列表初始化
        private void ListInit(string classId)
        {
            List<Student> students = dbUtill.GetCheckerListByClassId(classId);
            itemsCtrl.ItemsSource = students;
        }
        //选中
        private void ItemsCtrl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (itemsCtrl.SelectedItem is Student student)
            {
                StudentCheckDetail checkDetail = new StudentCheckDetail(student.studentId);
                checkDetail.ShowDialog();
                if(checkDetail.DialogResult == false)
                {
                    itemsCtrl.SelectedItem = null;
                }
            }
        }
        //同步学生数据
        private void UpdateStuBtn_Click(object sender, RoutedEventArgs e)
        {
            MyCustomControlLibrary.MMessageBox.ShowLoading(MyCustomControlLibrary.MMessageBox.LoadType.Circle, "正在同步", new Point(0, 0), new Size(0, 0), "&#xe752;", Orientation.Vertical, "#ffffff", 3);
            bool flag = requestUtill.SyncSystemData();
            if(flag == true)
            {
                MyCustomControlLibrary.MMessageBox.MClosed();
                dbUtill.SaveSynchronizeTime();
                MessageBox.Show("同步成功", "提示");
                UIInit();
                LastTimeInit();
            }
            else
            {
                MessageBox.Show("同步失败,请与技术人员联系", "提示");
            }
        }

        private void QueryBtn_Click(object sender, RoutedEventArgs e)
        {
            itemsCtrl.ItemsSource = dbUtill.QueryStudent(Keyword.Text);
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            UIInit();
            Keyword.Text = "";
        }
    }
}
