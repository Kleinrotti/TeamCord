using System;

namespace TeamCord.Core
{
    /// <summary>
    /// Attribute to indicate a unit of a property value
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class UnitAttribute : Attribute
    {
        private string _unit;

        public UnitAttribute(string unit)
        {
            _unit = unit;
        }

        public virtual string Unit
        {
            get { return _unit; }
        }
    }
}