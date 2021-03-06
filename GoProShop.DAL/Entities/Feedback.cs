﻿using GoProShop.DAL.Enums;
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

        public int Rating { get; set; }

        public string Message { get; set; }

        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
