﻿using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace BitMapper
{
    class Program
    {
        public static uint SizeOf<T>()
        {
            return Convert.ToUInt32(System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)));
        }

        public static uint GetSize(object obj)
        {
            return Convert.ToUInt32(System.Runtime.InteropServices.Marshal.SizeOf(obj));
        }

        public static byte[] GetBytes<T>(T str) where T : struct
        {
            int size = Marshal.SizeOf(str);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(str, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }

        static void Main(string[] args)
        {
            BitmapHeader header = new BitmapHeader();
            header.reserved1 = 0;
            header.reserved2 = 0;

            var infoHeader = new InfoHeader();

            header.type = Encoding.ASCII.GetBytes("BM");
            header.offset = GetSize(header) + GetSize(infoHeader);

            infoHeader.size = 40;
            infoHeader.bits = 24;
            infoHeader.compression = 0;
            infoHeader.height = 600;
            infoHeader.planes = 1;
            infoHeader.width = 800;
            infoHeader.imagesize = SizeOf<Pixel>() * (uint) infoHeader.height * (uint) infoHeader.width;
            infoHeader.xresolution = 2835;
            infoHeader.yresolution = 2835;
            infoHeader.ncolours = 0;

            var imageData = new Pixel[infoHeader.height, infoHeader.width];

            // generate image here
            for (int y = 0; y < infoHeader.height; y++)
            {
                for (int x = 0; x < infoHeader.width; x++)
                {
                    var outValue = Math.Abs(Math.Sin(x) * 100);
                    //if (outValue > 255)
                    //{
                    //    outValue = 255;
                    //}
                    byte val = 255;
                    if (x % 2 == 0)
                    {
                        val = 0;
                    }

                    imageData[y, x] = new Pixel {B = val, R = 255, G = val};
                }
            }

            header.size = GetSize(header) + SizeOf<InfoHeader>() + (SizeOf<Pixel>() * (uint) infoHeader.width * (uint) infoHeader.height);
            var bytes = GetBytes(header).ToList();
            bytes.AddRange(GetBytes(infoHeader));
            
            for (var j = 0; j < infoHeader.height; j++)
            for (var i = 0; i < infoHeader.width; i++)
            {
                bytes.AddRange(GetBytes(imageData[j, i]));
            }

            File.WriteAllBytes("test.bmp", bytes.ToArray());
        }
    }
}