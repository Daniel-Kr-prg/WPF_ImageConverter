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
using WeatherInfoCustomControl.Support;

namespace Converter.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : UserControl
    {
        public static readonly DependencyProperty ScaleXProperty =
            DependencyProperty.Register("ScaleX", typeof(double), typeof(Edit));

        public static readonly DependencyProperty ScaleYProperty =
            DependencyProperty.Register("ScaleY", typeof(double), typeof(Edit));

        public double ScaleX
        {
            get { return (double)GetValue(ScaleXProperty); }
            set { SetValue(ScaleXProperty, value); }
        }

        public double ScaleY
        {
            get { return (double)GetValue(ScaleYProperty); }
            set { SetValue(ScaleYProperty, value); }
        }

        public static readonly DependencyProperty ScaleCommandProperty =
            DependencyProperty.Register("ScaleCommand", typeof(RelayCommand), typeof(Edit));

        public static readonly DependencyProperty RotateRightCommandProperty =
            DependencyProperty.Register("RotateRightCommand", typeof(RelayCommand), typeof(Edit));

        public static readonly DependencyProperty RotateLeftCommandProperty =
            DependencyProperty.Register("RotateLeftCommand", typeof(RelayCommand), typeof(Edit));

        public static readonly DependencyProperty MirrorXCommandProperty =
            DependencyProperty.Register("MirrorXCommand", typeof(RelayCommand), typeof(Edit));

        public static readonly DependencyProperty MirrorYCommandProperty =
            DependencyProperty.Register("MirrorYCommand", typeof(RelayCommand), typeof(Edit));

        public static readonly DependencyProperty CropCommandProperty =
            DependencyProperty.Register("CropCommand", typeof(RelayCommand), typeof(Edit));

        public RelayCommand ScaleCommand
        {
            get { return (RelayCommand)GetValue(ScaleCommandProperty); }
            set { SetValue(ScaleCommandProperty, value); }
        }

        public RelayCommand RotateRightCommand
        {
            get { return (RelayCommand)GetValue(RotateRightCommandProperty); }
            set { SetValue(RotateRightCommandProperty, value); }
        }

        public RelayCommand RotateLeftCommand
        {
            get { return (RelayCommand)GetValue(RotateLeftCommandProperty); }
            set { SetValue(RotateLeftCommandProperty, value); }
        }

        public RelayCommand MirrorXCommand
        {
            get { return (RelayCommand)GetValue(MirrorXCommandProperty); }
            set { SetValue(MirrorXCommandProperty, value); }
        }

        public RelayCommand MirrorYCommand
        {
            get { return (RelayCommand)GetValue(MirrorYCommandProperty); }
            set { SetValue(MirrorYCommandProperty, value); }
        }

        public RelayCommand CropCommand
        {
            get { return (RelayCommand)GetValue(CropCommandProperty); }
            set { SetValue(CropCommandProperty, value); }
        }


        public Edit()
        {
            InitializeComponent();
        }
    }
}
