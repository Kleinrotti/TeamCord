namespace TeamCord.Core
{
    /// <summary>
    /// Provides methods to store and read data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStorage<T>
    {
        /// <summary>
        /// Store given data
        /// </summary>
        /// <param name="data"></param>
        void Store(T data);

        /// <summary>
        /// Read data
        /// </summary>
        /// <returns></returns>
        T Get();
    }
}