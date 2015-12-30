using System.ComponentModel;

namespace Utils
{
    public class EnumDescription<T>
    {
        public static string GetDescription(T enumValue)
        {
            var fi = enumValue.GetType().GetField(enumValue.ToString());
            if (null != fi)
            {
                var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (0 < attributes.Length)
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }
            return enumValue.ToString();
        }
    }
}
