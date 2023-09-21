using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class APUpDownInteger : UserControl, INotifyPropertyChanged
    {
        private Color _textBackgroundColor;
        public Color TextBackgroundColor
        { get { return _textBackgroundColor; } set { _textBackgroundColor = value; tbValue.Background = new SolidColorBrush(value); } }

        private Color _textForegroundColor;
        public Color TextForegroundColor
        { get { return _textForegroundColor; } set { _textForegroundColor = value; tbValue.Foreground = new SolidColorBrush(value); } }

        private Color _upButtonForeground;
        public Color UpButtonForeground
        { get { return _upButtonForeground; } set { _upButtonForeground = value; Up.Foreground = new SolidColorBrush(value); } }

        private Color _upButtonBackground;
        public Color UpButtonBackground
        { get { return _upButtonForeground; } set { _upButtonForeground = value; Up.Background = new SolidColorBrush(value); } }

        private Color _downButtonForeground;
        public Color DownButtonForeground
        { get { return _downButtonForeground; } set { _downButtonForeground = value; Down.Foreground = new SolidColorBrush(value); } }

        private Color _downButtonBackground;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Color DownButtonBackground
        { get { return _upButtonForeground; } set { _upButtonForeground = value; Down.Background = new SolidColorBrush(value); } }

        private int _value;
        public int Value 
        {
            get { return _value; } 
            set { _value = value; OnPropertyChanged(nameof(Value)); }
        }

        public int MaxValue { get; set; } = int.MaxValue;
        public int MinValue { get; set; } = int.MinValue;
        public int Step { get; set; }
        public APUpDownInteger()
        {
            InitializeComponent();
            Value = 0;
            Step = 1;
            DataContext = this;
        }
        // Dependency Property for Value
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(APUpDownDouble), new PropertyMetadata(0.0));
        private void Up_Click(object sender, RoutedEventArgs e)
        {
            Value=Value+Step>MaxValue? Value : Value+Step;
            tbValue.Text = Value.ToString();
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            Value=Value+Step<MinValue? Value: Value-Step;
            tbValue.Text = Value.ToString();
        }
        private void tbValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = 0;
            if (int.TryParse(tbValue.Text, out result))
            {
                Value = result>MaxValue? Value : result < MinValue? Value : result ;
            }
            tbValue.Text = Value.ToString();
        }
    }
}