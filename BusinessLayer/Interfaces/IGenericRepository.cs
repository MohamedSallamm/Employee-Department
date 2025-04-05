namespace BusinessLayer.Interfaces
{
	public interface IGenericRepository<T>
	{
		IEnumerable<T> GetAll();
		T GetByID(int id);
		void Add(T Item);
		void Update(T Item);
		void Delete(T Item);

	}
}
