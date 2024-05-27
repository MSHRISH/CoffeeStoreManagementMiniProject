using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class NoSuchManagerFoundExecption : Exception
    {
        public string message;
        public NoSuchManagerFoundExecption()
        {
            message = "No Such Manager Found";
        }

        public override string Message => message;

    }
}