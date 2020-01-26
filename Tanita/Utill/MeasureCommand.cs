using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows;

namespace Tanita.Utill
{
    class MeasureCommand
    {
        public void Measure(SerialPort ComDevice,string eqptType,string sex,string age,string height)
        {
            if (eqptType == null||eqptType.Equals("")){
                MessageBox.Show("未选择使用的仪器型号,请选择后重试", "错误");
            }
            else
            {
                if (eqptType.Equals("DC-430MA"))
                {
                    byte[] t = new byte[4];  //设置仪器模式为PC模式
                    t[0] = 0x4D;
                    t[1] = 0x31;
                    t[2] = 0x0D;
                    t[3] = 0x0A;
                    ComDevice.Write(t, 0, 4);
                    Thread.Sleep(1000);
                    byte[] t1 = new byte[5]; //设置被检测人性别 1为男 2为女
                    byte[] sex_byte = Encoding.Default.GetBytes(sex);
                    t1[0] = 0x44;
                    t1[1] = 0x31;
                    t1[2] = sex_byte[0];
                    t1[3] = 0x0D;
                    t1[4] = 0x0A;
                    ComDevice.Write(t1, 0, 5);
                    Thread.Sleep(1000);
                    byte[] t2 = new byte[5]; //设置被测人类型   0 普通 2 运动员
                    t2[0] = 0x44;
                    t2[1] = 0x32;
                    t2[2] = 0x30;
                    t2[3] = 0x0D;
                    t2[4] = 0x0A;
                    ComDevice.Write(t2, 0, 5);
                    Thread.Sleep(1000);
                    byte[] t3 = new byte[9]; //身高
                    byte[] height_byte = Encoding.Default.GetBytes(height);
                    t3[0] = 0x44;
                    t3[1] = 0x33;
                    t3[2] = height_byte[0];
                    t3[3] = height_byte[1];
                    t3[4] = height_byte[2];
                    t3[5] = height_byte[3];
                    t3[6] = height_byte[4];
                    t3[7] = 0x0D;
                    t3[8] = 0x0A;
                    ComDevice.Write(t3, 0, 9);
                    Thread.Sleep(1000);
                    byte[] t4 = new byte[6]; //年龄
                    byte[] age_byte = Encoding.Default.GetBytes(age);
                    t4[0] = 0x44;
                    t4[1] = 0x34;
                    t4[2] = age_byte[0];
                    t4[3] = age_byte[1];
                    t4[4] = 0x0D;
                    t4[5] = 0x0A;
                    ComDevice.Write(t4, 0, 6);
                    Thread.Sleep(1000);
                    byte[] t6 = new byte[4];    //检测体脂
                    t6[0] = 0x47;
                    t6[1] = 0x30;
                    t6[2] = 0x0D;
                    t6[3] = 0x0A;
                    ComDevice.Write(t6, 0, 4);
                }
                else if (eqptType.Equals("MC-780MA"))
                {
                    byte[] t = new byte[4];  //设置仪器模式为PC模式
                    t[0] = 0x4D;
                    t[1] = 0x31;
                    t[2] = 0x0D;
                    t[3] = 0x0A;
                    ComDevice.Write(t, 0, 4);
                    Thread.Sleep(1000);
                    byte[] t1 = new byte[5]; //设置被检测人性别 
                    byte[] b = Encoding.Default.GetBytes(sex);
                    t1[0] = 0x44;
                    t1[1] = 0x31;
                    t1[2] = b[0];
                    t1[3] = 0x0D;
                    t1[4] = 0x0A;
                    ComDevice.Write(t1, 0, 5);
                    Thread.Sleep(1000);
                    byte[] t2 = new byte[5]; //设置被测人类型   0 普通 2 运动员
                    t2[0] = 0x44;
                    t2[1] = 0x32;
                    t2[2] = 0x30;
                    t2[3] = 0x0D;
                    t2[4] = 0x0A;
                    ComDevice.Write(t2, 0, 5);
                    Thread.Sleep(1000);
                    byte[] t3 = new byte[9]; //身高
                    byte[] height_byte = Encoding.Default.GetBytes(height);
                    t3[0] = 0x44;
                    t3[1] = 0x33;
                    t3[2] = height_byte[0];
                    t3[3] = height_byte[1];
                    t3[4] = height_byte[2];
                    t3[5] = height_byte[3];
                    t3[6] = height_byte[4];
                    t3[7] = 0x0D;
                    t3[8] = 0x0A;
                    ComDevice.Write(t3, 0, 9);
                    Thread.Sleep(1000);
                    byte[] t4 = new byte[6]; //年龄
                    byte[] age_byte = Encoding.Default.GetBytes(age);
                    t4[0] = 0x44;
                    t4[1] = 0x34;
                    t4[2] = age_byte[0];
                    t4[3] = age_byte[1];
                    t4[4] = 0x0D;
                    t4[5] = 0x0A;
                    ComDevice.Write(t4, 0, 6);
                    Thread.Sleep(1000);
                    byte[] t6 = new byte[3];    //检测体脂
                    t6[0] = 0x47;
                    t6[1] = 0x0D;
                    t6[2] = 0x0A;
                    ComDevice.Write(t6, 0, 3);
                }
            }
        }
    }
}
