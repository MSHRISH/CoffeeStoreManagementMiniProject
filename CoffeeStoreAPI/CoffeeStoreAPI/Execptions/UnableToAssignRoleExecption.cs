using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class UnableToAssignRoleExecption : Exception
    {
        public string message;
        public UnableToAssignRoleExecption()
        {
            message = "Unable to Assign Role";
        }
        public override string Message => message;

    }
}