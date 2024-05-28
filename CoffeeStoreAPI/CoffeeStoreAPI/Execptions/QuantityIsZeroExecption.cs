using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class QuantityIsZeroExecption : Exception
    {
        public string message;
        public QuantityIsZeroExecption()
        {
            message = "The Quantity is Zero";
        }

        public override string Message => message;

    }
}