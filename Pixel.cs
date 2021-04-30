using System.Runtime.InteropServices;

namespace BitMapper
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Pixel
    {
        public static Pixel Create(byte R, byte G, byte B) =>
            new Pixel
            {
                B = B,
                G = G,
                R = R
            };
        public byte B;
        public byte G;
        public byte R;
    }
}