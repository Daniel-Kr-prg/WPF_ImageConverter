using Converter.ViewModels;
using Converter.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class GIF : UserControl
    {

        public RelayCommand AddGifCommand
        {
            get { return (RelayCommand)GetValue(AddGifCommandProperty); }
            set { SetValue(AddGifCommandProperty, value); }
        }

        public static readonly DependencyProperty AddGifCommandProperty =
            DependencyProperty.Register("AddGifCommand", typeof(RelayCommand), typeof(GIF));

        public RelayCommand MakeGifCommand
        {
            get { return (RelayCommand)GetValue(MakeGifCommandProperty); }
            set { SetValue(MakeGifCommandProperty, value); }
        }

        public static readonly DependencyProperty MakeGifCommandProperty =
            DependencyProperty.Register("MakeGifCommand", typeof(RelayCommand), typeof(GIF));

        public RelayCommand SelectAllCommand
        {
            get { return (RelayCommand)GetValue(SelectAllCommandProperty); }
            set { SetValue(SelectAllCommandProperty, value); }
        }

        public static readonly DependencyProperty SelectAllCommandProperty =
            DependencyProperty.Register("SelectAllCommand", typeof(RelayCommand), typeof(GIF));

        public RelayCommand DeselectAllCommand
        {
            get { return (RelayCommand)GetValue(DeselectAllCommandProperty); }
            set { SetValue(DeselectAllCommandProperty, value); }
        }

        public static readonly DependencyProperty DeselectAllCommandProperty =
            DependencyProperty.Register("DeselectAllCommand", typeof(RelayCommand), typeof(GIF));



        public int FrameCount
        {
            get { return (int)GetValue(FramesCountProperty); }
            set { SetValue(FramesCountProperty, value); }
        }

        public static readonly DependencyProperty FramesCountProperty =
            DependencyProperty.Register("FrameCount", typeof(int), typeof(GIF));


        public GIF()
        {
            InitializeComponent();
        }
    }
}
