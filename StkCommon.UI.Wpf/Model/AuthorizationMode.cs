namespace StkCommon.UI.Wpf.Model
{
	/// <summary>
	/// ������ ������ ���� ����������� (��������������)
	/// </summary>
	public enum AuthorizationMode
	{
		/// <summary>
		/// ����� � ������
		/// </summary>
		LoginPassword,
		
		/// <summary>
		/// �����, ������ � ������ 
		/// </summary>
		Server,

		/// <summary>
		/// �����, ������ � ���� ������
		/// </summary>
		Database,

		/// <summary>
		/// �����, ������, ������, ���� ������
		/// </summary>
		ServerDatabase
	}
}
