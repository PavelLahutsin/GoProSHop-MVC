using GoProShop.BLL.Enums;
using System;

namespace GoProShop.BLL.DTO
{
    public class FeedbackDTO : IdProvider
    {
        public string Name { get; set; }

        public string SurName { get; set; }

        public string Email { get; set; }

        public DateTime Date { get; set; }

        public FeedbackStatus Status { get; set; }

        public bool IsViewed { get; set; }

        public int Rating { get; set; }

        public int? ProductId { get; set; }

        public string Message { get; set; }
    }
}
