using System;
using System.Windows;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using StkCommon.UI.Wpf.Exceptions;
using StkCommon.UI.Wpf.Model;
using StkCommon.UI.Wpf.Properties;
using StkCommon.UI.Wpf.ViewModels;
using StkCommon.UI.Wpf.Views;

namespace StkCommon.UI.Wpf.Test.ViewModels
{
	[TestFixture]
	public class AuthorizationViewModelBaseTest
	{
		private TestAuthorizationViewModel _target;
		private Mock<IShowDialogAgent> _agentMock;

		[SetUp]
		public void SetUpTest()
		{
			_agentMock = new Mock<IShowDialogAgent>();		
			_target = new TestAuthorizationViewModel(AuthorizationMode.LoginPassword, _agentMock.Object);
		}

		/// <summary>
		/// ������ �������� ����� Authorize ��� ������� ������, ���������� ��������������� ����� 
		/// � ������� �� ������ OK.
		/// </summary>
		[TestCase("1", "2", null, null, AuthorizationMode.LoginPassword, 1)]
		[TestCase("1", "2", "3", null, AuthorizationMode.Server, 1)]
		[TestCase("1", "2", null, "4", AuthorizationMode.Database, 1)]
		[TestCase("1", "2", "3", "4", AuthorizationMode.ServerDatabase, 1)]
		public void ShouldRiseAuthorizeWhenExecuteOkCommand(string userName, string password, string server, string dataBase, 
			AuthorizationMode mode, int expectedRiseCount )
		{
			//Given
			_target.UserName = userName;
			_target.Password = password;
			_target.Server = server;
			_target.Database = dataBase;
			_target.SetMode(mode);

			//When
			_target.OkCommand.Execute(null);

			//Then
			_target.AuthorizeRiseCount.Should().Be(expectedRiseCount, "����� Authorize �� ��������");
		}
		
		/// <summary>
		/// � ������ ����������������� ���������� ������ ��������� ������ � ���������� � ���������������� ������, ��� ����
		/// ���� ����������� �� ����������� (DialoResult = null) 
		/// </summary>
		[Test]
		public void ShouldShowDialogWhenRaiseUserExceptionInExecuteOkCommand()
		{
			//Given
			_target.UserName = "user";
			_target.Password = "123";
			var exception = new UserMessageException("����������� ������ ������.");
			_target.SetRaiseException(exception);
			
			//When
			_target.OkCommand.Execute(null);
			
			//Then
			_agentMock.Verify(a => a.ShowMessageDialog(exception.Message, It.IsAny<string>(), 
				MessageBoxButton.OK, MessageBoxImage.Error), Times.Once(), "������ �������� c ������������������ �������" );
			
			Assert.IsNull(_target.DialogResult, "DialogResult �� ����� null");
		}

		/// <summary>
		/// � ������ ��������������� ���������� ������ ��������� ������ � ���������� � �������������� ������, ��� ����
		/// ���� ����������� ����������� (DialoResult = false) 
		/// </summary>
		[Test]
		public void ShouldShowDialogWhenRaiseExceptionInExecuteOkCommand()
		{
			//Given	
			_target.UserName = "user";
			_target.Password = "123";
			var exception = new Exception("�� ����� � ��������.");
			_target.SetRaiseException(exception);
			
			//When
			_target.OkCommand.Execute(null);

			//Then
			_agentMock.Verify(a => a.ShowMessageDialog(Resources.AuthorizationWindow_FaultExceptionMessage + "\r\n" + exception.Message,
                It.IsAny<string>(), MessageBoxButton.OK, MessageBoxImage.Error), Times.Once(), "������ �������� c ������������ �������");

			_target.DialogResult.Should().BeFalse("DialogResult ������ ���� ����� false");
		}

		/// <summary>
		/// ������ ���� ������� ������ ����������� � ����������� DialogResult false, ���� ����� Authorize ������ false
		/// </summary>
		[Test]
		public void ShouldSilentCloseDialogWhenAuthorizationReturnFalse()
		{
			//Given
			_target.UserName = "user";
			_target.Password = "123";
			_target.SetMode(AuthorizationMode.LoginPassword);
			_target.ExpectedAuthorizeMethodReturnValue = false;

			//When
			_target.OkCommand.Execute(null);

			//Then
			_target.DialogResult.Should().BeFalse("DialogResult ������ ���� ����� false");
			_agentMock.Verify(p => p.ShowMessageDialog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>()), 
				Times.Never(), "������� �� ������ ������������");
		}

		/// <summary>
		/// ������ ������� ����� OnServersDropDownChanged ����� ��������� IsServersDropDownOpen
		/// </summary>
		[Test]
		public void ShouldCallOnServersDropDownChangedWhenIsServersDropDownOpenChanged()
		{
			//Given //When
			_target.IsServersDropDownOpen = true;

			//Then
			_target.OnServersDropDownChangedRiseCount.Should().Be(1, "����� OnServersDropDownChanged �� �������� ��� ��������� IsServersDropDownOpen");
		}

		/// <summary>
		/// ������ ������� ����� OnDataBasesDropDownChanged ����� ��������� IsDataBaseDropDownOpen
		/// </summary>
		[Test]
		public void ShouldCallOnDataBasesDropDownChangedWhenIsDataBaseDropDownOpenChanged()
		{
			//Given //When
			_target.IsDataBaseDropDownOpen = true;

			//Then
			_target.OnDataBasesDropDownChangedRiseCount.Should().Be(1, "����� OnDataBasesDropDownChanged �� �������� ��� ��������� IsServersDropDownOpen");
		}

		private class TestAuthorizationViewModel : AuthorizationViewModelBase
		{
			private Exception _ex�eption;

			public int AuthorizeRiseCount { get; private set; }

			public int OnServersDropDownChangedRiseCount { get; private set; }

			public int OnDataBasesDropDownChangedRiseCount { get; private set; }

			public bool ExpectedAuthorizeMethodReturnValue { get; set; }

			public TestAuthorizationViewModel(AuthorizationMode mode, IShowDialogAgent agent)
				: base(mode, agent)
			{
				ExpectedAuthorizeMethodReturnValue = true;
			}

			public void SetMode(AuthorizationMode mode)
			{
				Mode = mode;
			}

			public void SetRaiseException(Exception exception)
			{
				_ex�eption = exception;
			}

			protected override bool Authorize()
			{
				if (_ex�eption != null)
				{
					throw _ex�eption;
				}
					
				AuthorizeRiseCount++;
				return ExpectedAuthorizeMethodReturnValue;
			}

			protected override void InitializeLanguage()
			{				
			}

			protected override void OnServersDropDownChanged(bool isDropDown)
			{
				base.OnServersDropDownChanged(isDropDown);
				OnServersDropDownChangedRiseCount++;
			}

			protected override void OnDataBasesDropDownChanged(bool isDropDown)
			{
				base.OnDataBasesDropDownChanged(isDropDown);
				OnDataBasesDropDownChangedRiseCount++;
			}
		}	
	}

}
