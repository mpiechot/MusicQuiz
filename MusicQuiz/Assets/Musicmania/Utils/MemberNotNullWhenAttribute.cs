#nullable enable

using System;

namespace Musicmania.Utils
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public sealed class MemberNotNullWhenAttribute : Attribute
    {
        public MemberNotNullWhenAttribute(bool returnValue, string member)
        {
            ReturnValue = returnValue;
            Member = member;
        }

        public bool ReturnValue { get; }
        public string Member { get; }
    }
}
