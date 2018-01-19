using GoProShop.BLL.Services;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.EF;
using GoProShop.DAL.Interfaces;
using GoProShop.DAL.Repositories;
using GoProShop.ViewModels;
using GoProShop.ViewModels.Interfaces;
using Microsoft.Owin.Security;
using System;
using System.Web.Mvc;
using System.Web;
using Unity;
using Unity.Injection;
using GoProShop.Helpers.Interfaces;
using GoProShop.BLL.Helpers;

namespace GoProShop
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container
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
                .RegisterType<IPagedListHelper, PagedListHelper>()
                .RegisterType<ISearchService, SearchService>()
                .RegisterType<IAdminService, AdminService>()
                .RegisterType<IOrderService, OrderService>()
                .RegisterType<IEmailService, EmailService>()
                .RegisterType<IAuthenticationManager>(new InjectionFactory(x => HttpContext.Current.GetOwinContext().Authentication));
        }
    }
}