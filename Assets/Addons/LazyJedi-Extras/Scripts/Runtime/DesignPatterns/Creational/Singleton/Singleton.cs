using System;

namespace LazyJedi.DesignPatterns.Creational
{
    /// <summary>
    /// The base class for creating a singleton class. <br/>
    /// These are for non-MonoBehaviour classes.
    /// 
    /// Lazy is a class provided by the .NET framework that allows you to defer the creation of an instance of a type until it's actually needed.
    /// It's particularly useful for scenarios where the instance creation might be resource-intensive or
    /// where you want to ensure that an instance is only created when it's required.
    /// The Lazy class employs a technique known as lazy initialization,
    /// which means that the instance is created only when you access its Value property for the first time.
    /// Subsequent accesses to the Value property return the previously created instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> where T : class, new()
    {
        private static Lazy<T> _instance = new Lazy<T>(() => new T());

        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance.Value;
                }
                _instance ??= new Lazy<T>(() => new T());
                return _instance.Value;
            }
        }
    }
}