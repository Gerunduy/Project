using System;
using System.Collections.Generic;
using System.IO;
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
        List<stel> list_stelM1 = new List<stel>();
        List<stel> list_stelM3 = new List<stel>();
        bool type_internet;
        public MainWindow()
        {
            InitializeComponent();
            client.GetlistSensorCompleted += Client_GetlistSensorCompleted;
            //client.GetlistSensorAsync();
            Waiting(true);
            client.GetlistSteelM1Completed += Client_GetlistSteelM1Completed;

            client.GetlistSensor_SteelM1Completed += Client_GetlistSensor_SteelM1Completed;
            client.GetlistSteelM3Completed += Client_GetlistSteelM3Completed;
            client.GetlistSensor_SteelM3Completed += Client_GetlistSensor_SteelM3Completed;

            client.GetConnectionCompleted += Client_GetConnectionCompleted;
            client.GetConnectionAsync();
           
            create_y();

        }
        public void readfiles()
        {
            FileStream file = new FileStream("1_module.txt", FileMode.Open); //создаем файловый поток
            StreamReader reader = new StreamReader(file); // создаем «потоковый читатель» и связываем его с файловым потоком
            string s = "";

            while (s != null)
            {
                s = reader.ReadLine();

                if (s == null)
                {
                    break;
                }
                string[] words = s.Split(new char[] { ';' });
                stel stel = new stel();
                stel.name_sensor = words[0];
                stel.name_steel = words[1];
                stel.list_R01 = new List<int>();
                stel.list_R02 = new List<int>();
                for (int i = 0; i < 5; i++)
                {
                    stel.list_R01.Add(int.Parse(words[2 + i]));
                }
                for (int i = 0; i < 5; i++)
                {
                    stel.list_R02.Add(int.Parse(words[7 + i]));
                }


                list_stelM1.Add(stel);

            }
            file.Close();
            comboBox.ItemsSource = list_stelM1.GroupBy(o => o.name_sensor);

            //читаем 3 модуль
             file = new FileStream("3_module.txt", FileMode.Open); //создаем файловый поток
            reader = new StreamReader(file); // создаем «потоковый читатель» и связываем его с файловым потоком
            s = "";

            while (s != null)
            {
                s = reader.ReadLine();

                if (s == null)
                {
                    break;
                }
                string[] words = s.Split(new char[] { ';' });
                stel stel = new stel();
                stel.name_sensor = words[0];
                stel.name_steel = words[1];
                stel.list_W0 = new List<int>();
                stel.list_Wf = new List<int>();
                for (int i = 0; i < 5; i++)
                {
                    stel.list_W0.Add(int.Parse(words[2 + i]));
                }
                for (int i = 0; i < 5; i++)
                {
                    stel.list_Wf.Add(int.Parse(words[7 + i]));
                }


                list_stelM3.Add(stel);

            }
            file.Close();
            

            combobox4.ItemsSource = list_stelM3.GroupBy(o => o.name_sensor);
            comboBox1.ItemsSource = null;
            combobox5.ItemsSource = null;
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

        public class stel
        {
            public string name_sensor { get; set; }
            public string name_steel { get; set; }
            public List<int> list_R01 { get; set; }
            public List<int> list_R02 { get; set; }
            public List<int> list_W0 { get; set; }
            public List<int> list_Wf { get; set; }
        }
        public class module_1
        {
            public List<String> list_sensors;
            public List<int> list;
        }
        private void Client_GetConnectionCompleted(object sender, ServiceReference1.GetConnectionCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                
                if (e.Result == true)
                {
                    type_internet = true;
                    combobox5.ItemsSource = null;
                    comboBox1.ItemsSource = null;
                    client.GetlistSensorAsync();
                    label_internet.Foreground = Brushes.Green;
                    label_internet.Content = "Соединение с Базой установлено";

                }
                else
                {

                    type_internet = false;
                    label_internet.Foreground = Brushes.Red;
                    label_internet.Content = "Нет доступа к Базе";
                    readfiles();

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
                if (e.Result == null)
                {
                    MessageBox.Show("Потеряно соединение с базой");
                    Waiting(true);
                    client.GetConnectionAsync();
                }
                else
                {
                    double srWo = Math.Round((double)e.Result.Sum(o => o.W0) / (double)e.Result.Count());
                    textboxW0.Text = srWo.ToString();
                    double srWf = Math.Round((double)e.Result.Sum(o => o.Wf) / (double)e.Result.Count());
                    textboxWf.Text = srWf.ToString();
                    
                }

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
                if (e.Result == null)
                {
                    MessageBox.Show("Потеряно соединение с базой");
                    Waiting(true);
                    client.GetConnectionAsync();
                }
                else
                {
                    textboxW0.Text = "";
                    textboxWf.Text = "";
                    combobox5.ItemsSource = e.Result;
                }
                
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
                if (e.Result == null)
                {
                    
                        MessageBox.Show("Потеряно соединение с базой");
                        Waiting(true);
                        client.GetConnectionAsync();
                    
                }
                else
                {
                    double srR01 = Math.Round((double)e.Result.Sum(o => o.R01) / (double)e.Result.Count());
                    textboxR01.Text = srR01.ToString();
                    double srR02 = Math.Round((double)e.Result.Sum(o => o.R02) / (double)e.Result.Count());
                    textboxR02.Text = srR02.ToString();
                    //comboBox2.ItemsSource = e.Result;
                    //comboBox3.ItemsSource = e.Result;
                    
                }
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
                if (e.Result ==null)
                {
                    
                        MessageBox.Show("Потеряно соединение с базой");
                        Waiting(true);
                        client.GetConnectionAsync();
                    
                }
                else
                {
                    comboBox1.ItemsSource = e.Result;
                }
                
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
        //расчет ответа в 3 модуле
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
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
        //изменение датчика в 1 модуле
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (type_internet == true)
            {
                ServiceReference1.Sensors temp_Sensors = comboBox.SelectedItem as ServiceReference1.Sensors;
               
                if(temp_Sensors!=null)
                {
                    client.GetlistSteelM1Async(temp_Sensors.id_sensor);
                    Waiting2(true);
                }
            }
            else
            {
                textboxR01.Text = "";
                textboxR02.Text = "";
                var stel = comboBox.SelectedItem as IGrouping<string, stel>;
                if (stel != null)
                {
                    string name = stel.Key;
                    comboBox1.ItemsSource = list_stelM1.Where(o => o.name_sensor == name).GroupBy(o => o.name_steel);
                }
             
            }
            
            
        }
        //изменение стали в 1 модуле
        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (type_internet == true)
            {
                ServiceReference1.Sensors temp_Sensors = comboBox.SelectedItem as ServiceReference1.Sensors;
                ServiceReference1.Steels temp_Steel = comboBox1.SelectedItem as ServiceReference1.Steels;
                if (temp_Sensors != null && temp_Steel != null)
                {
                    client.GetlistSensor_SteelM1Async(temp_Sensors.id_sensor, temp_Steel.id_steel);
                    Waiting2(true);
                }
            }
            else
            {
                string name_sensor="";
                string name_stel = "";
                var sensor = comboBox.SelectedItem as IGrouping<string, stel>;
                if (sensor != null)
                {
                    name_sensor = sensor.Key;
                }
                
                var stel = comboBox1.SelectedItem as IGrouping<string, stel>;
                if (stel != null)
                {
                    name_stel = stel.Key;
                }
                if(name_sensor!="" && name_stel != "")
                {
                    double R01 = Math.Round((double)list_stelM1.First(o => o.name_sensor == name_sensor && o.name_steel == name_stel).list_R01.Sum(o => o) / (double)list_stelM1.First(o => o.name_sensor == name_sensor && o.name_steel == name_stel).list_R01.Count());
                    double R02 = Math.Round((double)list_stelM1.First(o => o.name_sensor == name_sensor && o.name_steel == name_stel).list_R02.Sum(o => o) / (double)list_stelM1.First(o => o.name_sensor == name_sensor && o.name_steel == name_stel).list_R02.Count());
                    textboxR01.Text = R01.ToString();
                    textboxR02.Text = R02.ToString();
                }
                
            }
            
        }
        //изменение дачиков в 3 модуле
        private void combobox4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (type_internet == true)
            {
                ServiceReference1.Sensors temp_Sensors = combobox4.SelectedItem as ServiceReference1.Sensors;
                if(temp_Sensors != null)
                {
                    client.GetlistSteelM3Async(temp_Sensors.id_sensor);
                    Waiting2(true);
                }
               
            }
            else
            {
                textboxW0.Text = "";
                textboxWf.Text = "";
                var stel = combobox4.SelectedItem as IGrouping<string, stel>;
                if (stel != null)
                {
                    string name = stel.Key;
                    combobox5.ItemsSource = list_stelM3.Where(o => o.name_sensor == name).GroupBy(o => o.name_steel);
                }
               
            }

        }
        //изменение стали в 3 модуле
        private void combobox5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (type_internet == true)
            {
                ServiceReference1.Sensors temp_Sensors = combobox4.SelectedItem as ServiceReference1.Sensors;
                ServiceReference1.Steels temp_Steel = combobox5.SelectedItem as ServiceReference1.Steels;
                if (temp_Sensors != null && temp_Steel != null)
                {
                    client.GetlistSensor_SteelM3Async(temp_Sensors.id_sensor, temp_Steel.id_steel);
                    Waiting2(true);
                }
            }
            else
            {
                string name_sensor = "";
                string name_stel = "";
                var sensor = combobox4.SelectedItem as IGrouping<string, stel>;
                if (sensor != null)
                {
                    name_sensor = sensor.Key;
                }

                var stel = combobox5.SelectedItem as IGrouping<string, stel>;
                if (stel != null)
                {
                    name_stel = stel.Key;
                }
                if (name_sensor != "" && name_stel != "")
                {
                    double W0 = Math.Round((double)list_stelM3.First(o => o.name_sensor == name_sensor && o.name_steel == name_stel).list_W0.Sum(o => o) / (double)list_stelM3.First(o => o.name_sensor == name_sensor && o.name_steel == name_stel).list_W0.Count());
                    double Wf = Math.Round((double)list_stelM3.First(o => o.name_sensor == name_sensor && o.name_steel == name_stel).list_Wf.Sum(o => o) / (double)list_stelM3.First(o => o.name_sensor == name_sensor && o.name_steel == name_stel).list_Wf.Count());
                    textboxW0.Text = W0.ToString();
                    textboxWf.Text = Wf.ToString();
                }
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
        //справка по 1 модулю
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
        //справка по 2 модулю
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

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            client.GetConnectionAsync();
            Waiting(true);
        }
    }
}
