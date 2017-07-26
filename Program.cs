using System;
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

            header.type = ASCIIEncoding.ASCII.GetBytes("BM");
            header.offset = GetSize(header) + GetSize(infoHeader);





            infoHeader.size = 40;
            infoHeader.bits = 24;
            infoHeader.compression = 0;
            infoHeader.height = 100;
            infoHeader.planes = 1;
            infoHeader.width = 100;
            infoHeader.imagesize = SizeOf<Pixel>() * (uint)infoHeader.height * (uint)infoHeader.width;
            infoHeader.xresolution = 2835;
            infoHeader.yresolution = 2835;
            infoHeader.ncolours = 0;

            var imageData = new Pixel[infoHeader.width, infoHeader.height];
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    imageData[x, y] = new Pixel { B = Convert.ToByte(x * 2), R = Convert.ToByte(y * 2), G = 100 };
                }
            }


            header.size = GetSize(header) + SizeOf<InfoHeader>() + (SizeOf<Pixel>() * (uint)infoHeader.width * (uint)infoHeader.height);
            var bytes = GetBytes(header).ToList();
            bytes.AddRange(GetBytes(infoHeader));

            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    bytes.AddRange(GetBytes(imageData[i, j]));
                }

            }


            File.WriteAllBytes("test.bmp", bytes.ToArray());
            Console.WriteLine("Hello World!");
        }
    }



    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BitmapHeader
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] type;                 /* Magic identifier            */
        public uint size;                       /* File size in bytes          */
        public ushort reserved1, reserved2;
        public uint offset;                     /* Offset to image data, bytes */
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct InfoHeader
    {
        public uint size;               /* Header size in bytes      */
        public int width, height;                /* Width and height of image */
        public ushort planes;       /* Number of colour planes   */
        public ushort bits;         /* Bits per pixel            */
        public uint compression;        /* Compression type          */
        public uint imagesize;          /* Image size in bytes       */
        public int xresolution, yresolution;     /* Pixels per meter          */
        public uint ncolours;           /* Number of colours         */
        public uint importantcolours;   /* Important colours         */
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Pixel
    {
        public byte B;

        public byte G;
        public byte R;

    }
}
