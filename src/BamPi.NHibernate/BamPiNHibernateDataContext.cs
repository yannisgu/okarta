using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;

namespace BamPi.NHibernate
{
    public class BamPiNHibernateDataContext : IBamPiDataContext
    {
        protected ISessionFactory sessionFactory;

        public BamPiNHibernateDataContext(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;                      
        }

        public async Task<IEnumerable<T>> Query<T>(Func<IQueryable<T>, IQueryable<T>> query) where T : class
        {
            using (var session = sessionFactory.OpenSession())
            {
                return query(session.Query<T>()).ToList();
            }
        }

        public Task<bool> Delete<T>(string id) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<T> Update<T>(string id, T entity) where T : class
        {
            using (var session = sessionFactory.OpenSession())
            {
                return session.Get<T>(id);
            }
        }

        public async Task<T> Get<T>(string id) where T : class
        {
            using (var session = sessionFactory.OpenSession())
            {
                return session.Get<T>(id);
            }
        }

        public Task<TChild> AddChild<TParent, TChild>(string parentId, Expression<Func<TParent, ICollection<TChild>>> property, TChild child) where TParent : class where TChild : class
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveChild<TParent, TChild>(string parentId, Expression<Func<TParent, ICollection<TChild>>> property, TChild child) where TParent : class where TChild : class
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TChild>> QueryChildren<TParent, TChild>(string parentId, Expression<Func<TParent, ICollection<TChild>>> property, Func<IQueryable<TChild>, IQueryable<TChild>> query) where TParent : class where TChild : class
        {
            throw new NotImplementedException();
        }
    }
}
