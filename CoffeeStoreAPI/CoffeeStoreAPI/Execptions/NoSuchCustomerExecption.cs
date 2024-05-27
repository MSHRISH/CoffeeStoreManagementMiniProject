using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class NoSuchCustomerExecption : Exception
    {
        public string message;
        public NoSuchCustomerExecption()
        {
            message = "No Such Customer Found";
        }
        public override string Message => message;

    }
}