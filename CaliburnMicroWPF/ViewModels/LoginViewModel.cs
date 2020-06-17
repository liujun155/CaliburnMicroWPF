using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SystemManage.ViewModels;

namespace CaliburnMicroWPF.ViewModels
{
    //[Export(typeof(LoginViewModel))]
    public class LoginViewModel : Screen
    {
        public bool IsLogin { get; set; } = false;
        public LoginViewModel()
        {
        }

        public void ClickMe()
        {
            MessageBox.Show("登录成功", "提示");
            IsLogin = true;
            this.TryClose();
        }
    }
}
