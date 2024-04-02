using Converter.Views;
using IUR_P07_solved.Support;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WeatherInfoCustomControl.Support;

namespace Converter.ViewModels
{
    internal class SaveWindowViewModel : ViewModelBase
    {
        private BitmapImage _imageToSave;

        private SaveWindow saveWindow;

        public BitmapImage ImageToSave
        {
            get { return _imageToSave; }
            set 
            { 
                _imageToSave = value;
                OnPropertyChanged(nameof(ImageToSave));
            }
        }


        private RelayCommand _saveImage;

        public RelayCommand SaveImageCommand
        {
            get { return _saveImage ?? (_saveImage = new RelayCommand(SaveImage, CanExecutePlaceholder)); }
        }

        private bool CanExecutePlaceholder(object obj)
        {
            return true;
        }

        public void SetSaveWindow(SaveWindow saveWindow)
        {
            this.saveWindow = saveWindow;
        }

        private void SaveImage(object format)
        {
            string filePath = ShowSaveFileDialog((string)format);
            if (!string.IsNullOrEmpty(filePath))
            {
                SaveImageToFile(filePath, (string)format);
            }
        }

        private void SaveImageToFile(string filePath, string format)
        {
            BitmapEncoder encoder = null;

            switch (format.ToLower())
            {
                case "png":
                    encoder = new PngBitmapEncoder();
                    break;
                case "jpg":
                    encoder = new JpegBitmapEncoder();
                    break;
                case "tiff":
                    encoder = new TiffBitmapEncoder();
                    break;
                case "bmp":
                    encoder = new BmpBitmapEncoder();
                    break;
                default:
                    break;
            }

            if (encoder != null)
            {
                encoder.Frames.Add(BitmapFrame.Create(ImageToSave));

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    encoder.Save(fs);
                }

                saveWindow.DialogResult = true;
                saveWindow.Close();
            }
        }

        private string ShowSaveFileDialog(string format)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = $"{format} files|*.{format}|All files|*.*",
                DefaultExt = format,
                AddExtension = true
            };

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                return saveFileDialog.FileName;
            }

            return null;
        }
    }
}
