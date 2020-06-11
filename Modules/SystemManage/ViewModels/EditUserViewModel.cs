using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SystemManage.ViewModels
{
	[Export(typeof(EditUserViewModel))]
	public class EditUserViewModel : Screen
    {
		private IWindowManager m_windowManager;
		public EditUserViewModel(IWindowManager windowManager)
		{
			m_windowManager = windowManager;
		}

		public void ClickMe()
		{
			MessageBox.Show("点击我了", "提示");
		}

		public void ClickAgain()
		{
			m_windowManager.ShowWindow(new TestViewModel());
		}
	}
}
