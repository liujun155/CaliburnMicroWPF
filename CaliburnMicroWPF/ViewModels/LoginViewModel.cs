using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemManage.ViewModels;

namespace CaliburnMicroWPF.ViewModels
{
    [Export(typeof(LoginViewModel))]
    public class LoginViewModel : Screen
    {
        private IWindowManager m_windowManager;
        [ImportingConstructor]      //必须加入这个，这是因为用MEF在创建LoginViewModel实例时，有[ImportingConstructor]标记的构造函数的参数会自动使用容器内相应的export对象
        public LoginViewModel(IWindowManager windowManager)
        {
            m_windowManager = windowManager;
        }

        public void ClickMe()
        {
            m_windowManager.ShowWindow(new EditUserViewModel(m_windowManager));
        }
    }
}
