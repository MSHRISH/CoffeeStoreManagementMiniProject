using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class ItemNotAvailableExecption : Exception
    {
        public string message;
        public ItemNotAvailableExecption()
        {
            message = "Item is not available";
        }

        public override string Message => message;

    }
}