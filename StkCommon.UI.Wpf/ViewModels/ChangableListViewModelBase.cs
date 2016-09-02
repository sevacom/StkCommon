using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using StkCommon.Data;

namespace StkCommon.UI.Wpf.ViewModels
{
	public interface IChangableListViewModel<in TModel>
	{
		/// <summary>
		/// ��������� ���������� ���������� ��������
		/// </summary>
		bool IsLoading { get; set; }

		/// <summary>
		/// ��������� ��������� ���������� �������
		/// </summary>
		/// <param name="models">��������� �������, ���� models == null - ���������� ������� ���������</param>
		void Fill(IEnumerable<TModel> models);

		/// <summary>
		/// ������� ������� �� ���������� ������
		/// </summary>
		void Delete(TModel model);

		/// <summary>
		/// �������� ������� �� ��������� ���������� ������, ���� ����� ������ ��� ��� � ���������, ��� ����� ���������
		/// </summary>
		void Update(TModel model);
	}

	/// <summary>
	/// ������ ���������� �������� � ���������� ����������
	/// </summary>
	public abstract class ChangableListViewModelBase<TModel, TVModel> : ViewModelBase, IChangableListViewModel<TModel> where TModel : IModelObject<TModel>
		where TVModel : ChangableViewModelBase<TModel>
	{
		private ObservableCollection<TVModel> _items;
		private ListCollectionView _itemsCollectionView;
		private TVModel _selectedItem;
		private bool _isLoading;

		protected ChangableListViewModelBase()
		{
			_items = new ObservableCollection<TVModel>();
			_itemsCollectionView = GetCollectionViewInternal(_items);
		}

		/// <summary>
		/// ������������� ������ ������� ��� ����������, ���������� � �.�.
		/// </summary>
		public virtual ListCollectionView ItemsCollectionView
		{
			get { return _itemsCollectionView; }
			private set
			{
				_itemsCollectionView = value;
                /*���� ������ ������� OnPropertyChanged ��������� 
                 * - �� ���������� � ����������� �� ��������*/
                OnPropertyChanged(() => ItemsCollectionView); 
				OnCollectionViewInitialized();
			}
		}

		/// <summary>
		/// ������ TVModel �� ������ �������� �������� ItemsCollectionView
		/// </summary>
		protected virtual ObservableCollection<TVModel> Items
		{
			get { return _items; }
			set
			{
				if (_items == value)
					return;
				_items = value;
				OnPropertyChanged(() => Items);
				ItemsCollectionView = GetCollectionView(_items);
			}
		}

		protected virtual SortDescription[] ItemsSortDescriptions
		{
			get
			{
				return null;
			}
		}

		protected virtual GroupDescription[] ItemsGroupDescriptions
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// ��������� �������
		/// </summary>
		public virtual TVModel SelectedItem
		{
			get { return _selectedItem; }
			set
			{
				if (Equals(_selectedItem, value))
				{
					return;
				}
				_selectedItem = value;
				OnPropertyChanged(() => SelectedItem);
			}
		}

		/// <summary>
		/// ��������� ���������� ���������� ��������
		/// </summary>
		public virtual bool IsLoading
		{
			get { return _isLoading; }
			set
			{
				if (value == _isLoading) 
					return;
				_isLoading = value;
				OnPropertyChanged(() => IsLoading);
			}
		}

		/// <summary>
		/// ��������� ��������� ���������� �������
		/// </summary>
		/// <param name="models">��������� �������, ���� models == null - ���������� ������� ���������</param>
		public virtual void Fill(IEnumerable<TModel> models)
		{
			models = models ?? new TModel[0];
			Items = new ObservableCollection<TVModel>(CreateViewModels(models, false));
		}

		/// <summary>
		/// ������� ������� �� ���������� ������
		/// </summary>
		public virtual void Delete(TModel model)
		{
			if (model == null) throw new ArgumentNullException("model");

			var delViewModel = Items.FirstOrDefault(t => t.Model.Equals(model));
			if (delViewModel == null)
				return;
			OnDeleteViewModel(delViewModel);
		}

		/// <summary>
		/// �������� ������� �� ��������� ���������� ������, ���� ����� ������ ��� ��� � ���������, ��� ����� ���������
		/// </summary>
		public virtual void Update(TModel model)
		{
			if (model == null) throw new ArgumentNullException("model");

			var existViewModel = Items.FirstOrDefault(t => t.Model.Equals(model));
			if (existViewModel == null)
			{
				OnAddViewModel(model);
			}
			else
			{
				OnUpdateViewModel(existViewModel, model);
			}
		}

		/// <summary>
		/// ������� ��� ������ TVModel �� Items ����� ResetChanges
		/// </summary>
		public void ResetChanges()
		{
			Items.ResetChanaged();
		}

		/// <summary>
		/// �������� TVModel �� TModel
		/// </summary>
		/// <param name="model"></param>
		/// <param name="isAdd">true - ���������� � ������������, false - �������������� ����������</param>
		/// <returns></returns>
		protected abstract TVModel CreateViewModel(TModel model, bool isAdd);
		
		protected virtual IEnumerable<TVModel> CreateViewModels(IEnumerable<TModel> models, bool isAdd)
		{
			return models.Select(p => CreateViewModel(p, isAdd));
		}

		/// <summary>
		/// ���������� ������ ��������
		/// </summary>
		protected virtual void OnAddViewModel(TModel model)
		{
			var viewModel = CreateViewModel(model, true);
			Items.Insert(0, viewModel);
		}

		/// <summary>
		/// ���������� ������������ ViewModel
		/// </summary>
		/// <param name="existViewModel"></param>
		/// <param name="model"></param>
		protected virtual void OnUpdateViewModel(TVModel existViewModel, TModel model)
		{
			existViewModel.Update(model);
			RefreshCollectionViewElement(existViewModel);
		}

		/// <summary>
		/// �������� ������������ ViewModel
		/// </summary>
		/// <param name="delViewModel"></param>
		protected virtual void OnDeleteViewModel(TVModel delViewModel)
		{
			Items.Remove(delViewModel);

			if (Equals(delViewModel, SelectedItem))
			{
				SelectedItem = null;
			}
		}

		/// <summary>
		/// ������������� ����������� �������, ���� SelectedItem �� �������� ������ �� �� ������������ � null
		/// </summary>
		/// <param name="isDeferRefresh">true - ����������� ������ DeferRefresh ���������� ���������, false - ���</param>
		protected virtual void ApplyFilterToCollectionView(bool isDeferRefresh = false)
		{
			if (SelectedItem != null && !FilterFunction(SelectedItem))
				SelectedItem = null;

			if (ItemsCollectionView != null)
			{
				ItemsCollectionView.Filter = FilterFunction;
			}
		}

		/// <summary>
		/// ������������� ���������� �� ItemsSortDescriptions
		/// </summary>
		protected virtual void ApplySortToCollectionView(bool isDeferRefresh = false)
		{
			if (ItemsCollectionView == null || ItemsSortDescriptions == null)
				return;

			ItemsCollectionView.SortDescriptions.Clear();
			foreach (var sortDescription in ItemsSortDescriptions)
			{
				ItemsCollectionView.SortDescriptions.Add(sortDescription);
			}
		}

		protected virtual void ApplyGroupToCollectionView(bool isDeferRefresh = false)
		{
			if (ItemsCollectionView == null 
				|| ItemsGroupDescriptions == null 
				|| ItemsCollectionView.GroupDescriptions == null)
				return;

			ItemsCollectionView.GroupDescriptions.Clear();
			foreach (var groupDescription in ItemsGroupDescriptions)
			{
				ItemsCollectionView.GroupDescriptions.Add(groupDescription);
			}
		}

		/// <summary>
		/// ���������� ItemsCollectionView, ������ �����, ����������� ��������� ���������� � ����������
		/// </summary>
		protected virtual void OnCollectionViewInitialized()
		{
			using (ItemsCollectionView.DeferRefresh())
			{
				ApplyFilterToCollectionView(true);
				ApplySortToCollectionView(true);
				ApplyGroupToCollectionView(true);
			}
		}

		/// <summary>
		/// ���������� TVModel
		/// </summary>
		/// <param name="viewModel"></param>
		/// <returns></returns>
		protected virtual bool FilterFunction(TVModel viewModel)
		{
			return true;
		}

		protected virtual ListCollectionView GetCollectionView(IEnumerable collection)
		{
			return GetCollectionViewInternal(collection);
		}

		protected ListCollectionView GetCollectionViewInternal(IEnumerable collection)
		{
			var listCollectionView = CollectionViewSource.GetDefaultView(collection) as ListCollectionView;

			if (listCollectionView == null)
				throw new NotSupportedException("������ �� ������������ ListCollectionView");

			return listCollectionView;
		}

		protected void RefreshCollectionViewElement<T>(T item)
		{
			if (ItemsCollectionView != null)
			{
				ItemsCollectionView.EditItem(item);
				ItemsCollectionView.CommitEdit();
			}
		}

		private bool FilterFunction(object o)
		{
			var viewModel = o as TVModel;
			if (viewModel == null)
				return false;

			return FilterFunction(viewModel);
		}

	}
}