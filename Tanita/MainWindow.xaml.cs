using MahApps.Metro.Controls;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Tanita.DAL;
using Tanita.Utill;
using Tanita.Views;
using Tanita.Views.Menu;
using Tanita.windows;
using System;

namespace Tanita
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        DbUtill dbUtill = new DbUtill();
        RequestUtill requestUtill = new RequestUtill();
        CommonUtill commonUtill = new CommonUtill();
        MainPage mainPage = new MainPage();
        ActivtyPage activtyPage = new ActivtyPage();
        CheckerPage checkerPage = new CheckerPage();
        TeacherPage teacherPage = new TeacherPage();
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(string username)
        {
            InitializeComponent();
            PortConfig.IsOpen = false;
            ContentControl.Content = new Frame()
            {
                Content = mainPage
            };
            welcomeBtn.Content = "你好，"+username+"，登陆时间："+DateTime.Now.ToString();
            DataTable dt = dbUtill.GetGradeinfo();
        }
        //重设密码
        private void EditPassword_Click(object sender, RoutedEventArgs e)
        {
            EditPassword edit = new EditPassword();
            edit.ShowDialog();
        }
        //关于
        private void About_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }
        private void SystemSetting_Click(object sender, RoutedEventArgs e)
        {
            SystemSetting ss = new SystemSetting();
            ss.ShowDialog();
        }
        private void ConnectBtn_Click(object sender, RoutedEventArgs e)
        {
            Connection connWin = new Connection();
            connWin.ShowDialog();
            if (connWin.DialogResult == true)
            {
                connStute.Text = "设备已连接";
            }
        }
        //检测活动
        private void CheckActivityBtn_Click(object sender, RoutedEventArgs e)
        {
            activtyPage.mainWindow = this;
            ContentControl.Content = new Frame()
            {
                Content = activtyPage
            };
        }
        //被检测人员
        private void StudentsBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new Frame()
            {
                Content = checkerPage
            };
        }
        //数校同步按钮
        private void Sync_btn_Click(object sender, RoutedEventArgs e)
        {
            bool flag = requestUtill.SyncSystemData();
            if(flag == true)
            {
                dbUtill.SaveSynchronizeTime();
                MessageBox.Show("同步成功", "提示");
                checkerPage.LastTimeInit();
            }
            else
            {
                MessageBox.Show("同步失败,请与技术人员联系", "提示");
            }
        }
        private void TeacherPage_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new Frame()
            {
                Content = teacherPage
            };
        }
        public void ContentChange(string activtyId,string activityName)
        {
            TestActivity testActivity = new TestActivity(activityName,activtyId)
            {
                mainWindow = this,
                Uid = activtyId,
                ActivityId = activtyId,
                ActivityName = activityName
            };
            ContentControl.Content = new Frame()
            {
                Content = testActivity
            };
        }
        public void ContentBack()
        {
            ContentControl.Content = new Frame()
            {
                Content = activtyPage
            };
        }
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Button_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Connection connWin = new Connection();
            connWin.ShowDialog();
            if(connWin.DialogResult == true)
            {
                connStute.Text = "设备已连接";
            }
        }

        private void CommoentSettingBtn_Click(object sender, RoutedEventArgs e)
        {
            commentSetting setting = new commentSetting();
            setting.ShowDialog();
        }
    }
}