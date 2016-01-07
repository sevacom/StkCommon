using System;
using System.Windows;
using Microsoft.Win32;
using StkCommon.UI.Wpf.ViewModels;
using StkCommon.UI.Wpf.Views.Windows;

namespace StkCommon.UI.Wpf.Views
{
	/// <summary>
	/// ��������� ������, ������������ �������.
	/// </summary>
	public interface IShowDialogAgent
	{
		/// <summary>
		/// ��������� ���� �� ��������� ��� ����������� � ShowMessageDialog, ���� �� ������� �������� �������� ����
		/// </summary>
		string DefaultWindowTitle { get; set; }

		/// <summary>
		/// �������� ������.
		/// </summary>
		/// <param name="dialogViewModel">ViewModel �������.</param>		
		/// <typeparam name="T">��� View �������.</typeparam>
		/// <returns>
		/// ������������ �������� ����� �� ��� � System.Windows.Window.ShowDialog().
		/// </returns>
		bool? ShowDialog<T>(object dialogViewModel) where T : Window, new();

		/// <summary>
		/// �������� ������.
		/// </summary>
		/// <param name="dialogViewModel">ViewModel �������.</param>
		/// <param name="owner">�������� ������������ ����, �������� null</param>
		/// <typeparam name="T">��� View �������.</typeparam>
		/// <returns>
		/// ������������ �������� ����� �� ��� � System.Windows.Window.ShowDialog().
		/// </returns>
		bool? ShowDialog<T>(object dialogViewModel, IWindow owner) where T : Window, new();

		/// <summary>
		/// �������� ���� � ����������� ������, owner � ���� ����������� ������� �������� ����
		/// </summary>
		/// <param name="viewModel">ViewModel ����.</param>
		/// <returns>��������� ���� ��� ���������� ���������</returns>
		IWindow Show<T>(object viewModel)
			where T : Window, IWindow, new();

		/// <summary>
		/// �������� ���� � ����������� ������
		/// </summary>
		/// <typeparam name="T">��� View ��� ����.</typeparam>
		/// <param name="viewModel">ViewModel ����.</param>
		/// <param name="owner">�������� ������������ ����</param>
		/// <returns>��������� ���� ��� ���������� ���������</returns>
		IWindow Show<T>(object viewModel, IWindow owner)
			where T : Window, IWindow, new();

        /// <summary>
        /// �������� MessageBox.
        /// </summary>
        /// <param name="message">����� ��� �����������.</param>
        /// <param name="caption">���������. ���� ������ �� ������ ��������� ��������� ����</param>
        /// <param name="button">
        /// ��������, ������������ ����� ������ ������ ��������� MessageBox.
        /// </param>
        /// <param name="icon">������ ��� �����������.</param>
        /// <returns>
        /// MessageBoxResult ���������� ����� ������ ����� ������������.
        /// </returns>
        MessageBoxResult ShowMessageDialog(string message, string caption,
            MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.Information);

		/// <summary>
		/// �������� MessageBox. � ���������� Caption �� �������� ��������� ����
		/// </summary>
		/// <param name="message">����� ��� �����������.</param>
		/// <param name="button">
		/// ��������, ������������ ����� ������ ������ ��������� MessageBox.
		/// </param>
		/// <param name="icon">������ ��� �����������.</param>
		/// <returns>
		/// MessageBoxResult ���������� ����� ������ ����� ������������.
		/// </returns>
		MessageBoxResult ShowMessageDialog(string message, MessageBoxButton button = MessageBoxButton.OK,
			MessageBoxImage icon = MessageBoxImage.Information);

        /// <summary>
        /// �������� MessageBox.
        /// </summary>
        /// <param name="message">����� ��� �����������.</param>
        /// <param name="caption">���������. ���� ������ �� ������ ��������� ��������� ����</param>
        /// <param name="button">
        /// ��������, ������������ ����� ������ ������ ��������� MessageBox.
        /// </param>
        /// <param name="icon">������ ��� �����������.</param>
        /// <param name="defaultButton">�������� ������������ ����� ������ ����� ������� �� ���������</param>
        /// <param name="options">Specifies special display options for a message box.</param>
        /// <returns>
        /// MessageBoxResult ���������� ����� ������ ����� ������������.
        /// </returns>
        MessageBoxResult ShowMessageDialog(string message, string caption,
            MessageBoxButton button, MessageBoxImage icon,
            MessageBoxResult defaultButton, MessageBoxOptions options);

		/// <summary>
		/// �������� MessageBox.
		/// </summary>
		/// <param name="owner">�������� ����, �������� null</param>
		/// <param name="message">����� ��� �����������.</param>
		/// <param name="caption">���������. ���� ������ �� ������ ��������� ��������� ����</param>
		/// <param name="button">
		/// ��������, ������������ ����� ������ ������ ��������� MessageBox.
		/// </param>
		/// <param name="icon">������ ��� �����������.</param>
		/// <param name="defaultButton">�������� ������������ ����� ������ ����� ������� �� ���������</param>
		/// <param name="options">Specifies special display options for a message box.</param>
		/// <returns>
		/// MessageBoxResult ���������� ����� ������ ����� ������������.
		/// </returns>
		MessageBoxResult ShowMessageDialog(
			IWindow owner, 
			string message, 
			string caption,
			MessageBoxButton button = MessageBoxButton.OK, 
			MessageBoxImage icon = MessageBoxImage.Information, 
			MessageBoxResult defaultButton = MessageBoxResult.OK,
			MessageBoxOptions options = MessageBoxOptions.None);

		/// <summary>
		/// �������� ������ � ���������� �� ������ ��� ��������� ����.
		/// </summary>
		void ShowErrorMessageDialog(string message, string details);

		/// <summary>
		/// �������� ������ � ���������� �� ������ � ��������� owner.
		/// </summary>
		void ShowErrorMessageDialog(IWindow owner, string message, string details, string caption);

		/// <summary>
		/// �������� ������ �������� �����
		/// </summary>
		/// <param name="filter"></param>
		/// <param name="fileName"></param>
		/// <param name="owner">�������� ������������ ����</param>
		/// <returns></returns>
		bool? ShowOpenFileDialog(string filter, out string fileName, IWindow owner = null);

		/// <summary>
		/// �������� ������ ���������� �����
		/// </summary>
		/// <param name="filter"></param>
		/// <param name="defFileName"></param>
		/// <param name="fileName"></param>
		/// <param name="owner">�������� ������������ ����</param>
		/// <returns></returns>
		bool? ShowSaveFileDialog(string filter, string defFileName, out string fileName, IWindow owner = null);
	}

	/// <summary>
	/// ���������� �������� ��������
	/// </summary>
	public class ShowDialogAgent : IShowDialogAgent
	{
		private readonly IDispatcher _dispatcher;
		private Window _theActiveWindow;

		/// <summary>
		/// �����������
		/// </summary>
		/// <param name="dispatcher"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public ShowDialogAgent(IDispatcher dispatcher)
		{
			if (dispatcher == null) throw new ArgumentNullException("dispatcher");
			_dispatcher = dispatcher;
			DefaultWindowTitle = string.Empty;
		}

		#region Properties

		/// <summary>
		/// ��������� ���� �� ��������� ��� ����������� � ShowMessageDialog ���� �� ������� �������� �������� ����
		/// </summary>
		public string DefaultWindowTitle { get; set; }

		/// <summary>
		/// ������� ���� ����������
		/// </summary>
		private Window MainWindow
		{
			get
			{
				if (!_dispatcher.CheckAccess())
				{
					return _dispatcher.Invoke(new Func<Window>(() => MainWindow)) as Window;
				}
				return Application.Current == null ? null : Application.Current.MainWindow;
			}
		}

		/// <summary>
		/// ������� �������� ���� ������ �� �������� ����� ���� ���������. 
		/// ���� ��� �������� �� MainWindow
		/// </summary>
		protected virtual Window ActiveWindow
		{
			get { return _theActiveWindow ?? (_theActiveWindow = MainWindow); }
			set { _theActiveWindow = value; }
		}

		#endregion

		#region Public methods

		/// <summary>
		/// �������� ������.
		/// </summary>
		/// <param name="dialogViewModel">ViewModel �������.</param>		
		/// <typeparam name="T">��� View �������.</typeparam>
		/// <returns>
		/// ������������ �������� ����� �� ��� � System.Windows.Window.ShowDialog().
		/// </returns>
		public bool? ShowDialog<T>(object dialogViewModel) where T : Window, new()
		{
			return ShowDialogInternal<T>(dialogViewModel, ActiveWindow);
		}

		/// <summary>
		/// �������� ������.
		/// </summary>
		/// <param name="dialogViewModel">ViewModel �������.</param>
		/// <param name="owner">�������� ������������ ����</param>
		/// <typeparam name="T">��� View �������.</typeparam>
		/// <returns>
		/// ������������ �������� ����� �� ��� � System.Windows.Window.ShowDialog().
		/// </returns>
		public bool? ShowDialog<T>(object dialogViewModel, IWindow owner) where T : Window, new()
		{
			ThrowArgumentExceptionInNotWindow(owner);

			return ShowDialogInternal<T>(dialogViewModel, (Window) owner);
		}

		/// <summary>
		/// �������� ���� � ����������� ������, owner � ���� ����������� ������� �������� ����
		/// </summary>
		/// <param name="viewModel">ViewModel ����.</param>
		/// <returns>��������� ���� ��� ���������� ���������</returns>
		public IWindow Show<T>(object viewModel)
			where T : Window, IWindow, new()
		{
			return ShowInternal<T>(viewModel, ActiveWindow);
		}
		
		/// <summary>
		/// �������� ���� � ����������� ������
		/// </summary>
		/// <param name="viewModel">ViewModel ����.</param>
		/// <param name="owner">�������� ������������ ����</param>
		/// <returns>��������� ���� ��� ���������� ���������</returns>
		public IWindow Show<T>(object viewModel, IWindow owner)
			where T : Window, IWindow, new()
		{
			ThrowArgumentExceptionInNotWindow(owner);

			return ShowInternal<T>(viewModel, (Window) owner);
		}

	    /// <summary>
	    /// �������� MessageBox. � ���������� Caption �� �������� ��������� ����
	    /// </summary>
	    /// <param name="message">����� ��� �����������.</param>
	    /// <param name="button">
	    /// ��������, ������������ ����� ������ ������ ��������� MessageBox.
	    /// </param>
	    /// <param name="icon">������ ��� �����������.</param>
	    /// <returns>
	    /// MessageBoxResult ���������� ����� ������ ����� ������������.
	    /// </returns>
	    public MessageBoxResult ShowMessageDialog(
			string message, 
			MessageBoxButton button = MessageBoxButton.OK,
			MessageBoxImage icon = MessageBoxImage.Information)
		{
			return ShowMessageDialogInternal(ActiveWindow, message, null, button, icon,
				MessageBoxResult.OK, MessageBoxOptions.None);
		}

	    /// <summary>
	    /// �������� MessageBox.
	    /// </summary>
	    /// <param name="message">����� ��� �����������.</param>
	    /// <param name="caption">���������. ���� ������ �� ������ ��������� ��������� ����</param>
	    /// <param name="button">
	    /// ��������, ������������ ����� ������ ������ ��������� MessageBox.
	    /// </param>
	    /// <param name="icon">������ ��� �����������.</param>
	    /// <returns>
	    /// MessageBoxResult ���������� ����� ������ ����� ������������.
	    /// </returns>
	    public MessageBoxResult ShowMessageDialog(
			string message, 
			string caption,
			MessageBoxButton button = MessageBoxButton.OK, 
			MessageBoxImage icon = MessageBoxImage.Information)
		{
			return ShowMessageDialogInternal(ActiveWindow, message, caption, button, icon, 
				MessageBoxResult.OK, MessageBoxOptions.None);
		}

		/// <summary>
	    /// �������� MessageBox.
	    /// </summary>
	    /// <param name="message">����� ��� �����������.</param>
	    /// <param name="caption">���������. ���� ������ �� ������ ��������� ��������� ����</param>
	    /// <param name="button">
	    /// ��������, ������������ ����� ������ ������ ��������� MessageBox.
	    /// </param>
	    /// <param name="icon">������ ��� �����������.</param>
	    /// <param name="defaultButton">�������� ������������ ����� ������ ����� ������� �� ���������</param>
	    /// <param name="options">Specifies special display options for a message box.</param>
	    /// <returns>
	    /// MessageBoxResult ���������� ����� ������ ����� ������������.
	    /// </returns>
	    public MessageBoxResult ShowMessageDialog(
	        string message,
	        string caption,
	        MessageBoxButton button,
	        MessageBoxImage icon,
	        MessageBoxResult defaultButton,
	        MessageBoxOptions options)
	    {
			return ShowMessageDialogInternal(ActiveWindow, message, caption, button, icon, 
				defaultButton, options);
	    }

		/// <summary>
		/// �������� MessageBox.
		/// </summary>
		/// <param name="owner">�������� ����, ��������� �������� null</param>
		/// <param name="message">����� ��� �����������.</param>
		/// <param name="caption">���������. ���� ������ �� ������ ��������� ��������� ����</param>
		/// <param name="button">
		/// ��������, ������������ ����� ������ ������ ��������� MessageBox.
		/// </param>
		/// <param name="icon">������ ��� �����������.</param>
		/// <param name="defaultButton">�������� ������������ ����� ������ ����� ������� �� ���������</param>
		/// <param name="options">Specifies special display options for a message box.</param>
		/// <returns>
		/// MessageBoxResult ���������� ����� ������ ����� ������������.
		/// </returns>
		public MessageBoxResult ShowMessageDialog(
			IWindow owner, 
			string message, 
			string caption,
			MessageBoxButton button = MessageBoxButton.OK, 
			MessageBoxImage icon = MessageBoxImage.Information, 
            MessageBoxResult defaultButton = MessageBoxResult.OK,
			MessageBoxOptions options = MessageBoxOptions.None)
		{
			ThrowArgumentExceptionInNotWindow(owner);
            return ShowMessageDialogInternal((Window)owner, message, caption, button, icon, 
				defaultButton, options);
		}

		/// <summary>
		/// �������� ������ � ���������� �� ������.
		/// </summary>
		public void ShowErrorMessageDialog(string message, string details)
		{
			ShowErrorMessageDialogInternal(ActiveWindow, message, details, null);
		}

		public void ShowErrorMessageDialog(IWindow owner, string message, string details, string caption)
		{
			ThrowArgumentExceptionInNotWindow(owner);
			ShowErrorMessageDialogInternal((Window)owner, message, details, caption);
		}

		/// <summary>
		/// �������� ������ �������� �����
		/// </summary>
		/// <param name="filter">������ ��� ���������� ������ �� �����</param>
		/// <param name="fileName">��� �����</param>
		/// <param name="owner">�������� ������������ ����</param>
		/// <returns></returns>
		public virtual bool? ShowOpenFileDialog(string filter, out string fileName, IWindow owner = null)
		{
			ThrowArgumentExceptionInNotWindow(owner);
			
			fileName = null;
			var ownerWindow = (Window)owner ?? ActiveWindow;

			var dlg = new OpenFileDialog
			{
				Filter = filter
			};
			var res = dlg.ShowDialog(ownerWindow);
			if (true == res)
			{
				fileName = dlg.FileName;
			}
			return res;
		}

		/// <summary>
		/// �������� ������ ���������� �����
		/// </summary>
		/// <param name="filter">������ ��� ���������� ������ �� �����</param>
		/// <param name="defFileName">�������������� ��� �����</param>
		/// <param name="fileName">��� ����� ����� �������� �������</param>
		/// <param name="owner">�������� ������������ ����</param>
		/// <returns></returns>
		public virtual bool? ShowSaveFileDialog(string filter, string defFileName, out string fileName, IWindow owner = null) 
		{
			ThrowArgumentExceptionInNotWindow(owner);

			fileName = null;
			var ownerWindow = (Window)owner ?? ActiveWindow;

			var dlg = new SaveFileDialog
			{
				Filter = filter, 
				FileName = defFileName
			};

			var res = dlg.ShowDialog(ownerWindow);
			if (true == res)
			{
				fileName = dlg.FileName;
			}
			return res;
		}

		#endregion

		#region Protected methods

		protected virtual bool? ShowDialogInternal<T>(object dialogViewModel, Window owner) where T : Window, new()
		{
			if (!_dispatcher.CheckAccess())
			{
				return (bool?)_dispatcher.Invoke(new Func<bool?>(() => ShowDialogInternal<T>(dialogViewModel, owner)));
			}

			bool? res;

			var prevActiveWindow = ActiveWindow;

			try
			{
				var dialog = new T
				{
					Owner = (owner != null && owner.IsLoaded) ? owner : null,
					DataContext = dialogViewModel
				};

				ActiveWindow = dialog;
				res = dialog.ShowDialog();
			}
			finally
			{
				ActiveWindow = prevActiveWindow;
			}
			return res;
		}

		protected virtual IWindow ShowInternal<T>(object viewModel, Window owner)
			where T : Window, IWindow, new()
		{
			if (!_dispatcher.CheckAccess())
			{
				return (IWindow)_dispatcher.Invoke(new Func<IWindow>(() => ShowInternal<T>(viewModel, owner)));
			}

			var window = new T
			{
				Owner = (owner != null && owner.IsLoaded) ? owner : null,
				DataContext = viewModel
			};
			window.Show();

			return window;
		}

		protected void ShowErrorMessageDialogInternal(Window owner, string message, string details, string caption)
		{
			if (!_dispatcher.CheckAccess())
			{
				_dispatcher.Invoke(() => ShowErrorMessageDialogInternal(owner, message, details, caption));
				return;
			}

			var messageCaption = GetMessageDialogCaption(owner, caption);
			ShowDialogInternal<ErrorMessageWindow>(new ErrorMessageViewModel(messageCaption, message, details), owner);
		}

		/// <summary>
		/// ���������� ����������� Message �������
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="messageBoxText"></param>
		/// <param name="caption">����� ���� ������</param>
		/// <param name="button"></param>
		/// <param name="icon"></param>
        /// <param name="options"></param>
        /// <param name="defaultButton"></param>
		/// <returns>MessageBoxResult</returns>
		protected virtual MessageBoxResult ShowMessageDialogInternal(
			Window owner, 
			string messageBoxText, 
			string caption,
			MessageBoxButton button, 
			MessageBoxImage icon, 
			MessageBoxResult defaultButton,
			MessageBoxOptions options)
		{		
			if (icon == MessageBoxImage.Error && button == MessageBoxButton.OK)
			{
				ShowErrorMessageDialogInternal(owner, messageBoxText, null, caption);
				return MessageBoxResult.OK;
			}

			if (!_dispatcher.CheckAccess())
			{
				return (MessageBoxResult)_dispatcher.Invoke(
					new Func<MessageBoxResult>(() => ShowMessageDialogInternal(owner, messageBoxText, caption, button, icon, defaultButton, options)));
			}

			var messageBoxCaption = GetMessageDialogCaption(owner, caption);

			return owner != null
				? MessageBox.Show(owner, messageBoxText, messageBoxCaption, button, icon, defaultButton, options)
				: MessageBox.Show(messageBoxText, messageBoxCaption, button, icon, defaultButton);
		}

		/// <summary>
		/// ���������� ��������� Caption ��� Message �������.
		/// ���� �������� ������, �� ���������� Title �������� ��������� ����
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="caption"></param>
		/// <returns></returns>
		protected virtual string GetMessageDialogCaption(Window owner, string caption)
		{
			if (!string.IsNullOrWhiteSpace(caption))
				return caption;

			if (owner != null)
				return owner.Title;

			try
			{
				return ActiveWindow == null || string.IsNullOrWhiteSpace(ActiveWindow.Title)
							? DefaultWindowTitle
							: ActiveWindow.Title;
			}
			catch
			{
				return DefaultWindowTitle;
			}
		}

		#endregion

		private static void ThrowArgumentExceptionInNotWindow(IWindow owner)
		{
			if (owner != null && !(owner is Window))
				throw new ArgumentException("owner ������ ���� ����������� ������ Window", "owner");
		}
	}
}
