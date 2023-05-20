using System;
using System.Reflection;
using LocoSoftware.Configuration.Attributes.Exceptions;

namespace LocoSoftware.Configuration.Attributes
{

    public static partial class Helpers
    {
        /// <summary>
        /// Check if Type has an Attribute
        /// </summary>
        /// <typeparam name="TObjectType"></typeparam>
        /// <typeparam name="TAttributeType"></typeparam>
        /// <returns></returns>
        public static Boolean HasAttribute<TObjectType, TAttributeType>() where TAttributeType : Attribute
        {
            Attribute attr = Attribute.GetCustomAttribute(typeof(TObjectType), typeof(TAttributeType));
            return attr != null;
        }
    
        /// <summary>
        /// Check if a Property of a Type has a Attribute
        /// </summary>
        /// <param name="propertyName"></param>
        /// <typeparam name="TObjectType"></typeparam>
        /// <typeparam name="TAttributeType"></typeparam>
        /// <returns></returns>
        /// <exception cref="PropertyNotFoundException"></exception>
        public static Boolean HasAttribute<TObjectType, TAttributeType>(String propertyName) where TAttributeType : Attribute
        {
            Type t = typeof(TObjectType);
            PropertyInfo property = t.GetProperty(propertyName);
            if (property == null)
            {
                throw new PropertyNotFoundException(
                    $"Object of Type {nameof(TObjectType)} has no Public Accessible Property {propertyName}");
            }

            return Attribute.IsDefined(property, typeof(TAttributeType));
        }
    }    
}
