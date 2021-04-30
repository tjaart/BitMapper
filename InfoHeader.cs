using System.Runtime.InteropServices;

namespace BitMapper
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct InfoHeader
    {
        public uint size; /* Header size in bytes      */
        public int width, height; /* Width and height of image */
        public ushort planes; /* Number of colour planes   */
        public ushort bits; /* Bits per pixel            */
        public uint compression; /* Compression type          */
        public uint imagesize; /* Image size in bytes       */
        public int xresolution, yresolution; /* Pixels per meter          */
        public uint ncolours; /* Number of colours         */
        public uint importantcolours; /* Important colours         */
    }
}