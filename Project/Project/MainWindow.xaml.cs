using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public MainWindow()
        {
            InitializeComponent();
            client.GetlistSensorCompleted += Client_GetlistSensorCompleted;
            client.GetlistSensorAsync();
            client.GetlistSteelM1Completed += Client_GetlistSteelM1Completed;
            
            client.GetlistSensor_SteelM1Completed += Client_GetlistSensor_SteelM1Completed;
            client.GetlistSteelM3Completed += Client_GetlistSteelM3Completed;
            client.GetlistSensor_SteelM3Completed += Client_GetlistSensor_SteelM3Completed;

        }

        private void Client_GetlistSensor_SteelM3Completed(object sender, ServiceReference1.GetlistSensor_SteelM3CompletedEventArgs e)
        {
            if (e.Error == null)
            {
               
                combobox6.ItemsSource = e.Result;
                combobox7.ItemsSource = e.Result;
            }

            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_GetlistSteelM3Completed(object sender, ServiceReference1.GetlistSteelM3CompletedEventArgs e)
        {
            if (e.Error == null)
            {
                combobox5.ItemsSource = e.Result;

            }

            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_GetlistSensor_SteelM1Completed(object sender, ServiceReference1.GetlistSensor_SteelM1CompletedEventArgs e)
        {
            if (e.Error == null)
            {
               
                comboBox2.ItemsSource = e.Result;
                comboBox3.ItemsSource = e.Result;
            }

            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_GetlistSteelM1Completed(object sender, ServiceReference1.GetlistSteelM1CompletedEventArgs e)
        {
            if (e.Error == null)
            {
               
                comboBox1.ItemsSource = e.Result;

            }

            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_GetlistSensorCompleted(object sender, ServiceReference1.GetlistSensorCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                
                comboBox.ItemsSource = e.Result;
                combobox4.ItemsSource = e.Result;
            }

            else
                MessageBox.Show(e.Error.Message);
        }

        //Критерий оценки ресурса сварных соединений паропроводов
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ServiceReference1.SensorSteel temp_R01 = comboBox2.SelectedItem as ServiceReference1.SensorSteel;
                ServiceReference1.SensorSteel temp_R02 = comboBox3.SelectedItem as ServiceReference1.SensorSteel;
                double R1 = temp_R01.R01;
                double R2 = temp_R02.R02;
                double Rt1 = Double.Parse(textBox2.Text);
                double Rt2 = Double.Parse(textBox3.Text);
                double k = (R1 * R2) / (Rt1 * Rt2);
                label2.Text = "K=" +Math.Round(k,5).ToString();
                if (k < 0.98)
                {

                    label_anwer.Text = "металл сварных соединений выработал свой ресурс и необходимо выполнение ремонтно-восстановительных мероприятий";

                }
                else
                {
                    label_anwer.Text = "металл сварных соединений может работать без проведения ремонтно-восстановительных работ.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //Критерий оценки ресурса сварных барабанов котлов
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(textBox7.Text!="" && textBox9.Text != "")
                {
                    double r0 = Double.Parse(textBox7.Text);
                    double r = Double.Parse(textBox9.Text);
                    double dR1per = Double.Parse(textBox4.Text);
                    double dR1par = Double.Parse(textBox8.Text);
                    double dR2per = Double.Parse(textBox5.Text);
                    double dR2par = Double.Parse(textBox6.Text);
                    double dR1 = 0;
                    double dR2 = 0;
                    double Kc = 0;
                    double y = 0;
                    double Fc = 0;
                    dR1 = Math.Abs(dR1per - dR1par);
                    dR2 = Math.Abs(dR2per - dR2par);
                    Kc = dR1 / dR2;
                    y = (r0 - r) / r;
                    Fc = y * Math.Pow(Kc, -1);
                    result.Text = "Fc=" + Math.Round(Fc, 5).ToString();
                    if (Fc > 0.22 && Kc > 0.64 && Kc < 1 && y>0.13)
                    {
                        anwer2.Text = "Металл находится в критическом состоянии. Необходимо проведение ремонтно-восстановительных мероприятий.";
                    }
                    else
                    {
                        anwer2.Text = "Металл находится в рабочем состоянии";
                    }
                }
                else
                {
                    double dR1per = Double.Parse(textBox4.Text);
                    double dR1par = Double.Parse(textBox8.Text);
                    double dR2per = Double.Parse(textBox5.Text);
                    double dR2par = Double.Parse(textBox6.Text);
                    double dR1 = 0;
                    double dR2 = 0;
                    double Kc = 0;
                    double y = 0;
                    double Fc = 0;
                    dR1 = Math.Abs(dR1per - dR1par);
                    dR2 = Math.Abs(dR2per - dR2par);
                    Kc = dR1 / dR2;
                    Fc = Math.Pow(Kc, -1);
                    result.Text = "Fc=" + Math.Round(Fc, 5).ToString();
                    if(Fc>0.22 && Kc>0.64 && Kc < 1)
                    {
                        anwer2.Text = "Металл находится в критическом состоянии. Необходимо проведение ремонтно-восстановительных мероприятий.";
                    }
                    else
                    {
                        anwer2.Text = "Металл находится в рабочем состоянии";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ServiceReference1.SensorSteelM3 temp_R01 = combobox6.SelectedItem as ServiceReference1.SensorSteelM3;
                ServiceReference1.SensorSteelM3 temp_R02 = combobox7.SelectedItem as ServiceReference1.SensorSteelM3;
                double W0 = temp_R01.W0;
                double Wf = temp_R02.Wf;
                double Wr = Double.Parse(textBox10.Text);
                double y = Double.Parse(textBox13.Text);
                double Kf = 0;
                Kf = ((Wr - W0) / (Wf - W0)) * (Wf / Wr) * y;
                label10.Text = "= " + Math.Round(Kf, 5).ToString();
                if(Kf>0.7 && Kf < 0.9)
                {
                    anwer3.Text = "Металл достиг предельного состояния.";
                }
                else
                {
                    anwer3.Text = "Металл контролируемого элемента может работать без проведения ремонтно-восстановительных работ.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceReference1.Sensors temp_Sensors = comboBox.SelectedItem as ServiceReference1.Sensors;
            ServiceReference1.Steels temp_Steel = comboBox1.SelectedItem as ServiceReference1.Steels;
            //if (temp_Sensors != null && temp_Steel!=null)
            //{
            //    client.GetlistSensor_SteelAsync(temp_Sensors.id_sensor, temp_Steel.id_steel);
            //}
            //else
            {
                client.GetlistSteelM1Async(temp_Sensors.id_sensor);
            }
            
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceReference1.Sensors temp_Sensors = comboBox.SelectedItem as ServiceReference1.Sensors;
            ServiceReference1.Steels temp_Steel = comboBox1.SelectedItem as ServiceReference1.Steels;
            if (temp_Sensors != null && temp_Steel != null)
            {
                client.GetlistSensor_SteelM1Async(temp_Sensors.id_sensor, temp_Steel.id_steel);
            }
        }

        private void combobox4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceReference1.Sensors temp_Sensors = combobox4.SelectedItem as ServiceReference1.Sensors;
            ServiceReference1.Steels temp_Steel = combobox5.SelectedItem as ServiceReference1.Steels;
            client.GetlistSteelM3Async(temp_Sensors.id_sensor);
            
        }

        private void combobox5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceReference1.Sensors temp_Sensors = combobox4.SelectedItem as ServiceReference1.Sensors;
            ServiceReference1.Steels temp_Steel = combobox5.SelectedItem as ServiceReference1.Steels;
            if (temp_Sensors != null && temp_Steel != null)
            {
                client.GetlistSensor_SteelM3Async(temp_Sensors.id_sensor, temp_Steel.id_steel);
            }
        }
    }
}
