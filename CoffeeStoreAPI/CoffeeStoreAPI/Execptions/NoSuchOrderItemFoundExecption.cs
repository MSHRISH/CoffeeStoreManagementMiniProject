using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class NoSuchOrderItemFoundExecption : Exception
    {
        public string message;
        public NoSuchOrderItemFoundExecption()
        {
            message = "No Such Order Item Found";
        }
        public override string Message => message;
    }
}