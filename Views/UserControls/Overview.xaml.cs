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
    /// Логика взаимодействия для Overview.xaml
    /// </summary>
    public partial class Overview : UserControl
    {
        // FileName
        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName", typeof(string), typeof(Overview));

        // FileSize
        public long FileSize
        {
            get { return (long)GetValue(FileSizeProperty); }
            set { SetValue(FileSizeProperty, value); }
        }

        public static readonly DependencyProperty FileSizeProperty =
            DependencyProperty.Register("FileSize", typeof(long), typeof(Overview));

        // FileType
        public string FileType
        {
            get { return (string)GetValue(FileTypeProperty); }
            set { SetValue(FileTypeProperty, value); }
        }

        public static readonly DependencyProperty FileTypeProperty =
            DependencyProperty.Register("FileType", typeof(string), typeof(Overview));

        // ImageSize
        public int ImageHeight
        {
            get { return (int)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(int), typeof(Overview));

        public int ImageWidth
        {
            get { return (int)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(int), typeof(Overview));

        // FilePath
        public string FilePath
        {
            get { return (string)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }

        public static readonly DependencyProperty FilePathProperty =
            DependencyProperty.Register("FilePath", typeof(string), typeof(Overview));

        // Zoom
        //public double Zoom
        //{
        //    get { return (double)GetValue(ZoomProperty); }
        //    set { SetValue(ZoomProperty, value); }
        //}

        //public static readonly DependencyProperty ZoomProperty =
        //    DependencyProperty.Register("Zoom", typeof(double), typeof(Overview));

        // HorizontalTranslate
        public double HorizontalTranslate
        {
            get { return (double)GetValue(HorizontalTranslateProperty); }
            set { SetValue(HorizontalTranslateProperty, value); }
        }

        public static readonly DependencyProperty HorizontalTranslateProperty =
            DependencyProperty.Register("HorizontalTranslate", typeof(double), typeof(Overview));

        // VerticalTranslate
        public double VerticalTranslate
        {
            get { return (double)GetValue(VerticalTranslateProperty); }
            set { SetValue(VerticalTranslateProperty, value); }
        }

        public static readonly DependencyProperty VerticalTranslateProperty =
            DependencyProperty.Register("VerticalTranslate", typeof(double), typeof(Overview));

        public RelayCommand RestoreImageCommand
        {
            get { return (RelayCommand)GetValue(RestoreImageCommandProperty); }
            set { SetValue(RestoreImageCommandProperty, value); }
        }

        public static readonly DependencyProperty RestoreImageCommandProperty =
            DependencyProperty.Register("RestoreImageCommand", typeof(RelayCommand), typeof(Overview));


        public Overview()
        {
            InitializeComponent();
        }
    }
}
