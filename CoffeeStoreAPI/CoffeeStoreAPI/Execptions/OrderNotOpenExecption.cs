using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class OrderNotOpenExecption : Exception
    {
        public string message;
        public OrderNotOpenExecption()
        {
            message = "Cannot Add items order is closed!";
        }
        public override string Message => message;
    }
}