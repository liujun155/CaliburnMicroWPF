using Caliburn.Micro;
using CaliburnMicroWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SystemManage.ViewModels;

namespace CaliburnMicroWPF
{
    [Export(typeof(MainViewModel))]
    public class MainViewModel : Screen
    {
        public IWindowManager m_windowManager { get; set; }
        [ImportingConstructor]      //必须加入这个，这是因为用MEF在创建LoginViewModel实例时，有[ImportingConstructor]标记的构造函数的参数会自动使用容器内相应的export对象
        public MainViewModel(IWindowManager windowManager)
        {
            m_windowManager = windowManager;
        }

        protected override void OnInitialize()
        {
            var vm = new LoginViewModel();
            bool? dialogResult = m_windowManager.ShowDialog(vm);
            if (vm.IsLogin == false)
                Application.Current.Shutdown();
            base.OnInitialize();
        }

        /// <summary>
        /// 系统管理弹窗
        /// </summary>
        public void ShowWindow()
        {
            m_windowManager.ShowWindow(new EditUserViewModel(m_windowManager));
        }
    }
}
