using AppCore.DataAccess.Results.Bases;

namespace AppCore.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(false, message)
        {

        }

        public ErrorResult() : base(false, "")
        {

        }
    }
}
