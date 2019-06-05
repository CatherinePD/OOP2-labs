using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auction.DataAccess.Entities;

namespace Auction.DataAccess
{
    public interface IRepository<T>
    {
        T Get(int id);

        ICollection<T> Get();

        ICollection<T> Get(Func<T, bool> predicate);

        ICollection<T> GetSorted<TKey>(Func<T, TKey> keySelector);

        Task<int> Query(string sql, params object[] args);

        void Update(T entity);

        void Delete(T entity);

        void Add(T entity);
    }
}