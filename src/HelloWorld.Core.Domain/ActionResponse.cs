namespace HelloWorld.Core.Domain
{
    public class ActionResponse
    {
        public static ActionResponse Ok;
        public static ActionResponse Fail;

        public ActionResponse(bool success = false)
        {
            Success = success;
        }

        public bool Success { get; }
    }
}