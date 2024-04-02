using Converter.Support;
using IUR_P07_solved.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Converter.ViewModels
{
    class ImageDataViewModel : ViewModelBase
    {
        private string _name = "";
        private string _type = "";
        private string _path = "";
        
        private int _height = 0;
        private int _width = 0;

        private long _memsize = 0;
        private bool _isChecked = false;


        private BitmapImage _image = new BitmapImage();


        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Type
        {
            get { return _type; }
            set 
            { 
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public string Path
        { 
            get { return _path; } 
            set 
            {  
                _path = value;
                OnPropertyChanged(nameof(Path));
            } 
        }

        public int Height
        { 
            get { return _height; } 
            set 
            { 
                _height = value;
                OnPropertyChanged(nameof(Height));
            } 
        }

        public int Width 
        { 
            get { return _width; } 
            set 
            { 
                _width = value;
                OnPropertyChanged(nameof(Width));
            } 
        }

        public long MemSize
        {
            get => _memsize;
            set
            {
                _memsize = value;
                OnPropertyChanged(nameof(MemSize));
            }
        }

        public BitmapImage Image 
        { 
            get { return _image; }
            set 
            { 
                _image = value;
                OnPropertyChanged(nameof(Image));
            } 
        }

        public bool IsChecked 
        { 
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        public ImageDataViewModel(string name, string type, string path, BitmapImage source)
        {
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
            Type = System.IO.Path.GetExtension(path).TrimStart('.');
            Path = path;
            if (source != null)
            {
                Image = source;
                MemSize = ImageOperations.EstimateMemoryUsage(source);
                Height = Image.PixelHeight;
                Width = Image.PixelWidth;
            }
        }

    }
}
