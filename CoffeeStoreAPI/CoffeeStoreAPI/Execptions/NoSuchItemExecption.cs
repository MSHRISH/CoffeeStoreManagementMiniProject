using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class NoSuchItemExecption : Exception
    {
        string message;
        public NoSuchItemExecption()
        {
            message = "No such Item Exists";
        }
        public override string Message => message;

    }
}