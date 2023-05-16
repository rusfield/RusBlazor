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
            if (type.IsUnsignedNumberType())
            {
                FieldInfo? field = type.GetField("MaxValue");
                if (field != null)
                {
                    value = (T)field.GetValue(null);
                    return true;
                }
            }
            else
            {
                try
                {
                    value = (T)Convert.ChangeType(-1, type);
                    return true;
                }
                catch(Exception ex)
                {
                    // Not valid type
                }
            }

            value = default(T);
            return false;
        }

        public static bool TryGetTypeAsUnsigned(this Type type, out Type? value)
        {
            value = type.Name switch
            {
                "Byte" or "SByte" => typeof(byte),
                "Int16" or "UInt16" => typeof(ushort),
                "Int32" or "UInt32" => typeof(uint),
                "Int64" or "UInt64" => typeof(ulong),
                _ => null
            };

            return value != null;
        }

        public static bool IsUnsignedNumberType(this Type type)
        {
            var unsignedTypes = new[] { typeof(byte), typeof(ushort), typeof(uint), typeof(ulong) };
            return unsignedTypes.Contains(type);
        }
    }
}
