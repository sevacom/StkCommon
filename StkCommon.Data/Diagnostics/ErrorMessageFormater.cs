using System;
using StkCommon.Data.Text;

namespace StkCommon.Data.Diagnostics
{
	public class ErrorMessageFormater
	{
		/// <summary>
		/// ��������� ��������� �� ������. message + separator + exception.Message
		/// ���� � exception ��� ��������� ��� exception = null, �� ��������� ������ message
		/// </summary>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		/// <param name="separator">null ������������� ���������� �������� ������</param>
		/// <returns></returns>
		public static string FormatErrorMessage(string message, Exception exception = null, string separator = null)
		{
			if (exception == null)
				return message;

			if (separator == null)
				separator = Environment.NewLine;

			return TextExtensions.JoinNotEmpty(separator, message, exception.Message);
		}
	}
}