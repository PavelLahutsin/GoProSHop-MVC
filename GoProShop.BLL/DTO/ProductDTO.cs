using GoProShop.BLL.Enums;
using System.Collections.Generic;
using System.Linq;

namespace GoProShop.BLL.DTO
{
    public class ProductDTO : IdProvider
    {
        public string Name { get; set; }

        public int ProductSubGroupId { get; set; }

        public decimal Price { get; set; }

        public byte[] Image { get; set; }

        public string MimeType { get; set; }

        public string Description { get; set; }

        public ProductStatus? Status { get; set; }

        public int? FeedbackAmount => Feedbacks.Where(y => y?.Status == FeedbackStatus.Approved).Count();

        public virtual ICollection<FeedbackDTO> Feedbacks { get; set; } = new HashSet<FeedbackDTO>();

        public double? AverageRating => 
            Feedbacks.Where(y => y?.Status == FeedbackStatus.Approved).Average(x => x?.Rating);
    }
}
