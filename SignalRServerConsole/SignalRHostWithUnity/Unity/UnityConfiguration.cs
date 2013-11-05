using System;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Practices.Unity;
using SignalRHostWithUnity.DataAccess;

namespace SignalRHostWithUnity.Unity
{
    public class UnityConfiguration
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<MyHub, MyHub>(new ContainerControlledLifetimeManager());
            container.RegisterType<IHubActivator, UnityHubActivator>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRepositoryUnityTestClass, RepositoryUnityTestClass>();
            
        }
    }
}
