using System;

namespace TeamCord.Core
{
    /// <summary>
    /// Attribute to indicate a type of a property value
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ControlTypeAttribute : Attribute
    {
        private Type _unit;

        public ControlTypeAttribute(Type unit)
        {
            _unit = unit;
        }

        public virtual Type ControlType
        {
            get { return _unit; }
        }
    }
}