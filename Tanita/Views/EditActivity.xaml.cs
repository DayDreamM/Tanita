using MahApps.Metro.Controls;
using System.Windows;

namespace Tanita.Views
{
    /// <summary>
    /// EditActivity.xaml 的交互逻辑
    /// </summary>
    public partial class EditActivity : MetroWindow
    {
        DbUtill dbUtill = new DbUtill();
        public string activityId { get; set; }
        public EditActivity()
        {
            InitializeComponent();
        }
        public EditActivity(string activityId)
        {
            InitializeComponent();
            string activityName = dbUtill.GetActivitieNameByID(activityId);
            txtUserName.Text = activityName;
        }
        //修改
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string activityName = txtUserName.Text.Trim();
            if (activityName.Length < 1)
            {
                popup1.IsOpen = true;
            }else if (activityName.Trim().Length > 15)
            {
                popup2.IsOpen = true;
            }
            else
            {
                bool flag = dbUtill.UpdateActivityName(activityId, activityName);
                if (flag == true)
                {
                    MessageBox.Show("修改成功", "提示");
                }
                else
                {
                    MessageBox.Show("修改失败", "提示");
                }
                DialogResult = true;
            }
        }
    }
}
