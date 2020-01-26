using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using Tanita.model;

namespace Tanita.Utill
{
    class Report
    {
        DbUtill dbUtill = new DbUtill();
        CheckDataUtill checkDataUtill = new CheckDataUtill();
        public StudentCheckData checkData { get; set; }
        public TeacherCheckData teacherCheckData { get; set; }
        //打印报告带预览
        public void PrintService()
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.Landscape = true;
                pd.PrintPage += Pd_PrintPage;
                PrintPreviewDialog dialog = new PrintPreviewDialog();
                dialog.Document = pd;
                if (dialog.ShowDialog() == DialogResult.Yes)
                {
                    pd.Print();
                };
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("错误,因为" + ex.Message, "错误");
            }
        }
        //打印报告不带预览
        public void PrintServiceNotPreview()
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.Landscape = true;
                pd.PrintPage += Pd_PrintPage;
                pd.Print();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("错误,因为" + ex.Message, "错误");
            }
        }
        //打印成人报告
        public void TeacherPrintService()
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += Teacher_PrintPage;
                PrintPreviewDialog cppd = new PrintPreviewDialog
                {
                    Document = pd
                };
                if (cppd.ShowDialog() == DialogResult.Yes)
                {
                    pd.Print();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("错误,因为" + ex.Message, "错误");
            }
        }
        public void TeacherPrintServiceNotPreview()
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += Teacher_PrintPage;
                pd.Print();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("错误,因为" + ex.Message, "错误");
            }
        }
        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("宋体", 11f);
            Font font1 = new Font("宋体", 8f);
            Font font2 = new Font("宋体", 9f);
            Dictionary<string, string> dic = checkDataUtill.GetDicByBackStr(checkData.jsonContent);
            Image image = Image.FromFile(@"Resources/780_boy.jpg");
            int num = 0x317;
            int num1 = 0x45f;
            e.Graphics.DrawImage(image, 0, 0, num1, num);
            string studentName = dbUtill.GetStudentNameByID(checkData.studentId);
            try
            {
                e.Graphics.DrawString(studentName, font, Brushes.Black, new Point(75, 73));   //姓名
                e.Graphics.DrawString(dic["AG"], font, Brushes.Black, new Point(200, 73));   //年龄
                e.Graphics.DrawString(dic["Da"], new Font("宋体", 11f), Brushes.Black, new Point(300, 73));  //日期
                string sex = dic["GE"];
                if (sex.Equals("0"))
                {
                    e.Graphics.DrawString("√", font, Brushes.Black, new Point(477, 73)); //性别
                }
                else
                {
                    e.Graphics.DrawString("√", font, Brushes.Black, new Point(513, 73)); //
                }
                e.Graphics.DrawString(checkData.studentId, new Font("宋体", 11f), Brushes.Black, new Point(75, 103));  //Id
                e.Graphics.DrawString(dic["Pt"], font, Brushes.Black, new Point(240, 103));   //着衣重量
                e.Graphics.DrawString(dic["Hm"], font, Brushes.Black, new Point(334, 104));  //身高
                string bodytype = dic["Bt"];
                if (bodytype.Equals("0"))
                {
                    e.Graphics.DrawString("√", font, Brushes.Black, new Point(440, 105)); //身体类型
                }
                else
                {
                    e.Graphics.DrawString("√", font, Brushes.Black, new Point(465, 105));
                }
                e.Graphics.DrawString(dic["Wk"], font, Brushes.Black, new Point(80, 195));  //体重
                e.Graphics.DrawString(dic["FW"], font, Brushes.Black, new Point(80, 220));  //体脂率
                e.Graphics.DrawString(dic["fW"], font, Brushes.Black, new Point(80, 248));  //体脂量
                e.Graphics.DrawString(dic["MW"], font, Brushes.Black, new Point(80, 273));  //去脂体重
                e.Graphics.DrawString(dic["wW"], font, Brushes.Black, new Point(80, 298));  //体水份量
                e.Graphics.DrawString(dic["MI"], font, Brushes.Black, new Point(80, 325));  //BMI
                //参考值
                e.Graphics.DrawString("30.22~38.11", font1, Brushes.Black, new Point(152, 198));  //体重
                e.Graphics.DrawString("10~25.9", font1, Brushes.Black, new Point(152, 223));  //体脂率
                e.Graphics.DrawString("2.84~8.95", font1, Brushes.Black, new Point(152, 248));  //体脂量
                e.Graphics.DrawString("27.38~29.16", font1, Brushes.Black, new System.Drawing.Point(152, 273));  //去脂体重
                e.Graphics.DrawString("17.1~18.525", font1, Brushes.Black, new Point(152, 298));  //体水份量
                e.Graphics.DrawString("13.6~18.6", font1, Brushes.Black, new System.Drawing.Point(152, 322));  //BMI
                //
                e.Graphics.DrawString(dic["mW"], font, Brushes.Black, new Point(130, 367));  //肌肉量
                e.Graphics.DrawString(dic["bW"], font, Brushes.Black, new Point(130, 405));  //推定骨量
                e.Graphics.DrawString(dic["rB"]+"Kcal", font, Brushes.Black, new Point(79, 450));  // 基础代谢率
                e.Graphics.DrawString(dic["MI"], font, Brushes.Black, new Point(79, 500));  // 肥胖指数
                double bmi = double.Parse(dic["MI"]);
                int bmi_i = (int)bmi;
                int b= 10*(bmi_i - 16); 
                Rectangle rectangle = new Rectangle(125, 495,80+b, 10);
                e.Graphics.FillRectangle(Brushes.Black, rectangle);
                e.Graphics.DrawString(dic["ml"], font1, Brushes.Black, new Point(60, 630));  //左上肢
                e.Graphics.DrawString(dic["mr"], font1, Brushes.Black, new Point(220, 630));  //BMI
                e.Graphics.DrawString(dic["mL"], font1, Brushes.Black, new Point(60, 710));  //BMI
                e.Graphics.DrawString(dic["mR"], font1, Brushes.Black, new Point(220, 710));  //BMI
                e.Graphics.DrawString(dic["mT"], font1, Brushes.Black, new System.Drawing.Point(185, 680));  //BMI
                //肌肉平衡
                e.Graphics.DrawString(dic["mr"], font1, Brushes.Black, new System.Drawing.Point(330, 400));  //左上肢
                e.Graphics.DrawString(dic["Mr"], font1, Brushes.Black, new Point(495, 400));  //BMI
                e.Graphics.DrawString(dic["mR"], font1, Brushes.Black, new Point(330, 498));  //BMI
                e.Graphics.DrawString(dic["MR"], font1, Brushes.Black, new System.Drawing.Point(495, 498));  //BMI
                //各部分脂肪
                e.Graphics.DrawString(dic["Fr"], font1, Brushes.Black, new Point(514, 625));  //右上肢
                e.Graphics.DrawString(dic["fr"], font1, Brushes.Black, new Point(514, 635));  //
                e.Graphics.DrawString(dic["Fl"], font1, Brushes.Black, new Point(336, 625));  //左上肢
                e.Graphics.DrawString(dic["fl"], font1, Brushes.Black, new Point(340, 635));  //
                e.Graphics.DrawString(dic["FR"], font1, Brushes.Black, new Point(518, 715));  //右下肢
                e.Graphics.DrawString(dic["fR"], font1, Brushes.Black, new Point(518, 725));  //
                e.Graphics.DrawString(dic["FL"], font1, Brushes.Black, new Point(336, 710));  //左下肢
                e.Graphics.DrawString(dic["fL"], font1, Brushes.Black, new Point(340, 720));  //
                e.Graphics.DrawString(dic["FT"], font1, Brushes.Black, new Point(515, 680));  //躯干部
                e.Graphics.DrawString(dic["fT"], font1, Brushes.Black, new Point(515, 690));  //
                //
                e.Graphics.DrawString(dic["fW"], font1, Brushes.Black, new Point(380, 178));  //体脂量
                e.Graphics.DrawString(dic["bW"], font1, Brushes.Black, new Point(440, 178));  //推定骨量
                e.Graphics.DrawString(dic["Wk"], font1, Brushes.Black, new Point(320, 330));  //体重
                e.Graphics.DrawString(dic["MW"], font1, Brushes.Black, new Point(380, 330));  //非脂肪量
                e.Graphics.DrawString(dic["mW"], font1, Brushes.Black, new Point(440, 330));  //肌肉量
                e.Graphics.DrawString(dic["wW"], font1, Brushes.Black, new Point(500, 330));  //体水份量
                int age = int.Parse(dic["AG"]);
                double weight =  double.Parse(dic["Wk"])*3.2;
                double height = double.Parse(dic["Hm"]) * 2.15;
                int weight1 = Convert.ToInt32(weight);
                int height1 = Convert.ToInt32(height);
                if (sex.Equals("0"))
                {
                    e.Graphics.DrawString("•", font, Brushes.Red, new Point(590+age*11, 512-weight1)); //
                    e.Graphics.DrawString("体重", font, Brushes.Black, new Point(593 + age * 11, 505 - weight1)); //体重
                    e.Graphics.DrawString(dic["Wk"], font, Brushes.Black, new Point(625 + age * 11, 505 - weight1)); //体重
                    e.Graphics.DrawString("•", font, Brushes.Blue, new Point(590 + age * 11, 512 - height1));
                    e.Graphics.DrawString("身高", font, Brushes.Black, new Point(593 + age * 11, 505 - height1)); //
                    e.Graphics.DrawString(dic["Hm"], font, Brushes.Black, new Point(625 + age * 11, 505 - height1)); //
                }
                else
                {
                    e.Graphics.DrawString("•", font, Brushes.Red, new Point(851+age*11, 512-weight1));
                    e.Graphics.DrawString("体重", font, Brushes.Black, new Point(854 + age * 11, 505 - weight1)); //体重
                    e.Graphics.DrawString(dic["Wk"], font, Brushes.Black, new Point(886 + age * 11, 505 - weight1)); //体重
                    e.Graphics.DrawString("•", font, Brushes.Blue, new Point(851 + age * 11, 512 - height1));
                    e.Graphics.DrawString("身高", font, Brushes.Black, new Point(854 + age * 11, 505 - height1)); //身高
                    e.Graphics.DrawString(dic["Hm"], font, Brushes.Black, new Point(886 + age * 11, 505 - height1)); //
                }
                //建议
                Comment comment = dbUtill.GetComment();
                if (bmi < 18.5)
                {
                    e.Graphics.DrawString(comment.low, font2, Brushes.Black, new Point(575, 610));
                }
                else if (bmi < 22.5)
                {
                    e.Graphics.DrawString(comment.mid, font2, Brushes.Black, new Point(575, 610));
                }
                else
                {
                    e.Graphics.DrawString(comment.hight, font2, Brushes.Black, new Point(575, 610));
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            CreateMC780_boy();
        }
        //保存报告图片
        public void CreateMC780_boy()
        {
            Dictionary<string, string> dic = checkDataUtill.GetDicByBackStr(checkData.jsonContent);
            Font font = new Font("宋体", 11f);
            Font font1 = new Font("宋体", 7f);
            Font font2 = new Font("宋体", 9f);
            Font font3 = new Font("宋体", 6f);
            int num = 0x317;
            int num1 = 0x45f;
            Image image = Image.FromFile(@"Resources/780_boy.jpg");
            Image image2 = new Bitmap(num1,num);
            Graphics g = Graphics.FromImage(image2);
            g.DrawImage(image, 0,0,num1,num);
            string studentName = dbUtill.GetStudentNameByID(checkData.studentId);
            string studentId = checkData.studentId;
            string activityId = checkData.activityId;
            try
            {
                g.DrawString(studentName, font, Brushes.Black, new Point(75, 72));   //姓名
                g.DrawString(dic["AG"], font, Brushes.Black, new Point(200, 72));   //年龄
                g.DrawString(dic["Da"], new Font("宋体", 11f), Brushes.Black, new Point(300, 72));  //日期
                string sex = dic["GE"];
                if (sex.Equals("1"))
                {
                    g.DrawString("√", font, Brushes.Black, new Point(476, 72)); //性别
                }
                else
                {
                    g.DrawString("√", font, Brushes.Black, new Point(511, 72));
                }
                g.DrawString(studentId, new Font("宋体", 11f), Brushes.Black, new Point(75, 103));  //Id
                g.DrawString(dic["Pt"], font, Brushes.Black, new Point(240, 103));   //着衣重量
                g.DrawString(dic["Hm"], font, Brushes.Black, new Point(330, 103));  //身高
                string bodytype = dic["Bt"];
                if (bodytype.Equals("0"))
                {
                    g.DrawString("√", font, Brushes.Black, new Point(437, 104)); //身体类型
                }
                else
                {
                    g.DrawString("√", font, Brushes.Black, new Point(457, 104));
                }
                g.DrawString(dic["Wk"], font, Brushes.Black, new Point(80, 191));  //体重
                g.DrawString(dic["FW"], font, Brushes.Black, new Point(80, 218));  //体脂率
                g.DrawString(dic["fW"], font, Brushes.Black, new Point(80, 245));  //体脂量
                g.DrawString(dic["MW"], font, Brushes.Black, new Point(80, 271));  //去脂体重
                g.DrawString(dic["wW"], font, Brushes.Black, new Point(80, 297));  //体水份量
                g.DrawString(dic["MI"], font, Brushes.Black, new Point(80, 325));  //BMI
                //参考值
                g.DrawString("30.22~38.11", font3, Brushes.Black, new Point(152, 193));  //体重
                g.DrawString("10~25.9", font3, Brushes.Black, new Point(152, 220));  //体脂率
                g.DrawString("2.84~8.95", font3, Brushes.Black, new Point(152, 247));  //体脂量
                g.DrawString("27.38~29.16", font3, Brushes.Black, new Point(152, 273));  //去脂体重
                g.DrawString("17.1~18.525", font3, Brushes.Black, new Point(152, 299));  //体水份量
                g.DrawString("13.6~18.6", font3, Brushes.Black, new Point(152, 327));  //BMI
                //
                g.DrawString(dic["mW"], font, Brushes.Black, new Point(130, 365));  //肌肉量
                g.DrawString(dic["bW"], font, Brushes.Black, new Point(130, 398));  //推定骨量
                g.DrawString(dic["rB"], font, Brushes.Black, new Point(79, 450));  // 基础代谢率

                g.DrawString(dic["ml"], font1, Brushes.Black, new Point(60, 630));  //左上肢
                g.DrawString(dic["mr"], font1, Brushes.Black, new Point(220, 630));  //BMI
                g.DrawString(dic["mL"], font1, Brushes.Black, new Point(60, 710));  //BMI
                g.DrawString(dic["mR"], font1, Brushes.Black, new Point(220, 710));  //BMI
                g.DrawString(dic["mT"], font1, Brushes.Black, new Point(220, 680));  //躯干
                //肌肉平衡
                g.DrawString(dic["mr"], font1, Brushes.Black, new Point(330, 420));  //左上肢
                g.DrawString(dic["Mr"], font1, Brushes.Black, new Point(490, 420));  //BMI
                g.DrawString(dic["mR"], font1, Brushes.Black, new Point(330, 480));  //BMI
                g.DrawString(dic["MR"], font1, Brushes.Black, new Point(490, 480));  //BMI
                //各部分脂肪
                g.DrawString(dic["Fr"], font1, Brushes.Black, new Point(510, 622));  //右上肢
                g.DrawString(dic["fr"], font1, Brushes.Black, new Point(510, 634));  //
                g.DrawString(dic["Fl"], font1, Brushes.Black, new Point(340, 625));  //左上肢
                g.DrawString(dic["fl"], font1, Brushes.Black, new Point(340, 637));  //
                g.DrawString(dic["FR"], font1, Brushes.Black, new Point(518, 712));  //右下肢
                g.DrawString(dic["fR"], font1, Brushes.Black, new Point(518, 724));  //
                g.DrawString(dic["FL"], font1, Brushes.Black, new Point(335, 708));  //左下肢
                g.DrawString(dic["fL"], font1, Brushes.Black, new Point(335, 720));  //
                g.DrawString(dic["FT"], font1, Brushes.Black, new Point(515, 679));  //躯干部
                g.DrawString(dic["fT"], font1, Brushes.Black, new Point(515, 691));  //
                //
                g.DrawString(dic["fW"], font1, Brushes.Black, new Point(380, 175));  //体脂量
                g.DrawString(dic["bW"], font1, Brushes.Black, new Point(440, 175));  //推定骨量
                g.DrawString(dic["Wk"], font1, Brushes.Black, new Point(318, 330));  //体重
                g.DrawString(dic["MW"], font1, Brushes.Black, new Point(378, 330));  //非脂肪量
                g.DrawString(dic["mW"], font1, Brushes.Black, new Point(438, 330));  //肌肉量
                g.DrawString(dic["wW"], font1, Brushes.Black, new Point(500, 330));  //体水份量
                int age = int.Parse(dic["AG"]);
                double weight = double.Parse(dic["Wk"]) * 3.2;
                double height = double.Parse(dic["Hm"]) * 2.08;
                int weight1 = Convert.ToInt32(weight);
                int height1 = Convert.ToInt32(height);
                if (sex.Equals("0"))
                {
                    g.DrawString("•", font, Brushes.Red, new Point(591 + age * 11, 512 - weight1)); //体重
                    g.DrawString("体重", font, Brushes.Black, new Point(593 + age * 11, 505 - weight1)); //体重
                    g.DrawString(dic["Wk"], font, Brushes.Black, new Point(625 + age * 11, 505 - height1)); //
                    g.DrawString("•", font, Brushes.Blue, new Point(591 + age * 11, 512 - height1));
                    g.DrawString("身高", font, Brushes.Black, new Point(593 + age * 11, 505 - height1));
                    g.DrawString(dic["Hm"], font, Brushes.Black, new Point(625 + age * 11, 505 - height1)); //
                }
                else
                {
                    g.DrawString("•", font, Brushes.Red, new Point(851 + age * 11, 512 - weight1));
                    g.DrawString("体重", font, Brushes.Black, new Point(855 + age * 11, 505 - weight1)); //体重
                    g.DrawString(dic["Wk"], font, Brushes.Black, new Point(895 + age * 11, 505 - weight1)); //体重
                    g.DrawString("•", font, Brushes.Blue, new Point(851 + age * 11, 512 - height1));
                    g.DrawString("身高", font, Brushes.Black, new Point(855 + age * 11, 505 - height1)); //身高
                    g.DrawString(dic["Hm"], font, Brushes.Black, new Point(895 + age * 11, 505 - height1)); //
                }
                g.DrawString(dic["MI"], font, Brushes.Black, new Point(79, 500));  // 肥胖指数
                double bmi = double.Parse(dic["MI"]);
                int bmi_i = (int)bmi;
                int b = 10 * (bmi_i - 16);
                Rectangle rectangle = new Rectangle(125, 495, 80 + b, 10);
                g.FillRectangle(Brushes.Black, rectangle);
                //建议
                Comment comment = dbUtill.GetComment();
                if (bmi < 18.5)
                {
                    g.DrawString(comment.low, font2, Brushes.Black, new Point(575, 610));
                }
                else if (bmi < 22.5)
                {
                    g.DrawString(comment.mid, font2, Brushes.Black, new Point(575, 610));
                }
                else
                {
                    g.DrawString(comment.hight, font2, Brushes.Black, new Point(575, 610));
                }
                string url = dbUtill.GetImageUrl();
                string activityName = dbUtill.GetActivitieNameByID(checkData.activityId);
                string res = dbUtill.GetStudentInfoByStudentId(checkData.studentId);
                string[] resArray = res.Split(',');
                string gradeName = resArray[1];
                string className = resArray[0];
                if (!Directory.Exists(@url + "\\"+activityName+"\\"+gradeName+"\\"+className+"\\"))
                {
                    Directory.CreateDirectory(@url + "\\" + activityName+"\\" + gradeName + "\\" + className + "\\");
                }
                image2.Save(@url + "\\" + activityName + "\\"+gradeName+"\\"+className + "\\" + studentId+"_"+studentName+".jpg");
            }
            catch(Exception ex)
            {
                MessageBox.Show("错误,因为" + ex.Message, "提示");
            }
            }
        private void Teacher_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("宋体", 11f);
            Font font1 = new Font("宋体", 7f);
            Font font2 = new Font("宋体", 9f);
            Dictionary<string, string> dic = checkDataUtill.GetDicByBackStr(teacherCheckData.JsonContent);
            Image image = Image.FromFile(@"Resources/420.jpg");
            int num = 0x317;
            int num1 = 0x45f;
            e.Graphics.DrawImage(image, 0, 0, num, num1);
            try
            {
                e.Graphics.DrawString(teacherCheckData.memberName, font, Brushes.Black, new Point(95, 102));   //姓名
                e.Graphics.DrawString(dic["AG"], font, Brushes.Black, new Point(220, 102));   //年龄
                e.Graphics.DrawString(dic["Da"], font, Brushes.Black, new Point(350, 102));  //日期
                string sex = dic["GE"];
                if (sex.Equals("2"))
                {
                    e.Graphics.DrawString("√", font, Brushes.Black, new Point(691, 102)); //性别
                }
                else
                {
                    e.Graphics.DrawString("√", font, Brushes.Black, new Point(681, 102)); //男
                }
                e.Graphics.DrawString(dic["Pt"], font, Brushes.Black, new Point(100, 137));   //着衣重量
                e.Graphics.DrawString(dic["Hm"], font, Brushes.Black, new Point(207, 137));  //身高
                string bodytype = dic["Bt"];
                if (bodytype.Equals("0"))
                {
                    e.Graphics.DrawString("√", font, Brushes.Black, new Point(325, 139)); //身体类型
                }
                else
                {
                    e.Graphics.DrawString("√", font, Brushes.Black, new Point(425, 139));
                }

                e.Graphics.DrawString(dic["Wk"], font, Brushes.Black, new Point(180, 208));  //体重
                e.Graphics.DrawString(dic["FW"], font, Brushes.Black, new Point(180, 232));  //体脂率
                e.Graphics.DrawString(dic["fW"], font, Brushes.Black, new Point(180, 257));  //体脂量
                e.Graphics.DrawString(dic["MW"], font, Brushes.Black, new Point(180, 282));  //非脂肪量
                e.Graphics.DrawString(dic["mW"], font, Brushes.Black, new Point(180, 310));  //肌肉量
                e.Graphics.DrawString(dic["bW"], font, Brushes.Black, new Point(180, 335));  //推定骨量
                e.Graphics.DrawString(dic["wW"], font, Brushes.Black, new Point(180, 362));  //体水分量
                e.Graphics.DrawString(dic["ww"], font, Brushes.Black, new Point(180, 387));  //体水分率
                //参考值
                e.Graphics.DrawString("43.3.22~58.3", font1, Brushes.Black, new Point(290, 208));  //体重
                e.Graphics.DrawString("21.0~34.9", font1, Brushes.Black, new Point(290, 232));  //体脂率
                e.Graphics.DrawString("9.6~19.5", font1, Brushes.Black, new Point(290, 257));  //体脂量
                e.Graphics.DrawString("33.7~38.8", font1, Brushes.Black, new Point(290, 282));  
                e.Graphics.DrawString("25.2±2.3", font1, Brushes.Black, new Point(290, 310));  
                e.Graphics.DrawString("2.2", font1, Brushes.Black, new Point(290, 335));  
                e.Graphics.DrawString("24.1~32.1", font1, Brushes.Black, new Point(290, 362));
                e.Graphics.DrawString("45~60", font1, Brushes.Black, new Point(290, 387));  //体水分率

                e.Graphics.DrawString(dic["rB"], font, Brushes.Black, new Point(88, 428));  // 基础代谢率
                e.Graphics.DrawString("1096", font, Brushes.Black, new Point(88, 450));  //基础代谢率参考值
                e.Graphics.DrawString(dic["MI"], font, Brushes.Black, new Point(480, 440));  //BMI
                e.Graphics.DrawString(dic["rA"], new Font("宋体",22), Brushes.Black, new Point(235,1010));  //代谢年龄
                e.Graphics.DrawString(dic["OV"], font, Brushes.Black, new Point(470,537));  //肥胖程度
                e.Graphics.DrawString(dic["sW"], font, Brushes.Black, new Point(120, 555));  //肌肉等级
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
        public void CreateTeacherPrint(int i)
        {
            Font font = new Font("宋体", 11f);
            Font font1 = new Font("宋体", 7f);
            Font font2 = new Font("宋体", 9f);
            int num = 0x317;
            int num1 = 0x45f;
            Dictionary<string, string> dic = checkDataUtill.GetDicByBackStr(teacherCheckData.JsonContent);
            Image image = Image.FromFile(@"Resources/420.jpg");
            Image image2 = new Bitmap(num, num1);
            Graphics g = Graphics.FromImage(image2);
            g.DrawImage(image, 0, 0, num, num1);
            try
            {
                g.DrawString(teacherCheckData.memberName, font, Brushes.Black, new Point(85, 101));   //姓名
                g.DrawString(dic["AG"], font, Brushes.Black, new Point(222, 101));   //年龄
                g.DrawString(teacherCheckData.checkDate, font, Brushes.Black, new Point(345, 101));  //日期
                string sex = dic["GE"];
                if (sex.Equals("1"))
                {
                    g.DrawString("√", font, Brushes.Black, new Point(680, 101)); //男
                }
                else
                {
                    g.DrawString("√", font, Brushes.Black, new Point(718, 101)); //女
                }
                g.DrawString(dic["Pt"], font, Brushes.Black, new Point(100, 138));   //着衣重量
                g.DrawString(dic["Hm"], font, Brushes.Black, new Point(205, 138));  //身高
                string bodytype = dic["Bt"];
                if (bodytype.Equals("0"))
                {
                    g.DrawString("√", font, Brushes.Black, new Point(325, 138)); //身体类型
                }
                else
                {
                    g.DrawString("√", font, Brushes.Black, new Point(425, 138));
                }

                g.DrawString(dic["Wk"], font, Brushes.Black, new Point(180, 208));  //体重
                g.DrawString(dic["FW"], font, Brushes.Black, new Point(180, 232));  //体脂率
                g.DrawString(dic["fW"], font, Brushes.Black, new Point(180, 257));  //体脂量
                g.DrawString(dic["MW"], font, Brushes.Black, new Point(180, 282));  //非脂肪量
                g.DrawString(dic["mW"], font, Brushes.Black, new Point(180, 308));  //肌肉量
                g.DrawString(dic["bW"], font, Brushes.Black, new Point(180, 335));  //推定骨量
                g.DrawString(dic["wW"], font, Brushes.Black, new Point(180, 360));  //体水分量
                g.DrawString(dic["ww"], font, Brushes.Black, new Point(180, 386));  //体水分率

                g.DrawString("43.3.22~58.3", font1, Brushes.Black, new Point(290, 210));  //体重
                g.DrawString("21.0~34.9", font1, Brushes.Black, new Point(290, 235));  //体脂率
                g.DrawString("9.6~19.5", font1, Brushes.Black, new Point(290, 260));  //体脂量
                g.DrawString("33.7~38.8", font1, Brushes.Black, new Point(290, 288));
                g.DrawString("25.2±2.3", font1, Brushes.Black, new Point(290, 310));
                g.DrawString("2.2", font1, Brushes.Black, new Point(290, 336));
                g.DrawString("24.1~32.1", font1, Brushes.Black, new Point(290, 362));
                g.DrawString("45~60", font1, Brushes.Black, new Point(290, 387));  //体水分率
                g.DrawString("1096", font, Brushes.Black, new Point(88, 450));  //基础代谢率参考值
                g.DrawString(dic["MI"], font, Brushes.Black, new Point(480, 440));  //BMI
                string url = dbUtill.GetImageUrl();
                if (!Directory.Exists(@url + "\\"))
                {
                    Directory.CreateDirectory(@url + "\\");
                }
                image2.Save(@url + "\\" +i+"_"+teacherCheckData.memberName+".jpg");
            }
            catch (Exception)
            {

            }
        }
    }
}
