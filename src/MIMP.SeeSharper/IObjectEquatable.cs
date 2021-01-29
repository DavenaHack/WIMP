using System;

namespace MIMP.SeeSharper
{
    /// <summary>
    /// A interface to remember the developer to override the <see cref="object.Equals(object?)"/> method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObjectEquatable<T> : IEquatable<IObjectEquatable<T>>, IEquatable<object> where T : IObjectEquatable<T>
    {

        bool IEquatable<IObjectEquatable<T>>.Equals(IObjectEquatable<T> other) =>
            Equals(this, other);

        public bool Equals(T other) =>
            Equals(this, other);

    }

}
