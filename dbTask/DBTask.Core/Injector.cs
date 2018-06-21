using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace DBTask.Core
{
    class Injector

    {
        private IUnityContainer container;
        private static Injector Instance = null;
        private static readonly object lockObj = new object();
        private Injector() : this(new UnityContainer())
        {

        }
        private Injector(IUnityContainer container)
        {
            var sectionconfig = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            if (sectionconfig != null)
            {
                sectionconfig.Configure(this.container);
            }
            this.container = container;

        }

        public static Injector GetInstance()
        {
            if (Instance == null)
            {


                lock (lockObj)
                {
                    if (Instance == null)
                    {
                        Instance = new Injector();
                    }
                }

            }
            return Instance;
        }
        public T Resolve<T>()
        {
            T ret = default(T);

            if (this.container.IsRegistered(typeof(T)))
            {
                ret = this.container.Resolve<T>();
            }
            return ret;
        }
        public void RegisterType<T>(T instance)
        {
            this.container.RegisterInstance<T>(instance);
        }
    }
}
