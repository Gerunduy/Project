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
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для reference.xaml
    /// </summary>
    static class D
    {
        public static int type_module { get; set; }
    }
    public partial class reference : Window
    {
       
        public reference()
        {
            InitializeComponent();
            if (D.type_module == 3)
            {
                image.Source= BitmapFrame.Create(new Uri("3_module.png", UriKind.Relative)); 
            }
            if (D.type_module == 1)
            {
                image.Source = BitmapFrame.Create(new Uri("1_module.png", UriKind.Relative));
            }
            if (D.type_module == 2)
            {
                image.Source = BitmapFrame.Create(new Uri("2_module.png", UriKind.Relative));
            }
            //image.Source = BitmapFrame.Create(new Uri("3_module.png", UriKind.Relative));
        }
    }
}
