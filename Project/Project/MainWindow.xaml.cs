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
        public MainWindow()
        {
            InitializeComponent();
        }
        //Критерий оценки ресурса сварных соединений паропроводов
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double R1 = Double.Parse(textBox.Text);
                double R2 = Double.Parse(textBox1.Text);
                double Rt1 = Double.Parse(textBox2.Text);
                double Rt2 = Double.Parse(textBox3.Text);
                double k = (R1 * R2) / (Rt1 * Rt2);
                label2.Content = "K=" + k.ToString();
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
                result.Content = "=" + Fc.ToString();
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
                double W0 = Double.Parse(textBox12.Text);
                double Wf = Double.Parse(textBox11.Text);
                double Wr = Double.Parse(textBox10.Text);
                double y = Double.Parse(textBox13.Text);
                double Kf = 0;
                Kf = ((Wr - W0) / (Wf - W0)) * (Wf / Wr) * y;
                label10.Content = "=" + Kf.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
