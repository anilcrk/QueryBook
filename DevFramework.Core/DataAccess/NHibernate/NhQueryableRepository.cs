using DevFramework.Core.Entities;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.NHibernate
{
    public class NhQueryableRepository<T> : IQueryableRepository<T> where T : class, IEntity, new()
    {
        private NHibernateHelper _nHibernateHelper;
        private IQueryable<T> _entites;
        public NhQueryableRepository(NHibernateHelper nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }
        public IQueryable<T> Table
        {
            get
            {
                return this.Entites;
            }
        }
        public virtual IQueryable<T> Entites
        {
            get
            {
                if(_entites==null)
                {
                    _entites = _nHibernateHelper.OpenSession().Query<T>();
                    
                }
                return _entites;
            }
        }
    }
}
