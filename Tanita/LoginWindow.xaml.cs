using System.Windows;
using System.Windows.Input;
using Tanita.DAL;
using Tanita.model;

namespace Tanita
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        Api api = new Api();
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            bool flag = false;
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Password.Trim();
            if (username.Length < 1)
            {
                popup1.IsOpen = true;
                txtUserName.Focus();
            }
            else if (password.Length < 1)
            {
                popup4.IsOpen = true;
                txtPassword.Focus();
            }
            else
            {
                btnLogin.IsEnabled = false;
                btnLogin.Content = "登陆中..";
                flag = api.Login(username, password);
                if (flag == true)
                {
                    if (remenberBtn.IsChecked == true)
                    {
                        user.username = username;
                        user.password = password;
                        user.status = "1";
                    }
                    else
                    {
                        user.username = "";
                        user.password = "";
                        user.status = "0";
                    }
                    api.RemamberPassword(user);
                    MainWindow mainWindow = new MainWindow(username);
                    mainWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("账号或密码错误", "提示");
                    btnLogin.IsEnabled = true;
                    btnLogin.Content = "登陆";
                }
            }
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
        //移动
        private void window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        //最小化
        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            User user = api.GetUser();
            txtUserName.Text = user.username;
            txtPassword.Password = user.password;
            if (user.status.Equals("1"))
            {
                remenberBtn.IsChecked = true;
            }
            else
            {
                remenberBtn.IsChecked = false;
            }
        }
    }
}
