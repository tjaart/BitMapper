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
            File.WriteAllBytes("test.bmp", bmp.Create());
        }
    }
}