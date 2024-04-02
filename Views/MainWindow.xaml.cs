using Converter.ViewModels;
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

namespace Converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();

            viewModel = DataContext as MainViewModel;

            if (viewModel != null )
            {
                ImageBox.MouseDown += viewModel.ImageMouseDown;
                ImageBox.MouseUp += viewModel.ImageMouseUp;
                ImageBox.MouseMove += viewModel.ImageMouseMove;
                ImageBox.MouseLeave += viewModel.ImageMouseLeave;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ImageDataViewModel? image = (e.Source as CheckBox)?.DataContext as ImageDataViewModel;
            
            if (image == null)
                return;

            viewModel.CheckImage(image, true);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ImageDataViewModel? image = (e.Source as CheckBox)?.DataContext as ImageDataViewModel;

            if (image == null)
                return;

            viewModel.CheckImage(image, false);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is TabItem tab)
            {
                viewModel.TabChange((string)tab.Header);
            }
        }
    }
}
