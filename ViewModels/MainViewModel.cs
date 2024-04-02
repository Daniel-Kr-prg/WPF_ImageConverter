using Converter.Support;
using Converter.Views;
using IUR_P07_solved.Support;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WeatherInfoCustomControl.Support;

namespace Converter.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public ObservableCollection<ImageDataViewModel> Images { get; set; } = new ObservableCollection<ImageDataViewModel>();
        public ObservableCollection<ImageDataViewModel> SelectedImages { get; set; } = new ObservableCollection<ImageDataViewModel>();

        private ImageDataViewModel _selectedImage;

        public ImageDataViewModel SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                _selectedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
                RestoreImage(null);
            }
        }

        private double _hor = 0;
        private double _ver = 0;

        private double _zoom = 1;

        private double _horZoom = 1;
        private double _verZoom = 1;

        private int _horScale = 0;
        private int _verScale = 0;

        private double _horCrop = 0;
        private double _verCrop = 0;

        private int _rotation = 0;

        private bool _gifTabOpened;


        private bool _cropAllowed = false;

        public double Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                OnPropertyChanged(nameof(Zoom));
            }
        }

        public double HorZoom
        {
            get { return _horZoom; }
            set
            {
                _horZoom = Zoom * value;
                OnPropertyChanged(nameof(HorZoom));
            }
        }

        public double VerZoom
        {
            get { return _verZoom; }
            set
            {
                _verZoom = Zoom * value;
                OnPropertyChanged(nameof(VerZoom));
            }
        }

        public int HorScale
        {
            get { return _horScale; }
            set
            {
                _horScale = value;
                OnPropertyChanged(nameof(HorScale));
            }
        }

        public int VerScale
        {
            get { return _verScale; }
            set
            {
                _verScale = value;
                OnPropertyChanged(nameof(VerScale));
            }
        }

        public double HorizontalTranslate
        {
            get { return _hor; }
            set
            {
                _hor = value;
                OnPropertyChanged(nameof(HorizontalTranslate));
            }
        }

        public double VerticalTranslate
        {
            get { return _ver; }
            set
            {
                _ver = value;
                OnPropertyChanged(nameof(VerticalTranslate));
            }
        }

        public double HorizontalCrop
        {
            get { return _horCrop; }
            set
            {
                _horCrop = value;
                OnPropertyChanged(nameof(HorizontalCrop));
            }
        }

        public double VerticalCrop
        {
            get => _verCrop;
            set
            {
                _verCrop = value;
                OnPropertyChanged(nameof(VerticalCrop));
            }
        }

        public bool GifTabOpened
        {
            get => _gifTabOpened;
            set
            {
                _gifTabOpened = value;
                OnPropertyChanged(nameof(GifTabOpened));
            }
        }

        public bool CropAllowed
        {
            get { return _cropAllowed; }
            set
            {
                _cropAllowed = value;
                OnPropertyChanged(nameof(CropAllowed));
            }
        }


        #region RELAY COMMANDS

        private RelayCommand _loadImage;
        private RelayCommand _saveImage;
        private RelayCommand _addImage;
        private RelayCommand _deleteImage;
        private RelayCommand _restoreImage;
        private RelayCommand _makeGIF;
        private RelayCommand _loadGIF;

        private RelayCommand _selectAll;
        private RelayCommand _deselectAll;

        private RelayCommand _scale;
        private RelayCommand _rotateLeft;
        private RelayCommand _rotateRight;
        private RelayCommand _crop;
        private RelayCommand _mirrorX;
        private RelayCommand _mirrorY;


        public RelayCommand LoadImageCommand
        {
            get { return _loadImage ?? (_loadImage = new RelayCommand(LoadImage, CanExecutePlaceholder)); }
        }
        private void LoadImage(object obj)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open Picture";
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.tiff; *.ico; *.bmp)|*.jpg; *.jpeg; *.png; *.tiff; *.ico; *.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                ImageOperations.LoadImage(selectedFilePath, Images);

                if (Images.Count > 0)
                    SelectedImage = Images.Last();
            }
        }

        public RelayCommand SaveImageCommand
        {
            get { return _saveImage ?? (_saveImage = new RelayCommand(SaveImage, CanExecuteSelectedNotNull)); }
        }
        private void SaveImage(object obj)
        {
            SaveWindow saveWindow = new SaveWindow(SelectedImage.Image);
            saveWindow.Owner = Application.Current.MainWindow;
            bool? result = saveWindow.ShowDialog();
            if (result.HasValue && result.Value)
            {

            }
        }

        private bool CanExecutePlaceholder(object obj)
        {
            return true;
        }

        private bool CanExecuteSelectedNotNull(object obj)
        {
            return SelectedImage != null;
        }

        public RelayCommand AddImageCommand
        {
            get { return _addImage ?? (_addImage = new RelayCommand(AddImage, CanExecuteSelectedNotNull)); }
        }
        private void AddImage(object obj)
        {
            ImageDataViewModel copy = new ImageDataViewModel(SelectedImage.Name, SelectedImage.Type, SelectedImage.Path, SelectedImage.Image.Clone());
            Images.Add(copy);

            SelectedImage = Images.Last();
        }

        public RelayCommand DeleteImageCommand
        {
            get { return _deleteImage ?? (_deleteImage = new RelayCommand(DeleteImage, CanExecuteSelectedNotNull)); }
        }

        private void DeleteImage(object obj)
        {
            if (Images.Contains(SelectedImage)) 
                Images.Remove(SelectedImage);
            if (SelectedImages.Contains(SelectedImage))
                SelectedImages.Remove(SelectedImage);

            if (Images.Count > 0)
                SelectedImage = Images.Last();
        }

        public RelayCommand RestoreImageCommand
        {
            get { return _restoreImage ?? (_restoreImage = new RelayCommand(RestoreImage, CanExecutePlaceholder)); }
        }

        private void RestoreImage(object obj)
        {
            Zoom = 1;
            HorizontalTranslate = 0;
            VerticalTranslate = 0;
        }

        private ImageDataViewModel ReuploadImage(ImageDataViewModel imageData)
        {
            return new ImageDataViewModel(imageData.Name, imageData.Type, imageData.Path, imageData.Image);
        }

        public RelayCommand LoadGIFCommand
        {
            get { return _loadGIF ?? (_loadGIF = new RelayCommand(LoadGIF, CanExecutePlaceholder)); }
        }

        private void LoadGIF(object obj)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open GIF";
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "GIF Files(*.gif)|*.gif";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                ImageOperations.LoadGIF(selectedFilePath, Images);

                if (SelectedImage == null && Images.Count > 0)
                {
                    SelectedImage = Images[0];
                }
            }
        }

        public RelayCommand MakeGIFCommand
        {
            get { return _makeGIF ?? (_makeGIF = new RelayCommand(MakeGIF, CanExecutePlaceholder)); }
        }

        private void MakeGIF(object obj)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "GIF files (*.gif)|*.gif";

            if (saveFileDialog.ShowDialog() == true)
            {
                string outputPath = saveFileDialog.FileName;

                ImageOperations.CreateGif(SelectedImages.Select(image => image.Image).ToList(), outputPath);
            }
        }

        public RelayCommand SelectAllCommand
        {
            get { return _selectAll ?? (_selectAll = new RelayCommand(SelectAll, CanExecutePlaceholder)); }
        }

        private void SelectAll(object obj)
        {
            foreach (var item in Images)
            {
                item.IsChecked = true;
            }
        }

        public RelayCommand DeselectAllCommand
        {
            get { return _deselectAll ?? (_deselectAll = new RelayCommand(DeselectAll, CanExecutePlaceholder)); }
        }

        private void DeselectAll(object obj)
        {
            foreach (var item in Images)
            {
                item.IsChecked = false;
            }
        }

        public RelayCommand ScaleCommand
        {
            get { return _scale ?? (_scale = new RelayCommand(ScaleImage, CanExecutePlaceholder)); }
        }
        private void ScaleImage(object obj)
        {
            SelectedImage.Image = ImageOperations.ResizeImage(SelectedImage.Image, HorScale, VerScale);
            SelectedImage = ReuploadImage(SelectedImage);
        }


        public RelayCommand RotateLeftCommand
        {
            get { return _rotateLeft ?? (_rotateLeft = new RelayCommand(RotateImageLeft, CanExecutePlaceholder)); }
        }

        public RelayCommand RotateRightCommand
        {
            get { return _rotateRight ?? (_rotateRight = new RelayCommand(RotateImageRight, CanExecutePlaceholder)); }
        }

        private void RotateImageLeft(object obj)
        {
            _rotation = ((_rotation - 90) % 360 + 360) % 360;
            SelectedImage.Image = ImageOperations.RotateImage(SelectedImage.Image, _rotation);
            SelectedImage = ReuploadImage(SelectedImage);
        }

        private void RotateImageRight(object obj)
        {
            _rotation = ((_rotation + 90) % 360 + 360) % 360;
            SelectedImage.Image = ImageOperations.RotateImage(SelectedImage.Image, _rotation);
            SelectedImage = ReuploadImage(SelectedImage);
        }

        public RelayCommand CropCommand
        {
            get { return _crop ?? (_crop = new RelayCommand(CropImage, CanExecutePlaceholder)); }
        }

        private void CropImage(object obj)
        {
            CropAllowed = !CropAllowed;
            RestoreImage(null);
            StartPoint = new Vector(90, 90);
            HorizontalCrop = 50;
            VerticalCrop = 50;
        }

        public RelayCommand MirrorXCommand
        {
            get { return _mirrorX ?? (_mirrorX = new RelayCommand(MirrorXImage, CanExecutePlaceholder)); }
        }

        private void MirrorXImage(object obj)
        {
            SelectedImage.Image = ImageOperations.FlipImageHorizontally(SelectedImage.Image);
        }

        public RelayCommand MirrorYCommand
        {
            get { return _mirrorY ?? (_mirrorY = new RelayCommand(MirrorYImage, CanExecutePlaceholder)); }
        }

        private void MirrorYImage(object obj)
        {
            SelectedImage.Image = ImageOperations.FlipImageVertically(SelectedImage.Image);
        }

        public void CheckImage(ImageDataViewModel selectedImageData, bool check)
        {
            if (check && !SelectedImages.Contains(selectedImageData))
            {
                SelectedImages.Add(selectedImageData);
            }
            else if (!check && SelectedImages.Contains(selectedImageData))
            {
                SelectedImages.Remove(selectedImageData);
            }
        }

        public void TabChange(string TabName)
        {
            RestoreImage(null);
            if (TabName == "GIF")
            {
                GifTabOpened = true;
            }
            else
            {
                GifTabOpened = false;
            }
        }

        #endregion

        private Vector _translate;

        public Vector Translate
        {
            get { return _translate; }
            set
            {
                _translate = value;
                OnPropertyChanged(nameof(_translate));
            }
        }




        private Vector _currentPoint;
        private Vector _startPoint;

        public Vector StartPoint
        {
            get { return _startPoint; }
            set { 
                _startPoint = value; 
                OnPropertyChanged(nameof(StartPoint));
            }
        }


        private bool dragAllowed;
        public void ImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            dragAllowed = true;

            if (CropAllowed)
            {
                StartPoint = (Vector)e.GetPosition((IInputElement)sender);
            }
            else
            {
                _currentPoint = (Vector)e.GetPosition((IInputElement)sender);
            }
        }

        public void ImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            dragAllowed = false;
            if (CropAllowed)
                EndCrop();
        }
        public void ImageMouseLeave(object sender, MouseEventArgs e)
        {
            dragAllowed = false;
        }

        private void EndCrop()
        {
            BitmapImage img = SelectedImage.Image;

            double scale = (double)320 / Math.Max(img.PixelWidth, img.PixelHeight);

            Vector offset = new Vector(320 - img.PixelWidth * scale, 320 - img.PixelHeight * scale) / 2;


            SelectedImage.Image = ImageOperations.CropImage(img, 
                new Int32Rect(
                    (int)((StartPoint.X - offset.X) / scale), 
                    (int)((StartPoint.Y - offset.Y) / scale), 
                    (int)(HorizontalCrop / scale), 
                    (int)(VerticalCrop / scale)));
            CropAllowed = false;

            SelectedImage = ReuploadImage(SelectedImage);

        }

        public void ImageMouseMove(object sender, MouseEventArgs e)
        {
            if (!dragAllowed)
                return;

            if (CropAllowed)
            {
                _currentPoint = (Vector)e.GetPosition((IInputElement)sender);
                HorizontalCrop = _currentPoint.X - _startPoint.X;
                VerticalCrop = _currentPoint.Y - _startPoint.Y;
            }
            else
            {
                Vector move = ((Vector)e.GetPosition((IInputElement)sender) - _currentPoint);
                HorizontalTranslate += move.X;
                VerticalTranslate += move.Y;

                _currentPoint = (Vector)e.GetPosition((IInputElement)sender);
            }

        }
    }
}
