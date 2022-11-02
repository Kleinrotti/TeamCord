namespace TeamCord.Core
{
    public interface IFileStorable<T>
    {
        T Deserialize(string json);

        string Serialize();
    }
}