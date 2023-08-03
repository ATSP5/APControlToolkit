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

namespace APControlToolkit
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class APUpIntegerDownButton : UserControl
    {
        public int Value { get; set; }
        public APUpIntegerDownButton()
        {
            InitializeComponent();
            Value = 0;
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            Value++;
            tbValue.Text = Value.ToString();
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            Value--;
            tbValue.Text = Value.ToString();
        }

        

        private void tbValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = 0;
            if (int.TryParse(tbValue.Text, out result))
            {
                Value = result;
            }
            tbValue.Text = Value.ToString();
        }
    }
}