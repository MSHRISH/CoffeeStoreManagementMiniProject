using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class NoSuchBaristaFoundExecption : Exception
    {
        public string message;
        public NoSuchBaristaFoundExecption()
        {
            message = "No Such Barista Found";
        }
        public override string Message => message;


    }
}