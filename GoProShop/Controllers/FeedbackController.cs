using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using System;
using System.Collections.Generic;
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

        [Authorize(Roles = "admin")]
        public ActionResult GetAdminFeedbacks()
        {
            var feedbacksDTO = _feedbackService.GetFeedbacks();
            var feedbacksVM = Mapper.Map<IEnumerable<FeedbackDTO>, IEnumerable<FeedbackVM>>(feedbacksDTO);

            return PartialView("_AdminFeedbacks", feedbacksVM);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> View(int id)
        {
            var pendingFeedbackCount = await _feedbackService.ProcessFeedback(id);

            return Json(new { Count = pendingFeedbackCount }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var feedbackDTO = await _feedbackService.GetAsync(id);

            if (feedbackDTO == null)
                return Json(_responseService.Create(false, "Отзыва по вашему запросу не существует"));

            var feedbackVM = Mapper.Map<FeedbackVM>(feedbackDTO);
            return PartialView("_ViewFeedback", feedbackVM);        
        }
    }
}