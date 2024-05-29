using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class UnableToCancelOrderExecption : Exception
    {
        public string message;
        public UnableToCancelOrderExecption()
        {
            message = "Unable to cancel order,order must be closed or already cancelled.Check Status";
        }

        public override string Message => message;

    }
}