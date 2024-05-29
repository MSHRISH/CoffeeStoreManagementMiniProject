using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class ItemCancelledExecption : Exception
    {
        public string message;
        public ItemCancelledExecption()
        {
            message = "Cannot change status of an item that is cancelled";
        }
        public override string Message => message;

    }
}