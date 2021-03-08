namespace vt_encrypchat.Presentation.WebModels
{
    public class ErrorViewModel
    {
        public string Error { get; set; }
        public ExceptionViewModel Exception { get; set; }
    }

    public class ExceptionViewModel
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}