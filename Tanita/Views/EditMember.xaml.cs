using MahApps.Metro.Controls;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using Tanita.model;

namespace Tanita.Views
{
    /// <summary>
    /// EditMember.xaml 的交互逻辑
    /// </summary>
    public partial class EditMember : MetroWindow
    {
        DbUtill dbUtill = new DbUtill();
         public string memberId { get; set; }
        public EditMember()
        {
            InitializeComponent();
        }
        public EditMember(string memberId)
        {
            InitializeComponent();
             Teacher teacher = dbUtill.GetTeacherByMemberId(memberId);
            memberName.Text = teacher.memberName;
            Birthday.Text = teacher.memberAge;
            MenberSex.Text = teacher.memberSex;
            MenberHeight.Text = teacher.memberHeight;
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            string birthday = Birthday.Text.Trim();
            string membersex = MenberSex.Text.Trim();
            string memberheight = MenberHeight.Text.Trim();
            bool flag1 = Regex.IsMatch(memberheight, "^[1-9]\\d*$");
            bool flag2 = Regex.IsMatch(memberheight, "^[1-9]\\d*\\.\\d*|0\\.\\d*[1-9]\\d*$");
            if (birthday.Length < 1)
            {
                popup2.IsOpen = true;
            }
            else if (membersex.Length < 1)
            {
                popup3.IsOpen = true;
            }
            else if (memberheight.Length < 1 || (!flag1 & !flag2))
            {
                popup4.IsOpen = true;
            }
            else
            {
                int sex;
                if (membersex.Equals("男"))
                {
                    sex = 0;
                }
                else
                {
                    sex = 1;
                }
                try
                {
                    double height = double.Parse(memberheight);
                    bool flag = dbUtill.EditMember(birthday, sex, height, memberId);
                    if (flag == true)
                    {
                        MessageBox.Show("修改成功", "提示");
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("修改失败", "提示");
                    }
                }catch(Exception)
                {
                    MessageBox.Show("请输入正确格式的身高","错误");
                }
            }
        }
    }
}
