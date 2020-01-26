using System;
using System.IO.Ports;
using System.Windows;
using Tanita.model;
using Tanita.Utill;

namespace Tanita
{
    /// <summary>
    /// TeacherTest.xaml 的交互逻辑
    /// </summary>
    public partial class TeacherTest : Window
    {
        public static string memberId { get; set; }
        public static string memberName { get; set; }
        public static string memberBirthDay { get; set; }
        public static string memberAge { get; set; }
        public static string memberHeight { get; set; }
        public static string memberSex { get; set; }

        DbUtill dbUtill = new DbUtill();
        MeasureCommand mc = new MeasureCommand();
        CommonUtill commonUtill = new CommonUtill();
        public TeacherTest()
        {
            InitializeComponent();
        }
        public TeacherTest(Teacher teacher)
        {
            InitializeComponent();
            name.Text = teacher.memberName;
            sex.Text = teacher.memberSex;
            memberId = teacher.memberId;
            memberName = teacher.memberName;
            if (teacher.memberSex.Equals("男"))
            {
                memberSex = "0";
            }
            else
            {
                memberSex = "1";
            }
            memberBirthDay = teacher.memberAge;
            memberAge = commonUtill.GetAgeByBirthday(teacher.memberAge);
            age.Text = memberAge;
            if (memberAge.Length <= 1)
            {
                memberAge = "0" + memberAge;
            }
            if (teacher.memberHeight.IndexOf('.') > -1)
            {
                memberHeight = teacher.memberHeight;
            }
            else
            {
                memberHeight = teacher.memberHeight + ".0";
            }
            height.Text = memberHeight;
        }
        //接收数据
        private void Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            bool flag2 = PortConfig.serialPort1 == null || !PortConfig.serialPort1.IsOpen;
            if (!flag2)
            {
                string backStr = PortConfig.serialPort1.ReadExisting();
                if (backStr.Length > 30)
                {
                    if (backStr.Substring(0, 1) == "{")
                    {
                        string height = null;
                        if (memberHeight != null)
                        {
                            if (memberHeight.IndexOf('.') > -1)
                            {
                                height = memberHeight;
                            }
                            else
                            {
                                height = memberHeight + ".0";
                            }
                            TeacherCheckData data = new TeacherCheckData
                            {
                                Id = Guid.NewGuid().ToString(),
                                memberName = memberName,
                                memberAge = memberBirthDay,
                                memberSex = memberSex,
                                memberHeight = double.Parse(memberHeight),
                                checkDate = DateTime.Now.ToString(),
                                memberId = memberId,
                                JsonContent = backStr,
                                isUpload = "0"
                            };
                            bool isSave = dbUtill.SavaTeacherCheckData(data);
                            if (isSave)
                            {
                                Dispatcher.Invoke(new Action(() =>
                                {
                                    MessageBox.Show("检测完毕,请离开体测仪", "提示");
                                    closeBtn.IsEnabled = true;
                                    checkBtn.IsEnabled = true;
                                    display.Text = "检测完成，如需重新检测请按\"开始检测\"进行检测";
                                }));
                            }
                            else
                            {
                                Dispatcher.Invoke(new Action(() =>
                                {
                                    MessageBox.Show("保存失败","提示");
                                    closeBtn.IsEnabled = true;
                                    checkBtn.IsEnabled = true;
                                    display.Text = "检测完成，如需重新检测请按\"开始检测\"进行检测";
                                }));
                            }
                        }
                        else
                        {
                            MessageBox.Show("没有数据", "提示");
                        }
                    }
                }
            }
        }
        private void CheckBtn_Click(object sender, RoutedEventArgs e)
        {
            closeBtn.IsEnabled = false;
            checkBtn.IsEnabled = false;
            display.Text = "检测中......";
            PortConfig.serialPort1.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);
            mc.Measure(PortConfig.serialPort1, Port.modelId,memberSex,memberAge,memberHeight);
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
