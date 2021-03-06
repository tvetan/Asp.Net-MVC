﻿namespace TwitterCopy.Tests.Common.DataFakes
{
    using System;
    using System.Reflection;

    public static class ObjectsExtensions
    {
        public static object ObjectClone(this object obj)
        {
            var type = obj.GetType();
            var copy = Activator.CreateInstance(type);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            foreach (var field in fields)
            {
                field.SetValue(copy, field.GetValue(obj));
            }

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            foreach (var property in properties)
            {
                if (property.CanWrite)
                {
                    property.SetValue(copy, property.GetValue(obj));
                }
            }

            return copy;
        }
    }
}