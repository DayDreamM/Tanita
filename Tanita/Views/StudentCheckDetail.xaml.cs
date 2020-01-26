using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;
using Tanita.model;
using Tanita.Utill;

namespace Tanita.Views
{
    /// <summary>
    /// StudentCheckDetail.xaml 的交互逻辑
    /// </summary>
    public partial class StudentCheckDetail : MetroWindow
    {
        DbUtill dbUtill = new DbUtill();
        public StudentCheckDetail()
        {
            InitializeComponent();
        }
        public StudentCheckDetail(string studentId)
        {
            InitializeComponent();
            List<StudentCheckData> checkDatas = dbUtill.GetCheckerInfo(studentId);
            if (checkDatas.Count > 0)
            {
                CheckDataGrid.ItemsSource = checkDatas;
            }
            else
            {
                CheckDataGrid.Visibility = Visibility.Collapsed;
                display.Visibility = Visibility.Visible;
            }

        }
        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDataGrid.SelectedItem is StudentCheckData studentCheckData)
            {
                Report rp = new Report();
                if (studentCheckData.jsonContent != null)
                {
                    rp.checkData = studentCheckData;
                    rp.PrintService();
                }
                else
                {
                    MessageBox.Show("体侧数据不存在","提示");
                }
            }
            else
            {
                MessageBox.Show("体检报告不存在","提示");
            }
        }
    }
}
