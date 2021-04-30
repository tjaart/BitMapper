using System.Runtime.InteropServices;

namespace BitMapper
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BitmapHeader
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] type; /* Magic identifier            */

        public uint size; /* File size in bytes          */
        public ushort reserved1, reserved2;
        public uint offset; /* Offset to image data, bytes */
    }
}