using System.Collections.Generic;
using System.Windows.Media;
using StkCommon.UI.Wpf.Model;

namespace StkCommon.UI.Wpf.ViewModels
{
	/// <summary>
	/// ��������� ��������� ���� ����������� ������������
	/// </summary>
	public interface IAuthorizationViewModel
	{
		/// <summary>
		/// ������� ����, �� ��������� 1.0
		/// </summary>
		double UiScale { get; set; }

		/// <summary>
		/// ����� ������������
		/// </summary>
		string UserName { get; set; }

		/// <summary>
		/// ������ ������������
		/// </summary>
		string Password { get; set; }

		/// <summary>
		/// ��������� ������
		/// </summary>
		string Server { get; set; }

		/// <summary>
		/// ��������� ���� ������
		/// </summary>
		string Database { get; set; }
		
		/// <summary>
		/// ������ ����������
		/// </summary>
		ImageSource ApplicationIcon { get; set; }

		/// <summary>
		/// �������� ����������
		/// </summary>
		string ApplicationName { get; set; }

		/// <summary>
		/// ������� ���������� ��������� ���� � ������ �������� ����������� 
		/// </summary>
		bool? DialogResult { get; set; }

		/// <summary>
		/// ����� ������ ���� �����������
		/// </summary>
		AuthorizationMode Mode { get; }

		/// <summary>
		/// ������ ��������
		/// </summary>
		IEnumerable<object> Servers { get; set; }

		/// <summary>
		/// ������ ��� ������
		/// </summary>
		IEnumerable<object> Databases { get; set; } 

	}
}