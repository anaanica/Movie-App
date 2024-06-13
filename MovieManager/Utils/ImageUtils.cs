using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MovieManager.Utils
{
    public static class ImageUtils
    {
        public static BitmapImage ByteArrayToBitmapImage(byte[] picture)
        {
            using var memoryStream = new MemoryStream(picture);
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = memoryStream;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze(); // unmodifiable
            return bitmap;
        }

        public static byte[] BitmapImageToByteArray(BitmapImage image)
        {
            var jpegEncoder = new JpegBitmapEncoder();
            jpegEncoder.Frames.Add(BitmapFrame.Create(image));
            using var memoryStream = new MemoryStream();
            jpegEncoder.Save(memoryStream);
            return memoryStream.ToArray();
        }

        public static byte[] ByteArrayFromSqlDataReader(SqlDataReader dr, string columnName)
        {
            int bufferSize = 1024;
            byte[] buffer = new byte[bufferSize];

            int currentBytes = 0;

            using var memoryStream = new MemoryStream();
            using var binaryWriter = new BinaryWriter(memoryStream); //decorator pattern
            int readBytes;
            do
            {
                readBytes = (int)dr.GetBytes(dr.GetOrdinal(columnName), currentBytes, buffer, 0, bufferSize);
                binaryWriter.Write(buffer, 0, readBytes);
                currentBytes += readBytes;
            } while (readBytes == bufferSize);
            return memoryStream.ToArray();
        }
    }
}
