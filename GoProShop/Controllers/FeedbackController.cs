using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IResponseService _responseService;

        public FeedbackController(IFeedbackService feedbackService,
            IResponseService responseService)
        {
            _feedbackService = feedbackService ?? throw new ArgumentNullException(nameof(feedbackService));
            _responseService = responseService ?? throw new ArgumentNullException(nameof(responseService));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Create(FeedbackVM feedbackVM)
        {
            if (feedbackVM == null)
                throw new ArgumentNullException(nameof(feedbackVM));

            if (!ModelState.IsValid)
                return PartialView("_MainFeedback", feedbackVM);

            await _feedbackService.CreateAsync(Mapper.Map<FeedbackDTO>(feedbackVM));
            var response = Mapper.Map<ResponseVM>(_responseService.Create(true,
                "Cпасибо за ваш отзыв. Нам важно ваше мнение!"));

            return PartialView("~/Views/Shared/_Response.cshtml", response);
        }
    }
}