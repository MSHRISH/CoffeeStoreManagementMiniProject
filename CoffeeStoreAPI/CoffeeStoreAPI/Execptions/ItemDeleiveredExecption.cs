using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class ItemDeleiveredExecption : Exception
    {
        public string message;
        public ItemDeleiveredExecption()
        {
            message = "Item was already deleivered";
        }
        public override string Message => message;


    }
}