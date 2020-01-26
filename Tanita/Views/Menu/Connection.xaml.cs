using MahApps.Metro.Controls;
using System;
using System.IO.Ports;
using System.Windows;
using Tanita.model;
using Tanita.Utill;

namespace Tanita.Views.Menu
{
    /// <summary>
    /// Connection.xaml 的交互逻辑
    /// </summary>
    public partial class Connection : MetroWindow
    {
        string eqptType = "";
        public Connection()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            string[] s = SerialPort.GetPortNames();
            foreach (var s1 in s)
            {
                cbbComList.Items.Add(s1);
            }
            if (cbbComList.Items.Count > 0)
            {
                cbbComList.SelectedIndex = 0;
            }
            if(PortConfig.serialPort1.IsOpen == true)
            {
                btnOpen.Content = "已连接!";
                btnOpen.Background = System.Windows.Media.Brushes.Green;
                btnOpen.Foreground = System.Windows.Media.Brushes.White;
            }
        }
        //连接设备
        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            if (cbbComList.Items.Count <= 0)
            {
                MessageBox.Show("没有发现串口,请检查线路!", "提示");
                return;
            }
            if (PortConfig.serialPort1.IsOpen == false)
            {
                PortConfig.serialPort1.PortName = cbbComList.SelectedItem.ToString();
                PortConfig.serialPort1.BaudRate = 9600;
                PortConfig.serialPort1.Parity = Parity.None;
                PortConfig.serialPort1.DataBits = 8;
                PortConfig.serialPort1.StopBits = StopBits.One;

                Port.modelId = eqptType;
                Port.PortName = cbbComList.SelectedItem.ToString();
                try
                {
                    PortConfig.serialPort1.Open();
                    btnOpen.Content = "连接成功!";
                    btnOpen.Background = System.Windows.Media.Brushes.Green;
                    btnOpen.Foreground = System.Windows.Media.Brushes.White;
                    PortConfig.IsOpen = true;
                    DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "连接错误!");
                    return;
                }
            }
            else
            {
                try
                {
                    PortConfig.serialPort1.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误");
                }
                btnOpen.Content = "打开串口";
            }
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            eqptType = "DC-430MA";
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            eqptType = "MC-780MA";
        }
    }
}
