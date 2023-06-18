using System.Reflection;

namespace BinaryPatrick.ArgumentHelper.Models
{

    internal class RequiredArgument : Argument<RequiredArgumentAttribute>
    {
        public RequiredArgument(PropertyInfo propertyInfo, RequiredArgumentAttribute argumentAttribute) : base(propertyInfo, argumentAttribute) { }
    }
}
