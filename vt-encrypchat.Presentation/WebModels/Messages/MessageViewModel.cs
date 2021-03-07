using System;
using vt_encrypchat.Presentation.WebModels.User;

namespace vt_encrypchat.Presentation.WebModels.Messages
{
    public class MessageViewModel
    {
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public UserViewModel From { get; set; }
        public UserViewModel To { get; set; }
    }
}