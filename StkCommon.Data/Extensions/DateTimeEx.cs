using System;

namespace StkCommon.Data.Extensions
{
	/// <summary>
	/// ���������� ��� ������ DateTime, ����������� �������������� ��������� � ������.
	/// ��������: DateTimeEx.Now ������ DateTime.Now
	/// </summary>
	public static class DateTimeEx
	{
		[ThreadStatic]
		private static IDateTimeProvider _provider;
		
		[ThreadStatic]
		private static DateTime? _nowDateTime;

		/// <summary>
		/// ���������� ��������, ������� ����� ���������� �������� Now
		/// ������ ��� ������.
		/// </summary>
		public static void MockNow(DateTime now)
		{
			_nowDateTime = now;
		}

		/// <summary>
		/// ���������� ���������, ����� ������� ����� ������������ ������
		/// ������ ��� ������, ����� ��������� ���������.
		/// </summary>
		/// <param name="provider"></param>
		public static void Mock(IDateTimeProvider provider)
		{
			if (provider == null) throw new ArgumentNullException("provider");
			_provider = provider;
		}

		/// <summary>
		/// �������� ��������������� ��������� (������� � ������������)
		/// </summary>
		public static void Reset()
		{
			_nowDateTime = null;
			_provider = null;
		}

		/// <summary>
		/// ���������� DateTime.Now, ���� ��������� �� ��������������.
		/// </summary>
		public static DateTime Now
		{
			get
			{
				return _provider != null ? 
					_provider.Now : 
					_nowDateTime.GetValueOrDefault(DateTime.Now);
			}
		}
	}

	public interface IDateTimeProvider
	{
		DateTime Now { get; }
	}
}