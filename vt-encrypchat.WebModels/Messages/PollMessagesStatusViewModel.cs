namespace vt_encrypchat.Presentation.WebModels.Messages
{
    public class PollMessagesStatusViewModel
    {
        public bool NewMessages { get; set; }

        public static PollMessagesStatusViewModel HasNewMesssages()
        {
            return new() {NewMessages = true};
        }
        
        public static PollMessagesStatusViewModel NoNewMesssages()
        {
            return new() {NewMessages = false};
        }
    }
}