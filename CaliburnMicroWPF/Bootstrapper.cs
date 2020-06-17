using Caliburn.Micro;
using CaliburnMicroWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CaliburnMicroWPF
{
    /// <summary>
    /// 使用MEF IOC容器
    /// </summary>
    public class Bootstrapper : BootstrapperBase
    {
        //private SimpleContainer m_container;
        private CompositionContainer m_container;
        public Bootstrapper()
        {
            Initialize();
        }

        /// <summary>
        /// 配置IOC容器
        /// </summary>
        protected override void Configure()
        {
            //m_container = new SimpleContainer();
            //m_container.Instance(m_container);
            //m_container
            //    .Singleton<IWindowManager, WindowManager>()
            //    .Singleton<IEventAggregator, EventAggregator>();

            m_container = new CompositionContainer(
            new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x)))
            );

            var batch = new CompositionBatch();
            batch.AddExportedValue<IWindowManager>(new WindowManager());        //新建一个窗口管理器添加到IOC中
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());    //如果要使用弱事件就需要添加这个了
            batch.AddExportedValue(m_container);                                //只是个习惯，这样就可以在任何地方通过IOC使用container了
            m_container.Compose(batch);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// IOC容器获取实例的方法
        /// </summary>
        /// <param name="service"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected override object GetInstance(Type service, string key)
        {
            //try
            //{
            //    var obj = service?.Assembly.CreateInstance(service.FullName);
            //    return obj;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}

            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service) : key;
            var exports = m_container.GetExportedValues<object>(contract);

            var exportList = exports.ToList();//避免直接用exports时 调用2次IEnumerable操作
            if (exportList.Any())
                return exportList.First();
            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        /// <summary>
        /// IOC容器获取实例的方法
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            //return m_container.GetAllInstances(service);
            return m_container.GetExportedValues<object>(AttributedModelServices.GetContractName(service));
        }

        /// <summary>
        /// IOC容器注入实例的方法
        /// </summary>
        /// <param name="instance"></param>
        protected override void BuildUp(object instance)
        {
            //m_container.BuildUp(instance);
            m_container.SatisfyImportsOnce(instance);
        }

        /// <summary>
        /// 设置加载到AssemblySource中的程序集列表
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            List<Assembly> list = base.SelectAssemblies().ToList();
            list.AddRange(new List<Assembly>
            {
                Assembly.Load("SystemManage")
            });
            return list;
        }
    }
}
