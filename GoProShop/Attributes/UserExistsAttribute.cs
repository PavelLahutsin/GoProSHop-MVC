using System;
using System.ComponentModel.DataAnnotations;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.Interfaces;
using GoProShop.ViewModels;

namespace GoProShop.Attributes
{
    public class UserExistsAttribute : ValidationAttribute
    {
        private readonly IUserService _userService;

        public UserExistsAttribute(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        protected override ValidationResult IsValid(object obj, ValidationContext context)
        {
            if (context.ObjectInstance is UserRegisterVM userVM)
            {
                if (_userService.IsUserExist(userVM.UserName).Result)
                {
                    return new ValidationResult(this.FormatErrorMessage(context.DisplayName));
                }
            }

            return ValidationResult.Success;
        }
    }
}