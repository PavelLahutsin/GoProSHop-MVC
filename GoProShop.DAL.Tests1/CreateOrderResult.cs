using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProShop.DAL.Tests1
{
    class CreateOrderResult
    {
        public bool IsPaymentTypeErrorMessageDisplayed { get; set; }

        public bool IsEmailErrorMessageDisplayed { get; set; }

        public bool IsPhoneErrorMessageDisplayed { get; set; }

        public bool IsDeliveryTypeErrorMessageDisplayed { get; set; }

        public bool IsOrderDateErrorMessageDisplayed { get; set; }

    }
}
