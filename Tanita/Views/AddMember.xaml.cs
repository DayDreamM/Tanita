using MahApps.Metro.Controls;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Tanita.Views
{
    /// <summary>
    /// AddMember.xaml 的交互逻辑
    /// </summary>
    public partial class AddMember : MetroWindow
    {
        DbUtill dbUtill = new DbUtill();
        public AddMember()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string memberName = MemberName.Text.Trim();
            string birthday = Birthday.Text.Trim();
            string membersex = MenberSex.Text.Trim();
            string memberheight = MenberHeight.Text.Trim();
            bool flag1 = Regex.IsMatch(memberheight, "^[1-9]\\d*$");
            bool flag2 = Regex.IsMatch(memberheight, "^[1-9]\\d*\\.\\d*|0\\.\\d*[1-9]\\d*$");
            if (memberName.Length < 1)
            {
                popup1.IsOpen = true;
            }
            else if (memberName.Length > 5)
            {
                popup5.IsOpen = true;
            }
            else if(birthday.Length<1)
            {
                popup2.IsOpen = true;
            }else if(membersex.Length < 1)
            {
                popup3.IsOpen = true;
            }else if (memberheight.Length < 1 || (!flag1 & !flag2))
            {
                popup4.IsOpen = true;
            }else if(memberheight.Length > 5)
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
                    bool flag = dbUtill.AddMember(memberName, birthday, sex, height);
                    DialogResult = true;
                }
                catch (Exception)
                {
                    popup4.IsOpen = true;
                }
            }
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
