using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class NoSuchUserException : Exception
    {
        string message;
        public NoSuchUserException()
        {
            message = "NO such User Exist";
        }
        public override string Message => message;

    }
}