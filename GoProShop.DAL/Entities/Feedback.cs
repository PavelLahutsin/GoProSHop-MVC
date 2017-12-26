using GoProShop.DAL.Enums;
using System;

namespace GoProShop.DAL.Entities
{
    public class Feedback : IdProvider
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime Date { get; set; }

        public FeedbackStatus Status { get; set; }
        
        public bool IsViewed { get; set; }

        public string Message { get; set; }
    }
}
