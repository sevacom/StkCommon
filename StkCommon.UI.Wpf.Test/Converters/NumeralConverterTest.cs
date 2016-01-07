using System.Globalization;
using FluentAssertions;
using NUnit.Framework;
using StkCommon.UI.Wpf.Converters;

namespace StkCommon.UI.Wpf.Test.Converters
{
	[TestFixture]
	public class NumeralConverterTest
	{
		private NumeralConverter _target;

		[SetUp]
		public void SetUpTest()
		{
			_target = new NumeralConverter();
		}

		[TestCase(1, null, "������� , ���������, ��������, ��� ��������", "�������")]
		[TestCase(10, null, "�������, ��������� , ��������, ��� ��������", "���������")]
		[TestCase(2, null, "�������, ���������, �������� , ��� ��������", "��������")]

		[TestCase(0, null, "�������, ���������, ��������", "���������")]
		[TestCase(0, null, "�������, ���������, ��������, ���_��������", "���_��������")]
		[TestCase(0, null, "�������, ���������, ��������,  ���_�������� ", "���_��������")]
		[TestCase(0, ",", "�������, ���������, ��������, ��� ��������", "��� ��������")]
		[TestCase(0, ",", "invalid string ", 0)]

		[TestCase(1, ",", "{0} ������� , ���������, ��������, ��� ��������", "1 �������")]
		[TestCase(10, ",", "�������, {0} ��������� , ��������, ��� ��������", "10 ���������")]
		[TestCase(2, ",", "�������, ���������, {0} �������� , ��� ��������", "2 ��������")]
		[TestCase(0, ",", "�������, {0} ���������, ��������", "0 ���������")]
		public void ShouldConvertToNumeralFromParameter(int number, string separator, string parameter, object expectedResult)
		{
			//Given
			if (!string.IsNullOrEmpty(separator))
				_target.Separator = separator;

			//When
			var result = _target.Convert(number, typeof (string), parameter, CultureInfo.InvariantCulture);

			//Then
			result.Should().Be(expectedResult);
		}
	}
}