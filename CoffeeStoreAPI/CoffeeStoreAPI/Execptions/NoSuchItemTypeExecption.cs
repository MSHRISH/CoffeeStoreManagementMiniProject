namespace CoffeeStoreAPI.Execptions
{
    public class NoSuchItemTypeExecption:Exception
    {
        public string message;
        public NoSuchItemTypeExecption()
        {
            message = "No such Item Type Exists";
        }
        public override string Message => message;
    }
}
