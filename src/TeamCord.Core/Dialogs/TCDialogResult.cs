namespace TeamCord.Core
{
    public class TCDialogResult<T>
    {
        public T Value { get; }
        public bool Confirmed { get; }

        internal TCDialogResult(T value)
        {
            Value = value;
        }

        internal TCDialogResult(T value, bool confirmed)
        {
            Value = value;
            Confirmed = confirmed;
        }
    }
}