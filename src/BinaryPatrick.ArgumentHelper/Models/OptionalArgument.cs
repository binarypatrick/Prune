using System.Reflection;

namespace BinaryPatrick.ArgumentHelper.Models
{
    internal class OptionalArgument : Argument<OptionalArgumentAttribute>
    {
        public OptionalArgument(PropertyInfo propertyInfo, OptionalArgumentAttribute argumentAttribute) : base(propertyInfo, argumentAttribute) { }

        public override bool TrySetValue(object obj, string value)
        {
            if (PropertyInfo.PropertyType == typeof(bool) && value is null)
            {
                PropertyInfo.SetValue(obj, true);
                return true;
            }

            return base.TrySetValue(obj, value);
        }
    }
}
