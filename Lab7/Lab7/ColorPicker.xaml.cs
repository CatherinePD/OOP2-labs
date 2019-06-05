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

namespace Lab7
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public ColorPicker()
        {
            InitializeComponent();
        }
        public static DependencyProperty ColorProperty;
        public static DependencyProperty RedProperty;
        public static DependencyProperty GreenProperty;
        public static DependencyProperty BlueProperty;
        public static readonly RoutedEvent ColorChangedEvent;

        static ColorPicker()
        {
            FrameworkPropertyMetadata metadata1 = new FrameworkPropertyMetadata();
            metadata1.CoerceValueCallback = new CoerceValueCallback(CorrectValue);
            metadata1.PropertyChangedCallback = new PropertyChangedCallback(OnColorRGBChanged);

            FrameworkPropertyMetadata metadata2 = new FrameworkPropertyMetadata();
            metadata2.CoerceValueCallback = new CoerceValueCallback(CorrectValue);
            metadata2.PropertyChangedCallback = new PropertyChangedCallback(OnColorRGBChanged);

            FrameworkPropertyMetadata metadata3 = new FrameworkPropertyMetadata();
            metadata3.CoerceValueCallback = new CoerceValueCallback(CorrectValue);
            metadata3.PropertyChangedCallback = new PropertyChangedCallback(OnColorRGBChanged);

            // Регистрация свойств зависимости
            ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(ColorPicker),
                new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorChanged)));
            RedProperty = DependencyProperty.Register("Red", typeof(byte), typeof(ColorPicker), metadata1, new ValidateValueCallback(ValidateValue));
            GreenProperty = DependencyProperty.Register("Green", typeof(byte), typeof(ColorPicker), metadata2, new ValidateValueCallback(ValidateValue));
            BlueProperty = DependencyProperty.Register("Blue", typeof(byte), typeof(ColorPicker), metadata3, new ValidateValueCallback(ValidateValue));

            ColorPicker.ColorChangedEvent = EventManager.RegisterRoutedEvent("ColorChanged", RoutingStrategy.Tunnel, //регистрация события
                typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorPicker));
        }

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }
        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }
        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }

        private static void OnColorRGBChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            ColorPicker colorPicker = (ColorPicker)sender;
            Color color = colorPicker.Color;
            if (e.Property == RedProperty)
                color.R = (byte)e.NewValue;
            else if (e.Property == GreenProperty)
                color.G = (byte)e.NewValue;
            else if (e.Property == BlueProperty)
                color.B = (byte)e.NewValue;

            colorPicker.Color = color;
            
        }

        private static void OnColorChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            Color newColor = (Color)e.NewValue;
            ColorPicker colorpicker = (ColorPicker)sender;
            colorpicker.Red = newColor.R;
            colorpicker.Green = newColor.G;
            colorpicker.Blue = newColor.B;

            colorpicker.txb.Text = "The new color is "+ newColor.ToString();

            colorpicker.RaiseEvent(new RoutedPropertyChangedEventArgs<Color>((Color)e.OldValue, newColor, ColorPicker.ColorChangedEvent));
            //инициализация события
        }

        public event RoutedPropertyChangedEventHandler<Color> ColorChanged //обертка над событием
        {
            add { base.AddHandler(ColorPicker.ColorChangedEvent, value); }
            remove { base.RemoveHandler(ColorPicker.ColorChangedEvent, value); }
        }

        private static object CorrectValue(DependencyObject d, object baseValue)
        {
            byte currentValue = (byte)baseValue;
            if (currentValue > 255)  // если больше 1000, возвращаем 1000
                return 255;
            return currentValue; // иначе возвращаем текущее значение
        }

        private static bool ValidateValue(object value)
        {
            byte currentValue = (byte)value;
            if (currentValue >= 0) // если текущее значение от нуля и выше
                return true;
            return false;
        }

    }

}
