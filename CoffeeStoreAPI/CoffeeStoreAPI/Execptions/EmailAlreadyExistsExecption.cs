using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class EmailAlreadyExistsExecption : Exception
    {
        public string message;
        public EmailAlreadyExistsExecption()
        {
            message = "The Email Already Exists";
        }
        public override string Message => message;
    }
}