using System;

namespace CipherPark.ExchangeTools.Utility
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UrlParameterAttribute
        : Attribute
    {
        public UrlParameterAttribute(string name) { Name = name; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public bool IsFlag { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class UrlParameterIgnoreAttribute : Attribute
    { }
}
