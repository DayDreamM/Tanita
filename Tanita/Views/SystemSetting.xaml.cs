using MahApps.Metro.Controls;

namespace Tanita.Views
{
    /// <summary>
    /// SystemSetting.xaml 的交互逻辑
    /// </summary>
    public partial class SystemSetting : MetroWindow
    {
        DbUtill dbUtill = new DbUtill();
        public SystemSetting()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            string imageUrlText = dbUtill.GetImageUrl();
            imageUrl.Content = imageUrlText;
        }
        private void BtnChangeUrl_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();
            if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dbUtill.UpdateImageUrl(folder.SelectedPath);
                Init();
            }
        }
    }
}
