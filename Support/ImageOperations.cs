using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.IO;
using System.Windows.Media.Media3D;
using System.Xml.Linq;
using WpfAnimatedGif;
using System.Collections.ObjectModel;
using Converter.ViewModels;

namespace Converter.Support
{
    class ImageOperations
    {
        public static BitmapImage ResizeImage(BitmapImage source, int newWidth, int newHeight)
        {
            ScaleTransform scaleTransform = new ScaleTransform((double)newWidth / source.PixelWidth, (double)newHeight / source.PixelHeight);
            TransformedBitmap resizedBitmap = new TransformedBitmap(source, scaleTransform);
            return ConvertBitmapFrameToMemoryStream(resizedBitmap);
        }

        public static BitmapImage RotateImage(BitmapImage source, double angle)
        {
            //TransformedBitmap rotatedBitmap = new TransformedBitmap(source, new RotateTransform(angle));
            //return ConvertBitmapFrameToMemoryStream(rotatedBitmap);

            var biOriginal = source;

            var biRotated = new BitmapImage();
            biRotated.BeginInit();
            biRotated.UriSource = biOriginal.UriSource;
            switch (angle)
            {
                case 0:
                    biRotated.Rotation = Rotation.Rotate0;
                    break;
                case 90:
                    biRotated.Rotation = Rotation.Rotate90;
                    break;
                case 180:
                    biRotated.Rotation = Rotation.Rotate180;
                    break;
                case 270:
                    biRotated.Rotation = Rotation.Rotate270;
                    break;
            }
            biRotated.EndInit();

            return biRotated;
        }

        public static BitmapImage CropImage(BitmapImage source, Int32Rect region)
        {
            if (region.IsEmpty || region.Width < 1 || region.Height < 1 || region.Y >= source.PixelHeight || region.X >= source.PixelWidth)
            {
                return source;
            }

            region.X = Math.Max(0, region.X);
            region.Y = Math.Max(0, region.Y);
            region.Width = Math.Min(source.PixelWidth - region.X, region.Width);
            region.Height = Math.Min(source.PixelHeight - region.Y, region.Height);

            CroppedBitmap croppedBitmap = new CroppedBitmap(source, region);
            return ConvertBitmapFrameToMemoryStream(croppedBitmap);
        }

        public static BitmapImage FlipImageHorizontally(BitmapImage source)
        {
            TransformedBitmap transformedBitmap = new TransformedBitmap(source, new ScaleTransform(-1, 1));
            return ConvertBitmapFrameToMemoryStream(transformedBitmap);
        }

        public static BitmapImage FlipImageVertically(BitmapImage source)
        {
            TransformedBitmap transformedBitmap = new TransformedBitmap(source, new ScaleTransform(1, -1));
            return ConvertBitmapFrameToMemoryStream(transformedBitmap);
        }

        private static MemoryStream ConvertBitmapSourceToStream(BitmapSource bitmapSource)
        {
            MemoryStream stream = new MemoryStream();
            BitmapEncoder encoder = new PngBitmapEncoder(); // Выберите нужный формат
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.Save(stream);
            return stream;
        }

        public static void CreateGif(List<BitmapImage> frames, string outputPath)
        {
            GifBitmapEncoder gifEncoder = new GifBitmapEncoder();

            foreach (BitmapSource frame in frames)
            {
                gifEncoder.Frames.Add(BitmapFrame.Create(frame));
            }

            using (FileStream fs = new FileStream(outputPath, FileMode.Create))
            {
                gifEncoder.Save(fs);
            }
        }

        public static void LoadImage(string path, ObservableCollection<ImageDataViewModel> targetList)
        {
            if (targetList == null || path == null || path == "")
                return;

            FileInfo fileInfo = new FileInfo(path);

            if (!fileInfo.Exists) { throw new NullReferenceException("File doesn't exist"); }

            BitmapImage img = new BitmapImage(new Uri(path));
            if (img != null)
                targetList.Add(new ImageDataViewModel(fileInfo.Name, fileInfo.Extension, path, img));
        }

        public static void LoadGIF(string path, ObservableCollection<ImageDataViewModel> targetList)
        {
            if (targetList == null || path == null || path == "")
                return;

            FileInfo fileInfo = new FileInfo(path);

            if (!fileInfo.Exists) { throw new NullReferenceException("File doesn't exist"); }

            int frameCounter = 0;
            foreach (BitmapImage img in LoadGifFrames(path))
            {
                targetList.Add(new ImageDataViewModel(fileInfo.Name + frameCounter++.ToString(), fileInfo.Extension, path, img));
            }
        }

        static IEnumerable<BitmapImage> LoadGifFrames(string gifPath)
        {
            Stream imageStreamSource = new FileStream(gifPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            GifBitmapDecoder decoder = new GifBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);

            foreach (BitmapSource frame in decoder.Frames)
            {
                yield return ConvertBitmapFrameToMemoryStream(frame);
            }
        }

        static BitmapImage ConvertBitmapFrameToMemoryStream(BitmapSource source)
        {
            BitmapImage bitmapImage = new BitmapImage();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(source));
                encoder.Save(memoryStream);

                // Загрузка закодированных данных в BitmapImage
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
                bitmapImage.Freeze(); // Замораживаем изображение для улучшения производительности
            }

            return bitmapImage;
        }

        public static long EstimateMemoryUsage(BitmapImage bitmapImage)
        {
            try
            {
                int width = bitmapImage.PixelWidth;
                int height = bitmapImage.PixelHeight;
                int bitsPerPixel = bitmapImage.Format.BitsPerPixel;

                long estimatedMemoryUsage = (long)width * height * bitsPerPixel / 8;
                return estimatedMemoryUsage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load image: {ex.Message}");
                return -1;
            }
        }
    }
}
