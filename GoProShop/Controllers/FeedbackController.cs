﻿using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Threading;
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

        public ActionResult Create(int? productId)
        {
            if (productId.HasValue)
            {
                ViewBag.ProductId = productId.Value;
            }

            return PartialView("_Create");
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Create(FeedbackVM feedbackVM)
        {
           await Task.Delay(700);
            if (!ModelState.IsValid)
            {
                if (feedbackVM.ProductId.HasValue)
                {
                    ViewBag.ProductId = feedbackVM.ProductId;
                }

                return PartialView("_Create", feedbackVM);
            }
               
            await _feedbackService.CreateAsync(Mapper.Map<FeedbackDTO>(feedbackVM));
            var response = Mapper.Map<ResponseVM>(_responseService.Create(true,
                "Cпасибо за ваш отзыв. Нам важно ваше мнение!"));

            return Json(response);
        }

        public ActionResult GetProductFeedbacks(int productId, int page = 1)
        {

            var feedbacksDTO = _feedbackService.GetProductFeedbacks(productId);
            var feedbacksVM = Mapper.Map<IEnumerable<FeedbackDTO>, IEnumerable<BaseFeedbackVM>>(feedbacksDTO);

            ViewBag.ProductId = productId;
            return PartialView("_ProductFeedbacks", feedbacksVM);
        }

        public ActionResult GetHomeFeedbacks()
        {
            var feedbacksDTO = _feedbackService.GetHomeFeedbacks();
            var feedbacksVM = Mapper.Map<IEnumerable<FeedbackDTO>, IEnumerable<FeedbackVM>>(feedbacksDTO);

            return PartialView("_HomeFeedbacks", feedbacksVM);
        }

        [Authorize(Roles = "admin")]
        public ActionResult GetAdminFeedbacks(int page = 1)
        {
            var feedbacksDTO = _feedbackService.GetFeedbacks();
            var feedbacksVM = Mapper.Map<IEnumerable<FeedbackDTO>, IEnumerable<FeedbackVM>>(feedbacksDTO);

            return PartialView("_AdminFeedbacks", feedbacksVM.ToPagedList(page,7));
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> View(int id)
        {
            var pendingFeedbackCount = await _feedbackService.ViewFeedback(id);

            return Json(new { Count = pendingFeedbackCount }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var feedbackDTO = await _feedbackService.GetAsync(id);

            if (feedbackDTO == null)
                return Json(_responseService.Create(false, "Отзыва по вашему запросу не существует"));

            var feedbackVM = Mapper.Map<BaseFeedbackVM>(feedbackDTO);
            return PartialView("_Edit", feedbackVM);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(BaseFeedbackVM feedback)
        {
            if (!ModelState.IsValid)
                return PartialView("_Edit", feedback);

            var response = await _feedbackService.UpdateAsync(Mapper.Map<FeedbackDTO>(feedback), "Отзыв");

            return Json(response);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = Mapper.Map<ResponseVM>(await _feedbackService.DeleteAsync(id, "Отзыв"));
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}