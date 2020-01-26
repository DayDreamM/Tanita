using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using Tanita.model;

namespace Tanita
{
    /// <summary>
    /// AddActivityWin.xaml 的交互逻辑
    /// </summary>
    public partial class AddActivityWin : MetroWindow
    {
        public AddActivityWin()
        {
            InitializeComponent();
        }
        //新建
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            DbUtill dbUtill = new DbUtill();
            string activityName = txtUserName.Text;
            if (activityName.Trim().Length < 1)
            {
                popup1.IsOpen = true;
            }
            else if (activityName.Trim().Length > 15)
            {
                popup2.IsOpen = true;
            }
            else
            {
                MyCustomControlLibrary.MMessageBox.ShowLoading(MyCustomControlLibrary.MMessageBox.LoadType.Grid, "新建中...", new Point(0, 0), new Size(0, 0), "&#xe752;", Orientation.Vertical, "#ffffff", 10);
                btnLogin.IsEnabled = false;
                Activity activity = new Activity
                {
                    ActivityName = activityName,
                    isFile = 0,
                    activityDate = DateTime.Now.ToString()
                };
                try
                {
                    dbUtill.AddActivity(activity);
                    DialogResult = true;
                    MyCustomControlLibrary.MMessageBox.MClosed();
                    MessageBox.Show("新建成功", "提示");
                    Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("新建失败", "提示");
                    Close();
                }
            }
        }
    }
}
