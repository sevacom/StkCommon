namespace StkCommon.Data
{
	/// <summary>
	/// ������� ��������� ��� �������� � ������
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	public interface IKeyModelObject<out TKey>
	{
		TKey Key { get; }
	}
}