using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RusBlazor.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsWholeNumberType(this Type type)
        {
            return (type == typeof(byte) || type == typeof(sbyte) ||
                type == typeof(short) || type == typeof(ushort) ||
                type == typeof(int) || type == typeof(uint) ||
                type == typeof(long) || type == typeof(ulong));
        }

        public static bool TryGetMaxValue<T>(this Type type, out T value)
        {
            FieldInfo? field = type.GetField("MaxValue");
            if (field != null)
            {
                value = (T)field.GetValue(null);
                return true;
            }
            value = default(T);
            return false;
        }
    }
}
