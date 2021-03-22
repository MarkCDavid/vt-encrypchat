using System;
using vt_encrypchat.Presentation.WebModels.User;

namespace vt_encrypchat.Presentation.WebModels.Messages
{
    public class MessageViewModel
    {
        public string FromValue { get; set; }
        public string ToValue { get; set; }
        
        public DateTime DateTime { get; set; }
        public UserViewModel From { get; set; }
        public UserViewModel To { get; set; }
    }
}