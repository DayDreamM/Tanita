using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Tanita.model;
using Tanita.Utill;

namespace Tanita.Views
{
    /// <summary>
    /// TeacherBathOutput.xaml 的交互逻辑
    /// </summary>
    public partial class TeacherBathOutput : MetroWindow
    {
        DbUtill dbUtill = new DbUtill();
        public TeacherBathOutput()
        {
            InitializeComponent();
        }
        //
        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            string memberName = MemberName.Text.Trim();
            string memberId = dbUtill.GetTeacherIDByName(memberName);
            if(memberName.Length>0 && memberId.Length <=1)
            {
                MessageBox.Show("不存在该用户，请核对后再试","提示");
            }
            else
            {
                string url = dbUtill.GetImageUrl();
                string memberSex = "";
                string IsUpload = "";
                Time1.SelectedDateFormat = DatePickerFormat.Short;
                Time2.SelectedDateFormat = DatePickerFormat.Short;
                string time1 = Time1.SelectedDate.ToString();
                string time2 = Time2.SelectedDate.ToString();
                if (MenberSex.SelectedItem is ComboBoxItem item)
                {
                    string membersex = item.Content.ToString();
                    if (membersex.Equals("男"))
                    {
                        memberSex = "0";
                    }
                    else
                    {
                        memberSex = "1";
                    }
                }
                if (Isupload.SelectedItem is ComboBoxItem item2)
                {
                    string isUpload = item2.Content.ToString();
                    if (isUpload.Equals("否"))
                    {
                        IsUpload = "0";
                    }
                    else
                    {
                        IsUpload = "1";
                    }
                }
                Report rp = new Report();
                List<TeacherCheckData> teachers = dbUtill.GetTeacherCheckDataList(memberId, time1, time2, memberSex, IsUpload);
                if (teachers.Count > 0)
                {
                    for(int i = 0;i< teachers.Count;i++)
                    {
                        rp.teacherCheckData = teachers[i];
                        rp.CreateTeacherPrint(i);
                    }
                    MessageBox.Show("已保存至" + url,"提示");
                }
                else
                {
                    MessageBox.Show("没有符合条件的检测记录","提示");
                }
            }
        }
        private void CancelBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }
    }
}
