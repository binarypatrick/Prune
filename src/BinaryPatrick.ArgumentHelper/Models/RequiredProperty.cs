using System.Reflection;

namespace BinaryPatrick.ArgumentHelper.Models;


internal class RequiredProperty : BaseProperty<RequiredArgumentAttribute>
{
    public RequiredProperty(PropertyInfo propertyInfo, RequiredArgumentAttribute argumentAttribute) : base(propertyInfo, argumentAttribute) { }
}
