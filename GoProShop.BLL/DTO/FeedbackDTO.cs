using System;

namespace GoProShop.BLL.DTO
{
    public class FeedbackDTO : IdProvider
    {
        public string Name { get; set; }

        public string SurName { get; set; }

        public string Email { get; set; }

        public DateTime Date { get; set; }

        public bool? IsApproved { get; set; }

        public string Message { get; set; }
    }
}
