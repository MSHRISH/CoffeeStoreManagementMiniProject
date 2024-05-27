using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class NoSuchOrderFoundExecption : Exception
    {
        public string message;
        public NoSuchOrderFoundExecption()
        {
            message = "No Such Order Found";
        }
        public override string Message => message;

    }
}