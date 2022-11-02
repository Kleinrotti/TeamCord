using System;

namespace TeamCord.Core
{
    /// <summary>
    /// Generic EventArgs class to provide easy and simple data transport via events
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericEventArgs<T> : EventArgs
    {
        private readonly T _data;

        public GenericEventArgs(T data)
        {
            _data = data;
        }

        /// <summary>
        /// Stored data of this object
        /// </summary>
        public T Data
        {
            get
            {
                return _data;
            }
        }
    }
}