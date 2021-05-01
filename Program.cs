using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace BitMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var bmp = new BitmapImage(800, 600);
            // bmp.Draw(pixels =>
            // {
            //     // generate image here
            //     for (int y = 0; y < bmp.PixelHeight; y++)
            //     {
            //         byte accumulator = 0;
            //         for (int x = 0; x < bmp.PixelWidth; x++)
            //         {
            //             if (accumulator == 255)
            //             {
            //                 accumulator = 0;
            //             }
            //
            //             accumulator++;
            //             byte val = accumulator;
            //
            //
            //             pixels[y, x] = Pixel.Create((byte) ((x * y) / 255), val, (byte) (val - accumulator));
            //         }
            //     }
            // });

            //bmp.Draw(DrawRect(50, 50, 200, 100, Pixel.Create(0, 200, 0), 50));
            bmp.Draw(DrawRect(400, 20, 20, 20, Pixel.Create(0, 0, 255), 0));

            File.WriteAllBytes("test.bmp", bmp.ToBytes());
        }

        private static Action<Pixel[,]> DrawRect(int x, int y, int width, int height, Pixel color, int opacity=100) =>
            pixels =>
            {
                var endY = y + height;
                var maxY = pixels.GetLength(0) -1;
                endY = endY >= maxY ? maxY : endY;
                for (int h = y-1; h <= endY; h++)
                {
                    for (int w = x - 1; w < x + width; w++)
                    {
                        pixels[h,w] = color.Blend(pixels[h,w], opacity);
                    }    
                }
                
            };
    }
}