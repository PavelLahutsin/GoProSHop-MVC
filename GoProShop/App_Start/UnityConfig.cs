using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.BLL.Services;
using GoProShop.DAL.Interfaces;
using GoProShop.DAL.EF;
using GoProShop.DAL.Repositories;
using System.Web;
using Microsoft.Owin.Security;
using GoProShop.ViewModels.Interfaces;
using GoProShop.ViewModels;

namespace GoProShop
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer()
                .RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>))
                .RegisterType<IGoProShopContext, GoProShopContext>()
                .RegisterType<IUnitOfWork, UnitOfWork>()          
                .RegisterType<IUserService, UserService>()
                .RegisterType<IProductSubGroupService, ProductSubGroupService>()
                .RegisterType<IProductService, ProductService>()
                .RegisterType<IProductGroupService, ProductGroupService>()
                .RegisterType<ICart, Cart>()
                .RegisterType<IFeedbackService, FeedbackService>()
                .RegisterType<IResponseService, ResponseService>()
                .RegisterType<IAuthenticationManager>(new InjectionFactory(x => HttpContext.Current.GetOwinContext().Authentication));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}