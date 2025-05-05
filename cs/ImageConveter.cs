//ImageConverter.cs
//Created by Torisima 2025
//License : https://github.com/Dev-Torisima/Imqutive-Codes/blob/main/LICENSE
//Using : BitChanger.cs

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Imqutive;

namespace Imqutive
{
      /// <summary>Convert Image</summary>
    public static class ImageConverter
    {
        private static readonly byte[] Const1 = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xff, 0x00, 0x00, 0x00, 0x00, 0xff, 0x00, 0x00, 0x00, 0x00, 0xff, 0x00, 0x00, 0x00, 0x00, 0xff, 0x42, 0x47, 0x52, 0x73, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private static readonly byte[] Const2 = new byte[] { 0x42, 0x4d };
        private static readonly byte[] Const3 = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x8a, 0x00, 0x00, 0x00, 0x7c, 0x00, 0x00, 0x00 };
        private static readonly byte[] Const4 = new byte[] { 0x01, 0x00, 0x20, 0x00, 0x03, 0x00, 0x00, 0x00 };

        private static readonly byte[] Const5 = new byte[] { 0x28, 0x00, 0x00, 0x00};
        private static readonly byte[] Const6 = new byte[] { 0x01, 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00};
        private static readonly byte[] Const7 = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};

        /// <summary>Convert Bitmap</summary>
        /// <param name="data">Image Data(RGBA)</param>
        /// <param name="width">Image Width</param>
        /// <param name="height">Image Height</param>
        public static byte[] ToBitmap(byte[] data, uint width, uint height)
        {
            List<byte> result = new List<byte>();
            result.AddRange(Const2);
            result.AddRange(BitChanger.ToBytes((uint)(0x8a + data.Length), ByteOrder.LittleEndian));
            result.AddRange(Const3);

            result.AddRange(BitChanger.ToBytes(width, ByteOrder.LittleEndian));
            result.AddRange(BitChanger.ToBytes((int)(-height), ByteOrder.LittleEndian));
            result.AddRange(Const4);
            result.AddRange(BitChanger.ToBytes((uint)(data.Length), ByteOrder.LittleEndian));
            result.AddRange(Const1);

            result.AddRange(data);

            return result.ToArray();
        }

        /// <summary>Convert Icon</summary>
        /// <param name="data">Image Data(Array of RGBA or Array of Png Datas)</param>
        /// <param name="width">Image Width</param>
        /// <param name="height">Image Height</param>
        /// <param name="type">1 : Icon, 2 : Cursor</param>
        /// <param name="hotspot_x">Hotspot X</param>
        /// <param name="hotspot_y">Hotspot Y</param>
        /// <param name="kind">0 : RGBA Array, 1 : Png Datas</param>
        public static byte[] ToIcon(byte[][] data, uint[] width, uint[] height, byte type, byte kind, byte hotspot_x = 0, byte hotspot_y = 0)
        {
            int num = data.Length;
            byte[][] data2;
            if (kind is 0)
            {
                data2 = new byte[num][];
                for (int i = 0; i < num; i++)
                {
                    data2[i] = ToIconBitmap(data[i], width[i], height[i]);
                }
            }
            else
            {
                data2 = data;
            }


                List<byte> result = new List<byte>();
            result.AddRange(new byte[2] { 0x00, 0x00 });
            result.AddRange(BitChanger.ToBytes((ushort)type, ByteOrder.LittleEndian));
            result.AddRange(BitChanger.ToBytes((ushort)num, ByteOrder.LittleEndian));

            int rl = 6 + num * 16;
            for (int i = 0; i < num; i++)
            {
                byte u = 0;
                if (width[i] is not 0 and not 256) u = (byte)width[i];
                result.Add(u);
                u = 0;
                if (height[i] is not 0 and not 256) u = (byte)height[i];
                result.Add(u);

                result.Add(0);
                result.Add(0);

                if (type is 1)
                {
                    result.AddRange(new byte[] { 0x01, 0x00 });
                    result.AddRange(new byte[] { 0x20, 0x00 });
                }
                else if (type is 2)
                {
                    result.AddRange(BitChanger.ToBytes((ushort)hotspot_x, ByteOrder.LittleEndian));
                    result.AddRange(BitChanger.ToBytes((ushort)hotspot_y, ByteOrder.LittleEndian));
                }
                else
                {
                    result.AddRange(BitChanger.ToBytes((ushort)0, ByteOrder.LittleEndian));
                    result.AddRange(BitChanger.ToBytes((ushort)0, ByteOrder.LittleEndian));
                }

                    result.AddRange(BitChanger.ToBytes((uint)data2[i].Length, ByteOrder.LittleEndian));
                result.AddRange(BitChanger.ToBytes((uint)rl, ByteOrder.LittleEndian));


                rl += data2[i].Length;
            }
            for (int i = 0; i < num; i++)
            {
                result.AddRange(data2[i]);
            }


            return result.ToArray();
        }
        
        /// <summary>Convert Bitmap for Icon</summary>
        /// <remarks>AND/XOR Bitmap</remarks>
        public static byte[] ToIconBitmap(byte[] data, uint width, uint height)
        {
            byte[] data2 = new byte[data.Length];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int h1 = (j + i * (int)width) * 4;
                    int h2 = (j + ((int)height - i - 1) * (int)width) * 4;
                    data2[h2] = data[h1 + 2];
                    data2[h2 + 1] = data[h1 + 1];
                    data2[h2 + 2] = data[h1];
                    data2[h2 + 3] = data[h1 + 3];
                }
            }

            byte[] data3 = new byte[(int)Math.Ceiling((double)((long)width / 8)) * height];
            for (int i = 0; i < data3.Length; i++)
            {
                data3[i] = 0;
            }
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int h1 = (j + i * (int)width) * 4 + 3;
                    int h2 = j / 8 + i * (int)Math.Ceiling((double)((long)width / 8));
                    if (data2[h1] <= 0x20)
                    {
                        data3[h2] += (byte)(0b01 << (7 - j % 8));
                    }
                }
            }



            List<byte> result = new List<byte>();

            result.AddRange(Const5);
            result.AddRange(BitChanger.ToBytes(width, ByteOrder.LittleEndian));
            result.AddRange(BitChanger.ToBytes(height * 2, ByteOrder.LittleEndian));
            result.AddRange(Const6);
            result.AddRange(BitChanger.ToBytes((uint)(data2.Length + data3.Length), ByteOrder.LittleEndian));
            result.AddRange(Const7);

            result.AddRange(data2);
            result.AddRange(data3);

            return result.ToArray();
        }

        /// <summary>Convert Animation Cursor from Bitmap Cursor</summary>
        /// <param name="data">Bitmap Cursor Datas</param>
        /// <param name="rate">frame rate</param>
        /// <param name="seq">sequence</param>
        public static byte[] ToAniCursor(byte[][] data, int[] rate, int[] seq)
        {
            if (rate.Length != seq.Length) return new byte[0];

            List<byte> result = new List<byte>();

            int num = data.Length;
            int size = 8 * (data.Length + rate.Length) + 84;
            int size2 = 0;
            for (int i = 0; i < num; i++)
            {
                size2 += data[i].Length;
            }
            size += size2;


            result.AddRange(new byte[4] { 0x52, 0x49, 0x46, 0x46 });
            result.AddRange(BitChanger.ToBytes((uint)(size - 8), ByteOrder.LittleEndian));
            result.AddRange(new byte[16] { 0x41, 0x43, 0x4F, 0x4E, 0x61, 0x6E, 0x69, 0x68, 0x24, 0x00, 0x00, 0x00, 0x24, 0x00, 0x00, 0x00 });
            result.AddRange(BitChanger.ToBytes((uint)num, ByteOrder.LittleEndian));
            result.AddRange(BitChanger.ToBytes((uint)rate.Length, ByteOrder.LittleEndian));
            result.AddRange(new byte[28] 
            {
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                0x72, 0x61, 0x74, 0x65,
            });

            result.AddRange(BitChanger.ToBytes((uint)(rate.Length * 4), ByteOrder.LittleEndian));
            for (int i = 0; i < rate.Length; i++)
            {
                result.AddRange(BitChanger.ToBytes((uint)rate[i], ByteOrder.LittleEndian));
            }


            result.AddRange(new byte[4] { 0x73, 0x65, 0x71, 0x20 });
            result.AddRange(BitChanger.ToBytes((uint)(rate.Length * 4), ByteOrder.LittleEndian));
            for (int i = 0; i < rate.Length; i++)
            {
                result.AddRange(BitChanger.ToBytes((uint)seq[i], ByteOrder.LittleEndian));
            }


            result.AddRange(new byte[4] { 0x4C, 0x49, 0x53, 0x54 });
            result.AddRange(BitChanger.ToBytes((uint)(4 + num * 8 + size2), ByteOrder.LittleEndian));
            result.AddRange(new byte[4] { 0x66, 0x72, 0x61, 0x6D });

            for (int i = 0; i < num; i++)
            {
                result.AddRange(new byte[4] { 0x69, 0x63, 0x6F, 0x6E });
                result.AddRange(BitChanger.ToBytes((uint)(data[i].Length), ByteOrder.LittleEndian));
                result.AddRange(data[i]);
            }


            return result.ToArray();
        }
    }
}
