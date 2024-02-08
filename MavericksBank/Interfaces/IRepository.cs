using System;
namespace MavericksBank.Interfaces
{
	public interface IRepository<T,K>
	{
		public Task<T> Add(T item);
        public Task<T> Delete(K item);
        public Task<T> Update(T item);
        public Task<T> GetByID(K key);
        public Task<List<T>> GetAll();

    }
}

