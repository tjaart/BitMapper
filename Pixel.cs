using System;
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

        public Pixel Blend(Pixel pixel, int opacityp)
        {
            var opacity = opacityp;
            byte OpacityBlend(byte value, byte secondValue)
            {
                var result = (((value / 100f) * opacity) + ((secondValue / 100f) * (100f - opacity)));
                return Convert.ToByte(result);
            }

            return new Pixel
            {
                B = OpacityBlend(pixel.B, B),
                G = OpacityBlend(pixel.G, G),
                R = OpacityBlend(pixel.R, R),
            };
        }
    }
}