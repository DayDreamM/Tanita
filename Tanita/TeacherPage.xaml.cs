using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Tanita.model;
using Tanita.Utill;
using Tanita.Views;

namespace Tanita
{
    /// <summary>
    /// TeacherPage.xaml 的交互逻辑
    /// </summary>
    public partial class TeacherPage : Page
    {
        public static string memberId { get; set; }
        public static string memberName { get; set; }
        public static string memberAge { get; set; }
        public static string memberHeight { get; set; }
        public static string memberSex { get; set; }
        DbUtill dbUtill = new DbUtill();
        CommonUtill commonUtill = new CommonUtill();
        public TeacherPage()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            List<Teacher> teachers = dbUtill.GetTeacherList();
            MemberDataGrid.ItemsSource = teachers;
            checkDataGrid.ItemsSource = null;
        }
        //新增按钮
        private void AddMemberBtn_Click(object sender, RoutedEventArgs e)
        {
            AddMember addMember = new AddMember();
            addMember.ShowDialog();
            if(addMember.DialogResult == true)
            {
                Init();
            }
        }
        //查询
        private void QueryBtn_Click(object sender, RoutedEventArgs e)
        {
            string keyword = Keyword.Text;
            List<Teacher> teachers = dbUtill.QueryMember(keyword);
            if (teachers.Count>0)
            {
                MemberDataGrid.ItemsSource = teachers;
            }
            else
            {
                System.Windows.MessageBox.Show("您查询的用户不存在,请检查后重新输入", "提示");
            }
        }
        //检测
        private void AddCheck_Click(object sender, RoutedEventArgs e)
        {
            if(PortConfig.IsOpen == true)
            {
                Teacher teacher = MemberDataGrid.SelectedItem as Teacher;
                TeacherTest teacherTest = new TeacherTest(teacher);
                teacherTest.ShowDialog();
            }
            else
            {
                System.Windows.MessageBox.Show("未连接设备,请连接后再试", "提示");
            }
        }
        //批量导入
        private void BathInoutBtn_Click(object sender, RoutedEventArgs e)
        {
            CheckDataUtill checkDataUtill = new CheckDataUtill();
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "请选择批量导入文件",
                Filter = "excel文件(*.xlsx)|*.xlsx|所有文件(*.*)|*.*"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path = Path.GetFullPath(ofd.FileName);
                DataTable dt = checkDataUtill.DBExcelToDataTable(path, "Sheet1");
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string memberName = dt.Rows[i][0].ToString();
                        string birthday = dt.Rows[i][1].ToString();
                        string sex = dt.Rows[i][2].ToString();
                        int sex_int;
                        if (sex.Equals("男"))
                        {
                            sex_int = 0;
                        }
                        else
                        {
                            sex_int = 1;
                        }
                        string height = dt.Rows[i][3].ToString();
                        double memberHeight = double.Parse(height);
                        dbUtill.AddMember(memberName, birthday, sex_int, memberHeight);
                    }
                    System.Windows.MessageBox.Show("导入成功", "提示");
                    Init();
                }
                catch (Exception ex)
                {

                }
            }
        }
        //批量导出
        private void BathOutputBtn_Click(object sender, RoutedEventArgs e)
        {
            TeacherBathOutput output = new TeacherBathOutput();
            output.Show();
        }
        //选中
        private void MemberDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MemberDataGrid.SelectedItem is Teacher teacher)
            {
                List<TeacherCheckData> checkDatas = dbUtill.GetTeacherCheckDatasByMemberId(teacher.memberId);
                checkDataGrid.ItemsSource = checkDatas;
            }
        }
        //打印
        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            if (checkDataGrid.SelectedItem is TeacherCheckData teacherCheckData)
            {
                Report rp = new Report();
                if (teacherCheckData.JsonContent != null)
                {
                    rp.teacherCheckData = teacherCheckData;
                    rp.TeacherPrintService();
                }
                else
                {
                    System.Windows.MessageBox.Show("体侧数据不存在", "提示");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("体检报告不存在","提示");
            }
        }
        //重置
        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            Keyword.Text = "";
            List<Teacher> teachers = dbUtill.GetTeacherList();
            MemberDataGrid.ItemsSource = teachers;
        }
        //删除
        private void DeleteMemberBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MemberDataGrid.SelectedItem is Teacher teacher)
            {
                if (System.Windows.MessageBox.Show("是否删除该用户?", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {

                    bool flag = dbUtill.DeleteMemberByID(teacher.memberId);
                    if (flag == true)
                    {
                        System.Windows.MessageBox.Show("删除成功", "提示");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("删除失败", "提示");
                    }
                }
                Init();
            }
            else
            {
                System.Windows.MessageBox.Show("请先选择被检测人", "提示");
            }
        }
        //编辑
        private void EditCheck_Click(object sender, RoutedEventArgs e)
        {
            Teacher teacher = MemberDataGrid.SelectedItem as Teacher;
            if (teacher != null)
            {
                EditMember editMember = new EditMember(teacher.memberId)
                {
                    memberId = teacher.memberId
                };
                editMember.ShowDialog();
                if (editMember.DialogResult == true)
                {
                    Init();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("请先选择要操作的人员","提示");
            }
        }
        //下载
        private void DownBtn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                FileInfo sourceFile = new FileInfo(@".\model\被检测人批量导入模板.xlsx");
                sourceFile.CopyTo(folder.SelectedPath + @"\被检测人批量导入模板.xlsx");
                System.Windows.MessageBox.Show("下载完成，已保存至" + folder.SelectedPath + @"\被检测人批量导入模板.xlsx", "提示");
            }
        }
        private void MemberDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex()+1;
        }
    }
}
