using System.Runtime.Serialization;

namespace CoffeeStoreAPI.Execptions
{
    [Serializable]
    internal class PreparationStartedExecption : Exception
    {
        public string message;
        public PreparationStartedExecption()
        {
            message = "Cannot Cancel Item, Preparation Already Started or already deleivered";
        }

        public override string Message =>message;

    }
}