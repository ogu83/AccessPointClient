
namespace Common
{
    public class OperationResult<T> where T : class
    {
        public bool Success { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public T ReturnValue { get; set; }

        public JsonResult ToJsonResult()
        {
            return new JsonResult
            {
                Success = Success,
                ErrorCode = ErrorCode,
                Message = Message
            };
        }
    }
}
