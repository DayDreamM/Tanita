using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Tanita.model;
using Tanita.Views;

namespace Tanita
{
    /// <summary>
    /// ActivtyPage.xaml 的交互逻辑
    /// </summary>
    public partial class ActivtyPage : Page
    {
        DbUtill dbUtill = new DbUtill();
        public MainWindow mainWindow { get; set; }
        private string ActivityId { get; set; }
        public ActivtyPage()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            List<Activity> list = dbUtill.GetActivities();
            activity_panel.ItemsSource = list;
        }
        //修改
        private void Edititem_Click(object sender, RoutedEventArgs e)
        {
            if (ActivityId == null || ActivityId.Equals(""))
            {
                MessageBox.Show("未选择活动","提示");
            }
            else
            {
                EditActivity editActivity = new EditActivity(ActivityId)
                {
                    activityId = ActivityId
                };
                editActivity.ShowDialog();
                if (editActivity.DialogResult == true)
                {
                    Init();
                    ActivityId = "";
                }
            }
        }
        //新增活动
        private void Adddetection_btn_Click(object sender, RoutedEventArgs e)
        {
            if (dbUtill.QueryStudentCount() > 0)
            {
                AddActivityWin add = new AddActivityWin
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                add.ShowDialog();
                if (add.DialogResult == true)
                {
                    Init();
                    ActivityId = "";
                }
            }
            else
            {
                MessageBox.Show("暂无学生数据,请先导入数据再试", "提示");
            }
        }
        //归档
        private void IsFileItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActivityId == null || ActivityId.Equals(""))
            {
                MessageBox.Show("未选择活动", "提示");
            }
            else
            {
                if (MessageBox.Show("是否归档该活动?", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    int i = dbUtill.IsFileActivity(ActivityId);
                    if (i > 0)
                    {
                        MessageBox.Show("归档成功", "提示");
                    }
                    else
                    {
                        MessageBox.Show("归档失败", "提示");
                    }
                    Init();
                    ActivityId = null;
                }
            }
        }
        //删除活动
        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActivityId == null || ActivityId.Equals(""))
            {
                MessageBox.Show("未选择活动", "提示");
            }
            else
            {
                if (dbUtill.GetActivityCheckerCount(ActivityId) > 0)
                {
                    if (MessageBox.Show("该测试活动已存在学生体侧数据，是否确定删除?", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        int i = dbUtill.DeleteActivity(ActivityId);
                        if (i > 0)
                        {
                            MessageBox.Show("删除成功", "提示");
                        }
                        else
                        {
                            MessageBox.Show("删除失败", "提示");
                        }
                        Init();
                        ActivityId = "";
                    }
                }
                else
                {
                    if (MessageBox.Show("是否删除该活动?", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        int i = dbUtill.DeleteActivity(ActivityId);
                        if (i > 0)
                        {
                            MessageBox.Show("删除成功", "提示");
                        }
                        else
                        {
                            MessageBox.Show("删除失败", "提示");
                        }
                        Init();
                        ActivityId = "";
                    }
                }
            }
        }
        //打开归档界面
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            List<Activity> activities = dbUtill.GetIsFiles();
            if (activities.Count > 0)
            {
                IsFileWin isFileWin = new IsFileWin();
                isFileWin.ShowDialog();
                if (isFileWin.DialogResult == false)
                {
                    Init();
                }
            }
            else
            {
                BlankWin blankWin = new BlankWin();
                blankWin.ShowDialog();
            }
        }
        // 选择活动
        private void Activity_panel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (activity_panel.SelectedItem is Activity activity)
            {
                ActivityId = activity.id;
            }
        }
        //进入
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string activityInfo = (string)button.Tag;
            string[] activityInfoArray = activityInfo.Split(',');
            string activityId = activityInfoArray[0];
            string activityName = activityInfoArray[1];
            mainWindow.ContentChange(activityId, activityName);
        }
    }
}
