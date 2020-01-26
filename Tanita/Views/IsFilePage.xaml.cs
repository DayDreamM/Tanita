using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Tanita.model;

namespace Tanita.Views
{
    /// <summary>
    /// IsFilePage.xaml 的交互逻辑
    /// </summary>
    public partial class IsFilePage : Page
    {
        public IsFilePage()
        {
            InitializeComponent();
            Init();
        }
        DbUtill dbUtill = new DbUtill();
        public IsFileWin isFileWin { get; set; }
        private string ActivityId { get; set; }
        public void Init()
        {
            List<Activity> list = dbUtill.GetIsFiles();
            activity_panel.ItemsSource = list;
        }
        //取消归档
        private void IsFileItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActivityId == null || ActivityId.Equals(""))
            {
                MessageBox.Show("未选择活动", "提示");
            }
            else
            {
                if (MessageBox.Show("是否取消归档该活动?", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    int i = dbUtill.CancelIsFileActivity(ActivityId);
                    if (i > 0)
                    {
                        MessageBox.Show("取消归档成功", "提示");
                        ActivityId = null;
                    }
                    else
                    {
                        MessageBox.Show("取消归档失败", "提示");
                        ActivityId = null;
                    }
                    activity_panel.SelectedItem = null;
                    Init();
                }
            }
        }
        //进入活动
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string activityInfo = (string)button.Tag;
            string[] activityInfoArray = activityInfo.Split(',');
            string activityId = activityInfoArray[0];
            string activityName = activityInfoArray[1];
            isFileWin.ContentChange(activityId, activityName);
        }
        private void Activity_panel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (activity_panel.SelectedItem is Activity activity)
            {
                ActivityId = activity.id;
            }
        }
        //搜索按钮
        private void QueryBtn_Click(object sender, RoutedEventArgs e)
        {
            Time1.SelectedDateFormat = DatePickerFormat.Short;
            Time2.SelectedDateFormat = DatePickerFormat.Short;
            string time1 = Time1.SelectedDate.ToString();
            string time2 = Time2.SelectedDate.ToString();
            List<Activity> activities = dbUtill.QueryActivities(Keyword.Text, time1, time2);
            if (activities.Count>0)
            {
                activity_panel.ItemsSource = activities;
            }
            else
            {
                MessageBox.Show("暂未找到匹配的记录", "提示");
            }
        }
        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            Keyword.Text = "";
            Time1.Text = "";
            Time2.Text = "";
            Init();
        }
    }
}
