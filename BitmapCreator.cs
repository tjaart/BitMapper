using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BitMapper.ByteStuff;

namespace BitMapper
{
    public class BitmapCreator
    {
        public int PixelWidth { get; }
        public int PixelHeight { get; }
        private BitmapHeader _header;
        private List<byte> _headerData;
        private Pixel[,] _imageData;

        public BitmapCreator(int pixelWidth, int pixelHeight)
        {
            PixelWidth = pixelWidth;
            PixelHeight = pixelHeight;
            InitHeader();
            
            _imageData = new Pixel[PixelHeight, PixelWidth];
        }

        private void InitHeader()
        {
            _header = new BitmapHeader();
            _header.reserved1 = 0;
            _header.reserved2 = 0;
            var infoHeader = new InfoHeader();

            _header.type = Encoding.ASCII.GetBytes("BM");
            _header.offset = GetSize(_header) + GetSize(infoHeader);
            infoHeader.size = 40;
            infoHeader.bits = 24;
            infoHeader.compression = 0;
            infoHeader.height = PixelHeight;
            infoHeader.planes = 1;
            infoHeader.width = PixelWidth;
            infoHeader.imagesize = SizeOf<Pixel>() * (uint) infoHeader.height * (uint) infoHeader.width;
            infoHeader.xresolution = 2835;
            infoHeader.yresolution = 2835;
            infoHeader.ncolours = 0;
            
            _header.size = GetSize(_header) + SizeOf<InfoHeader>() + (SizeOf<Pixel>() * (uint) PixelWidth * (uint) PixelHeight);
            _headerData = GetBytes(_header).ToList();
            _headerData.AddRange(GetBytes(infoHeader));
        }

        public void Draw(Action<Pixel[,]> drawAction) => drawAction(_imageData);

        public byte[] ToBytes()
        {
            var finalImage = new List<byte>();
            
            finalImage.AddRange(_headerData);
            
            for (var j = 0; j < PixelHeight; j++)
            for (var i = 0; i < PixelWidth; i++)
            {
                finalImage.AddRange(GetBytes(_imageData[j, i]));
            }
            return finalImage.ToArray();
        }
    }
}