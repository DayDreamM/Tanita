using MahApps.Metro.Controls;
using System.Windows;
using Tanita.DAL;
using Tanita.model;

namespace Tanita.Views
{
    /// <summary>
    /// EditPassword.xaml 的交互逻辑
    /// </summary>
    public partial class EditPassword : MetroWindow
    {
        public EditPassword()
        {
            InitializeComponent();
        }

        private void BtnEditPassword_Click(object sender, RoutedEventArgs e)
        {
            Api api = new Api();
            string password = txtNewPassword.Text;
            string password_confirm = txtNewPassword_confirm.Text;
            if (password.Trim().Length > 0 && password.Trim().Length < 20)
            {
                if (password != password_confirm)
                {
                    MessageBox.Show("输入的新密码不一致,请重新输入", "提示");
                }
                else
                {
                    int i = api.EditPassword(password, "");
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
            else
            {
                popup.IsOpen = true;
            }
        }
    }
}
