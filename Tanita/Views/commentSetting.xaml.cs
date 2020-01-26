using MahApps.Metro.Controls;
using System.Windows;
using Tanita.model;

namespace Tanita.Views
{
    /// <summary>
    /// commentSetting.xaml 的交互逻辑
    /// </summary>
    public partial class commentSetting : MetroWindow
    {
        DbUtill dbUtill = new DbUtill();
        public commentSetting()
        {
            InitializeComponent();
            Comment comment = dbUtill.GetComment();
            low.Text = comment.low;
            mid.Text = comment.mid;
            hight.Text = comment.hight;
        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (low.Text.Length > 30)
            {
                popup1.IsOpen = true;
            }
            else if (mid.Text.Length > 30)
            {
                popup2.IsOpen = true;
            }
            else if (hight.Text.Length > 30)
            {
                popup3.IsOpen = true;
            }
            else
            {
                int i = dbUtill.EditComment(low.Text, mid.Text, hight.Text);
                if (i > 0)
                {
                    MessageBox.Show("修改成功", "提示");
                    Close();
                }
                else
                {
                    MessageBox.Show("修改失败", "提示");
                }
            }
        }
    }
}
