using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class ItemTypeAlreadyExistsExecption : Exception
    {
        public string message;
        public ItemTypeAlreadyExistsExecption()
        {
            message = "The Item Type Already Exists";
        }

        public override string Message => message;

    }
}