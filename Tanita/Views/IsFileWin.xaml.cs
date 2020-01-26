using MahApps.Metro.Controls;
using System.Windows.Controls;

namespace Tanita.Views
{
    /// <summary>
    /// IsFileWin.xaml 的交互逻辑
    /// </summary>
    public partial class IsFileWin : MetroWindow
    {
        DbUtill dbUtill = new DbUtill();
        public IsFileWin()
        {
            InitializeComponent();
            IsFileInit();
        }
        public void IsFileInit()
        {
            IsFilePage isFilePage = new IsFilePage
            {
                isFileWin = this
            };
            pagecontent.Content = new Frame()
            {
                Content = isFilePage
            };
        }
        public void ContentChange(string activtyId, string activityName)
        {
            TestActivityPage testActivity = new TestActivityPage(activityName,activtyId)
            {
                isFileWin = this,
                Uid = activtyId,
                ActivityId = activtyId,
                ActivityName = activityName
            };
            pagecontent.Content = new Frame()
            {
                Content = testActivity
            };
        }
        public void ContentBack()
        {
            IsFilePage isFilePage = new IsFilePage
            {
                isFileWin = this
            };
            pagecontent.Content = new Frame()
            {
                Content = isFilePage
            };
        }
        public void SetResult()
        {
            DialogResult = true;
        }
    }
}
