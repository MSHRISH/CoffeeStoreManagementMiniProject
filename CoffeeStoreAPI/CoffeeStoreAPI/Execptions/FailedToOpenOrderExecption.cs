using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class FailedToOpenOrderExecption : Exception
    {
        public string message; 
        public FailedToOpenOrderExecption()
        {
            message = "Failed To Open Order";
        }
        public override string Message => message;

    }
}