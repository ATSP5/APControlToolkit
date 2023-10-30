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
    public partial class APUpDownInteger : UserControl
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

        private int _inValue;
        public int InValue 
        {
            get { return (int)GetValue(ControlValueProperty); }
            set { SetValue(ControlValueProperty, value); }
        }

        public int MaxValue { get; set; } = int.MaxValue;
        public int MinValue { get; set; } = int.MinValue;
        public int Step { get; set; }
        public APUpDownInteger()
        {
            InitializeComponent();
            InValue = 0;
            Step = 1;
            
        }
        // Dependency Property for Value
        public static readonly DependencyProperty ControlValueProperty =
           DependencyProperty.Register(nameof(InValue), typeof(int), typeof(APUpDownInteger),
           new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ControlValueChangedCallback));

        private static void ControlValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var customControl = (APUpDownInteger)d;

            TextBox textBox = customControl.tbValue;

            textBox.Text = customControl.InValue.ToString();
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            InValue=InValue+Step>MaxValue? InValue : InValue+Step;
            tbValue.Text = InValue.ToString();
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            InValue=InValue+Step<MinValue? InValue: InValue-Step;
            tbValue.Text = InValue.ToString();
        }

        private bool isUpdatingTextBox = false;
        private void tbValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isUpdatingTextBox)
                return;

            var valueText = tbValue.Text;

            if (int.TryParse(valueText, out int result))
            {

                if (result < MinValue)
                    result = MinValue;
                else if (result > MaxValue)
                    result = MaxValue;

                isUpdatingTextBox = true;

                InValue = result;

                isUpdatingTextBox = false;
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                tbValue.Text = InValue.ToString();
                isUpdatingTextBox = false;
                tbValue.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                e.Handled = true;
            }
        }
    }
}