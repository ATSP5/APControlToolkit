using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    /// Logika interakcji dla klasy APUpDownDouble.xaml
    /// </summary>
    public partial class APUpDownDouble : UserControl, INotifyPropertyChanged
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
        public Color DownButtonBackground
        { get { return _upButtonForeground; } set { _upButtonForeground = value; Down.Background = new SolidColorBrush(value); } }

        private double _value;
        public double Value 
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged(nameof(Value)); }
        }

        public double MaxValue { get; set; } = double.MaxValue;
        public double MinValue { get; set; } = double.MinValue;
        public double Step { get; set; }

        private string _roundingFormat;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int RoundDigits { get; set; } = 1;

        // Get the current culture's NumberFormatInfo
        private NumberFormatInfo _formatInfo = CultureInfo.CurrentCulture.NumberFormat;

        public APUpDownDouble()
        {
            InitializeComponent();
            SetRoundingFormat();
            Value = 0.0;
            Step = 1.0;
            DataContext = this;
        }
        // Dependency Property for Value
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(APUpDownDouble), new PropertyMetadata());

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            SetRoundingFormat();
            Value = Value + Step > MaxValue ? Value : Value + Step;
            tbValue.Text = Value.ToString(_roundingFormat);
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            SetRoundingFormat();
            Value = Value + Step < MinValue ? Value : Value - Step;
            tbValue.Text = Value.ToString(_roundingFormat);
        }
        private void tbValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            var valueText = tbValue.Text.ToString();
            if (!valueText.Contains(_formatInfo.CurrencyDecimalSeparator))
            {
                if (_formatInfo.CurrencyDecimalSeparator == ",")
                    valueText = tbValue.Text.Replace(".", ",");
                else
                    valueText = tbValue.Text.Replace(",", ".");
            }              
            SetRoundingFormat();
            if(double.TryParse(valueText,  out double result)==true)
                Value = result > MaxValue ? Value : result < MinValue ? Value : result;                
            
            if(tbValue.Text != Value.ToString(_roundingFormat))
                tbValue.Text = Value.ToString(_roundingFormat);
        }
        private void SetRoundingFormat()
        {
            _roundingFormat = "0";
            if (RoundDigits>0)
            {
                _roundingFormat += ".";
                for (int i=0; i< RoundDigits; i++)
                {
                    _roundingFormat += "0";
                }
            }
        }
    }
}
