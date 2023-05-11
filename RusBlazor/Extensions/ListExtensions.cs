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
            Type? unsignedType;
            if (!typeof(T).IsWholeNumberType())
                throw new ArgumentException($"Type {typeof(T)} is not supported here.");
            else if (!typeof(T).TryGetTypeAsUnsigned(out unsignedType))
                throw new ArgumentException($"Type {typeof(T)} does not have a supported unsigned type.");

            dynamic result = 0;

            if (typeof(T) == unsignedType)
            {
                foreach (var item in list)
                {
                    result += (dynamic)item;
                }
            }
            else
            {
                dynamic unsignedResult = Activator.CreateInstance(unsignedType);
                foreach (var item in list)
                {
                    unsignedResult += Convert.ChangeType((dynamic)item, unsignedType);
                }
                result = Convert.ChangeType(unsignedResult, typeof(T));
            }


            return (T)result;
        }

        public static void GetFlagsByValue<T>(this List<T> list, T flag)
        {
            Type? unsignedType;
            if (!typeof(T).IsWholeNumberType())
                throw new ArgumentException($"Type {typeof(T)} is not supported here.");
            else if (!typeof(T).TryGetTypeAsUnsigned(out unsignedType))
                throw new ArgumentException($"Type {typeof(T)} does not have a supported unsigned type.");

            dynamic value = Convert.ChangeType(flag, unsignedType);
            dynamic one = Convert.ChangeType(1, unsignedType);
            list.Clear();

            int size = Marshal.SizeOf(default(T)) * 8;
            for (int i = 0; i < size; i++)
            {
                dynamic mask = one << i;
                if ((value & mask) != 0)
                {
                    list.Add((T)Convert.ChangeType(mask, typeof(T)));
                }
            }
        }
    }
}
