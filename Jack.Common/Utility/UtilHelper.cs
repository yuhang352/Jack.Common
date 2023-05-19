using System;
using System.IO;
using System.Text;
using YH.Common.Extensions;

namespace YH.Common.Utility
{
    public class UtilHelper
    {
        public static string Base64Encode(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        public static string Base64Decode(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(bytes);
        }

        public static decimal Ex4s5R(decimal obj, int i)
        {
            return Math.Round(obj, i, MidpointRounding.AwayFromZero);
        }

        public static decimal Ex4s5R(decimal obj)
        {
            return Ex4s5R(obj, 2);
        }

        public static decimal ExRu(decimal obj, int i)
        {
            int num = obj.ToInt();
            if (obj - (decimal)num <= 0m)
            {
                return num;
            }

            string text = "0.";
            for (int j = 0; j < i; j++)
            {
                text += "0";
            }

            text += "5";
            decimal num2 = Convert.ToDecimal(text);
            return Ex4s5R(obj + num2, i);
        }

        public static decimal ExRu(decimal obj)
        {
            return ExRu(obj, 2);
        }

        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] array = new byte[stream.Length];
            stream.Read(array, 0, array.Length);
            stream.Seek(0L, SeekOrigin.Begin);
            return array;
        }

        public static Stream BytesToStream(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }
    }
}
