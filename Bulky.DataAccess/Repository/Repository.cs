using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> Set;

		public Repository(ApplicationDbContext db)
		{
			_db = db;
			Set = _db.Set<T>();
		}

		public void Add(T entity)
		{
			Set.Add(entity);
		}

		public void Delete(T entity)
		{
			Set.Remove(entity);
		}

		public void DeleteRande(IEnumerable<T> entities)
		{
			Set.RemoveRange(entities);
		}

		public T Get(Expression<Func<T, bool>> filter)
		{
			IQueryable<T> query = Set;
			query = query.Where(filter);
			return query.FirstOrDefault();
		}

		public List<T> GetAll()
		{
			IQueryable<T> query = Set;
			return query.ToList();
		}
	}
}
