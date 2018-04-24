﻿using System;
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
        reference _reference;
        public MainWindow()
        {
            InitializeComponent();
            client.GetlistSensorCompleted += Client_GetlistSensorCompleted;
            client.GetlistSensorAsync();
            Waiting(true);
            client.GetlistSteelM1Completed += Client_GetlistSteelM1Completed;
            
            client.GetlistSensor_SteelM1Completed += Client_GetlistSensor_SteelM1Completed;
            client.GetlistSteelM3Completed += Client_GetlistSteelM3Completed;
            client.GetlistSensor_SteelM3Completed += Client_GetlistSensor_SteelM3Completed;

            client.GetConnectionCompleted += Client_GetConnectionCompleted;
            //client.GetConnectionAsync();
            create_y();

        }

        public void create_y()
        {
            List<coefficient> list_y = new List<coefficient>();
            coefficient coefficient = new coefficient();
            coefficient.name_y = "исходное состояние";
            coefficient.y = 1;
            list_y.Add(coefficient);
            coefficient = new coefficient();
            coefficient.name_y = "25 тыс. часов";
            coefficient.y = 1.01;
            list_y.Add(coefficient);
            coefficient = new coefficient();
            coefficient.name_y = "50 тыс. часов";
            coefficient.y = 1.02;
            list_y.Add(coefficient);
            coefficient = new coefficient();
            coefficient.name_y = "75  тыс. часов";
            coefficient.y = 1.03;
            list_y.Add(coefficient);
            coefficient = new coefficient();
            coefficient.name_y = "100 тыс. часов";
            coefficient.y = 1.04;
            list_y.Add(coefficient);
            coefficient = new coefficient();
            coefficient.name_y = "125 тыс. часов";
            coefficient.y = 1.05;
            list_y.Add(coefficient);
            coefficient = new coefficient();
            coefficient.name_y = "150 тыс. часов";
            coefficient.y = 1.06;
            list_y.Add(coefficient);
            coefficient = new coefficient();
            coefficient.name_y = "150–200 тыс. часов";
            coefficient.y = 1.07;
            list_y.Add(coefficient);
            coefficient = new coefficient();
            coefficient.name_y = "200–250  тыс. часов";
            coefficient.y = 1.08;
            list_y.Add(coefficient);
            coefficient = new coefficient();
            coefficient.name_y = "более 250  тыс. часов";
            coefficient.y = 1.09;
            list_y.Add(coefficient);
            combobox8.ItemsSource = list_y;
        }
        public class coefficient
        {
            public double y { get; set; }
            public string name_y { get; set; }
        }


        private void Client_GetConnectionCompleted(object sender, ServiceReference1.GetConnectionCompletedEventArgs e)
        {
            if (e.Error == null)
            {

                if (e.Result == true)
                {

                }
                Waiting(false);
            }

            else
            {
                MessageBox.Show(e.Error.Message);
                Waiting(false);
            }
        }

        //========================================================================================
        private void Waiting(bool Wait)
        {
            if (Wait)
            {
                tabControl.Visibility = Visibility.Hidden;
                tblWait.Visibility = Visibility.Visible;
            }
            else
            {
                tabControl.Visibility = Visibility.Visible;
                tblWait.Visibility = Visibility.Hidden;
            }
        }
        private void Waiting2(bool Wait)
        {
            if (Wait)
            {
                tabControl.IsEnabled = false;
            }
            else
            {
                tabControl.IsEnabled = true;
            }
        }
        //========================================================================================

        private void Client_GetlistSensor_SteelM3Completed(object sender, ServiceReference1.GetlistSensor_SteelM3CompletedEventArgs e)
        {
            if (e.Error == null)
            {
                double srWo = Math.Round((double) e.Result.Sum(o => o.W0)/(double)e.Result.Count());
                textboxW0.Text = srWo.ToString();
                double srWf = Math.Round((double)e.Result.Sum(o => o.Wf) / (double)e.Result.Count());
                textboxWf.Text = srWf.ToString();
                //combobox6.ItemsSource = e.Result;
                //combobox7.ItemsSource = e.Result;
                Waiting2(false);
            }

            else
            {
                MessageBox.Show(e.Error.Message);
                Waiting2(false);
            }  
        }

        private void Client_GetlistSteelM3Completed(object sender, ServiceReference1.GetlistSteelM3CompletedEventArgs e)
        {
            if (e.Error == null)
            {
                textboxW0.Text = "";
                textboxWf.Text = "";
                combobox5.ItemsSource = e.Result;
                Waiting2(false);
            }

            else
            {
                MessageBox.Show(e.Error.Message);
                Waiting2(false);
            }        
        }

        private void Client_GetlistSensor_SteelM1Completed(object sender, ServiceReference1.GetlistSensor_SteelM1CompletedEventArgs e)
        {
            if (e.Error == null)
            {
                double srR01 =Math.Round( (double)e.Result.Sum(o => o.R01) / (double)e.Result.Count());
                textboxR01.Text = srR01.ToString();
                double srR02 = Math.Round((double) e.Result.Sum(o => o.R02) /(double) e.Result.Count());
                textboxR02.Text = srR02.ToString();
                //comboBox2.ItemsSource = e.Result;
                //comboBox3.ItemsSource = e.Result;
                Waiting2(false);
            }

            else
            {
                MessageBox.Show(e.Error.Message);
                Waiting2(false);
            }
        }

        private void Client_GetlistSteelM1Completed(object sender, ServiceReference1.GetlistSteelM1CompletedEventArgs e)
        {
            if (e.Error == null)
            {
                textboxR01.Text = "";
                textboxR02.Text = "";

                comboBox1.ItemsSource = e.Result;
                Waiting2(false);
            }

            else
            {
                MessageBox.Show(e.Error.Message);
                Waiting2(false);
            }  
        }

        private void Client_GetlistSensorCompleted(object sender, ServiceReference1.GetlistSensorCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                textboxR01.Text = "";
                textboxR02.Text = "";
                textboxW0.Text = "";
                textboxWf.Text = "";
                comboBox.ItemsSource = e.Result;
                combobox4.ItemsSource = e.Result;
                Waiting(false);
            }

            else
            {
                MessageBox.Show(e.Error.Message);
                Waiting(false);
            }
        }

        //Критерий оценки ресурса сварных соединений паропроводов
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //ServiceReference1.SensorSteel temp_R01 = comboBox2.SelectedItem as ServiceReference1.SensorSteel;
                //ServiceReference1.SensorSteel temp_R02 = comboBox3.SelectedItem as ServiceReference1.SensorSteel;
                //double R1 = temp_R01.R01;
                //double R2 = temp_R02.R02;
                double R1 = double.Parse(textboxR01.Text);
                double R2 = double.Parse(textboxR02.Text);
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
                //ServiceReference1.SensorSteelM3 temp_R01 = combobox6.SelectedItem as ServiceReference1.SensorSteelM3;
                //ServiceReference1.SensorSteelM3 temp_R02 = combobox7.SelectedItem as ServiceReference1.SensorSteelM3;
                //double W0 = temp_R01.W0;
                //double Wf = temp_R02.Wf;
                double W0 = double.Parse( textboxW0.Text);
                double Wf= double.Parse(textboxWf.Text);
                double Wr = Double.Parse(textBox10.Text);
                coefficient coefficient = combobox8.SelectedItem as coefficient;
                double y = coefficient.y;//Double.Parse(textBox13.Text);
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
                Waiting2(true);
            }
            
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceReference1.Sensors temp_Sensors = comboBox.SelectedItem as ServiceReference1.Sensors;
            ServiceReference1.Steels temp_Steel = comboBox1.SelectedItem as ServiceReference1.Steels;
            if (temp_Sensors != null && temp_Steel != null)
            {
                client.GetlistSensor_SteelM1Async(temp_Sensors.id_sensor, temp_Steel.id_steel);
                Waiting2(true);
            }
        }

        private void combobox4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceReference1.Sensors temp_Sensors = combobox4.SelectedItem as ServiceReference1.Sensors;
            ServiceReference1.Steels temp_Steel = combobox5.SelectedItem as ServiceReference1.Steels;
            client.GetlistSteelM3Async(temp_Sensors.id_sensor);
            Waiting2(true);

        }

        private void combobox5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceReference1.Sensors temp_Sensors = combobox4.SelectedItem as ServiceReference1.Sensors;
            ServiceReference1.Steels temp_Steel = combobox5.SelectedItem as ServiceReference1.Steels;
            if (temp_Sensors != null && temp_Steel != null)
            {
                client.GetlistSensor_SteelM3Async(temp_Sensors.id_sensor, temp_Steel.id_steel);
                Waiting2(true);
            }
        }
        //справка по 3 модулю
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            D.type_module = 3;
            _reference = new reference();
            _reference.Owner = this;
            
            if (_reference.ShowDialog() == true)
            {
                
            }
            else
            {
                //MessageBox.Show("запись не записана");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            D.type_module = 1;
            _reference = new reference();
            _reference.Owner = this;

            if (_reference.ShowDialog() == true)
            {

            }
            else
            {
                //MessageBox.Show("запись не записана");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            D.type_module = 2;
            _reference = new reference();
            _reference.Owner = this;

            if (_reference.ShowDialog() == true)
            {

            }
            else
            {
                //MessageBox.Show("запись не записана");
            }
        }
    }
}
