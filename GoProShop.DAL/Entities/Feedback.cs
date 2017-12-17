using System;

namespace GoProShop.DAL.Entities
{
    public class Feedback : IdProvider
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime Date { get; set; }

        public bool? IsApproved { get; set; }
        
        public string Message { get; set; }
    }
}
