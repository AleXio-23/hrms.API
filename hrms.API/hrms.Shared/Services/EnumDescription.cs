﻿using System.ComponentModel;

namespace hrms.Shared.Services
{
    public static class EnumDescription
    {
        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name != null)
            {
                var field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(field,
                         typeof(DescriptionAttribute)) is DescriptionAttribute attr)
                    {
                        return attr.Description;
                    }
                }
            }
            throw new ArgumentException($"Wrong enum value {value}");
        }
    }
}
