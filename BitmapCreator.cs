using System;
using System.Linq;
using System.Text;
using static BitMapper.ByteStuff;

namespace BitMapper
{
    public class BitmapCreator
    {
        public BitmapCreator(int pixelWidth, int pixelHeight)
        {
            
        }
        
        public byte[] Create()
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

            return bytes.ToArray();
        }
    }
}