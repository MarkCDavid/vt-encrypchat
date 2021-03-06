using System;

namespace vt_encrypchat.WebModels
{
    public class MessageViewModel
    {
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public UserViewModel From { get; set; }
        public UserViewModel To { get; set; }
    }
}