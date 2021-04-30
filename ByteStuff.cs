using System;
using System.Runtime.InteropServices;
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace BitMapper
{
    public class ByteStuff
    {
        /// <summary>
        /// Get the size of <T> in bytes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static uint SizeOf<T>() => Convert.ToUInt32(Marshal.SizeOf(typeof(T)));

        /// <summary>
        /// Get the size of an object in bytes.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static uint GetSize(object obj) => Convert.ToUInt32(Marshal.SizeOf(obj));

        /// <summary>
        /// Convert a struct/value type to bytes.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static byte[] GetBytes<T>(T value) where T : struct
        {
            int size = Marshal.SizeOf(value);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(value, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }
    }
}