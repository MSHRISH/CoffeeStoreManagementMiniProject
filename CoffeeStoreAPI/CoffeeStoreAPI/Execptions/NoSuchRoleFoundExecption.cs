using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class NoSuchRoleFoundExecption : Exception
    {
        public string message;
        public NoSuchRoleFoundExecption()
        {
            message = "No Such Role Found";
        }

        public override string Message => message;

    }
}