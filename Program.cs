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
            var bmp = new BitmapCreator(800, 600);
            bmp.Draw(pixels =>
            {
                // generate image here
                for (int y = 0; y < bmp.PixelHeight; y++)
                {
                    byte accumulator = 0;
                    for (int x = 0; x < bmp.PixelWidth; x++)
                    {
                        if (accumulator == 255)
                        {
                            accumulator = 0;
                        }

                        accumulator++;
                        byte val = accumulator;
                        

                        pixels[y, x] = Pixel.Create((byte) ((x * y)/255),val,(byte) (val - accumulator));
                    }
                }
            });
            
            File.WriteAllBytes("test.bmp", bmp.ToBytes());
        }
    }
}