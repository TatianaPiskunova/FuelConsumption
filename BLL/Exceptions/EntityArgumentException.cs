namespace BLL.Exceptions
{
    public class EntityArgumentException : Exception
    {
        public string ParamName { get; private set; }

        public EntityArgumentException(string paramName, string message) : base(message)
        {
            ParamName = paramName;
        }
    }
}
