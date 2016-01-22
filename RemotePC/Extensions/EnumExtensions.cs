using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using RemotePC.Attributes;

namespace RemotePC.Extensions
{
    public static class EnumExtensions
    {
        public static bool IsFollowUpEnabled(this Enum e)
        {
            var type = e.GetType();            

            var enumValue = type.GetMember(e.ToString());

            var value = enumValue.FirstOrDefault();
            if (value == null)
            {
                return false;
            }

            var attributes = value.GetCustomAttributes(typeof(FollowUpAttribute), false);
            var attribute = attributes.FirstOrDefault();

            return attribute != null;
        }
    }
}