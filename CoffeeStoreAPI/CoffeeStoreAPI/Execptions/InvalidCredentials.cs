using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class InvalidCredentials : Exception
    {
        public string message;
        public InvalidCredentials()
        {
            message = "Invalid Password or Username";
        }
        public override string Message => message;

    }
}