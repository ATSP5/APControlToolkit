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
    public partial class APUpDownDouble : UserControl
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

        public double DValue
        {
            get { return (double)GetValue(ControlValueProperty); }
            set { SetValue(ControlValueProperty, value); }
        }

        public static readonly DependencyProperty ControlValueProperty =
            DependencyProperty.Register(nameof(DValue), typeof(double), typeof(APUpDownDouble),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ControlValueChangedCallback));

        private static void ControlValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var customControl = (APUpDownDouble)d;

            TextBox textBox = customControl.tbValue;

            textBox.Text = customControl.DValue.ToString(); 

        }

        public double MaxValue { get; set; } = double.MaxValue;
        public double MinValue { get; set; } = double.MinValue;
        public double Step { get; set; }

        private string _roundingFormat;

        public int RoundDigits { get; set; } = 1;

        // Get the current culture's NumberFormatInfo
        private NumberFormatInfo _formatInfo = CultureInfo.CurrentCulture.NumberFormat;

        public APUpDownDouble()
        {
            InitializeComponent();
            SetRoundingFormat();
            DValue = 0.0;
            Step = 1.0;
        }
       
        private void Up_Click(object sender, RoutedEventArgs e)
        {
            SetRoundingFormat();
            DValue = DValue + Step > MaxValue ? DValue : DValue + Step;
            tbValue.Text = DValue.ToString(_roundingFormat);
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            SetRoundingFormat();
            DValue = DValue + Step < MinValue ? DValue : DValue - Step;
            tbValue.Text = DValue.ToString(_roundingFormat);
        }

        private bool isUpdatingTextBox = false;

        private void tbValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isUpdatingTextBox)
                return;

            var valueText = tbValue.Text;

            valueText = valueText.Replace(",", _formatInfo.CurrencyDecimalSeparator).Replace(".", _formatInfo.CurrencyDecimalSeparator);

            if (double.TryParse(valueText, out double result))
            {

                if (result < MinValue)
                    result = MinValue;
                else if (result > MaxValue)
                    result = MaxValue;

                isUpdatingTextBox = true;

                DValue = result;

                isUpdatingTextBox = false;
            }
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

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                tbValue.Text = DValue.ToString(_roundingFormat);
                isUpdatingTextBox = false;
                tbValue.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                e.Handled = true;
            }  
        }
    }
}
