using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RusBlazor.Extensions
{
    public static class ListExtensions
    {
        public static T FlagsToValue<T>(this List<T> list)
        {
            if (!GenericIsWholeNumber<T>())
                throw new ArgumentException($"Type {typeof(T)} is not supported here.");

            dynamic result = 0;
            foreach (var item in list)
            {
                result += (dynamic)item;
            }
            return (T)result;
        }

        public static void GetFlagsByValue<T>(this List<T> list, T flag)
        {
            if (!GenericIsWholeNumber<T>())
                throw new ArgumentException($"Type {typeof(T)} is not supported here.");

            dynamic value = flag;
            dynamic one = Convert.ChangeType(1, typeof(T));
            list.Clear();

            if (value < 0)
            {
                list.Add(flag);
            }
            else
            {
                int size = Marshal.SizeOf(default(T)) * 8;
                for (int i = 0; i < size; i++)
                {
                    dynamic mask = one << i;
                    if ((value & mask) != 0)
                    {
                        list.Add((T)mask);
                    }
                }
            }
        }


        static bool GenericIsWholeNumber<T>()
        {
            return (typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte) ||
                typeof(T) == typeof(short) || typeof(T) == typeof(ushort) ||
                typeof(T) == typeof(int) || typeof(T) == typeof(uint) ||
                typeof(T) == typeof(long) || typeof(T) == typeof(ulong));
        }
    }
}
