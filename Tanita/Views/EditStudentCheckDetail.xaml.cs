using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;
using Tanita.model;
using Tanita.Utill;

namespace Tanita.Views
{
    /// <summary>
    /// EditStudentCheckDetail.xaml 的交互逻辑
    /// </summary>
    public partial class EditStudentCheckDetail : MetroWindow
    {
        DbUtill dbUtill = new DbUtill();
        CheckDataUtill checkDataUtill = new CheckDataUtill();
        public string activityId { get; set; }
        public string studentId { get; set; }
        public EditStudentCheckDetail()
        {
            InitializeComponent();
        }
        public EditStudentCheckDetail(string activityId,string studentId)
        {
            InitializeComponent();
            StudentCheckData checkData = dbUtill.GetStudentCheckInfo(activityId, studentId);
            Dictionary<string, string> dic = checkDataUtill.GetDicByBackStr(checkData.jsonContent);
            Student student1 = dbUtill.GetStudentNameAndBirthDayByID(studentId);
            name.Text = student1.studentName;
            age.Text = dic["AG"];
            date.Text = checkData.checkDate;
            birthday.Text = student1.birthday.Split(' ')[0];
            if (dic["GE"].Equals("0"))
            {
                sex.Text = "男";
            }
            else
            {
                sex.Text = "女";
            }
            weight.Text = dic["Wk"];
            Pt.Text = dic["Pt"];
            FW.Text = dic["FW"];
            fW.Text = dic["fW"];
            wW.Text = dic["wW"];
            MW.Text = dic["MW"];
            mW.Text = dic["mW"];
            height.Text = dic["Hm"];
            BMI.Text = dic["MI"];
            rB.Text = dic["rB"];
            bW.Text = dic["bW"];
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }
        //确定
        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            StudentCheckData checkData = dbUtill.GetStudentCheckInfo(activityId, studentId);
            Dictionary<string, string> dic = checkDataUtill.GetDicByBackStr(checkData.jsonContent);
            dic["Wk"] = weight.Text;
            dic["AG"] = age.Text;
            dic["Pt"] = Pt.Text;
            dic["FW"] = FW.Text;
            dic["fW"] = fW.Text;
            dic["wW"] = wW.Text;
            dic["MW"] = MW.Text;
            dic["mW"] = mW.Text;
            dic["Hm"] = height.Text;
            dic["MI"] = BMI.Text;
            dic["rB"] = rB.Text;
            dic["bW"] = bW.Text;
            string jsonContent = checkDataUtill.DictionaryListToString(dic);
            int i = dbUtill.EditStudentCheckInfo(activityId, studentId, jsonContent);
            if (i > 0)
            {
                MessageBox.Show("修改成功", "提示");
                Close();
            }
            else
            {
                MessageBox.Show("修改失败", "提示");
                Close();
            }
        }
    }
}
