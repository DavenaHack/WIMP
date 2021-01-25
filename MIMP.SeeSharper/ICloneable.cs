using System;

namespace MIMP.SeeSharper
{
    /// <summary>
    /// Clone a instance of type <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICloneable<T> : ICloneable
    {

        /// <summary>
        /// Create a new instance of this with same value (not references)
        /// </summary>
        /// <returns>cloned instance</returns>
        public new T Clone();

    }
}
