using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class PhoneAlreadyExistsExecption : Exception
    {
        public string message;
        public PhoneAlreadyExistsExecption()
        {
            message = "Phone Number already exists!";
        }
        public override string Message => message;

    }
}