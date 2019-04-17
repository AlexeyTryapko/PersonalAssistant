using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace CastleActiveRecord.Persistence
{
    public class GenericDao<T>
    {
        public GenericDao()
        {
        }

        public int Count(DetachedCriteria dc)
        {
            return ActiveRecordMediator.Count(typeof(T), dc);
        }

        public bool Exists()
        {
            return ActiveRecordMediator.Exists(typeof(T));
        }

        public bool Exists(DetachedCriteria dc)
        {
            return ActiveRecordMediator.Exists(typeof(T), dc);
        }

        public T Find(int id)
        {
            return (T)ActiveRecordMediator.FindByPrimaryKey(typeof(T), id);
        }

        public T FindOne(DetachedCriteria dc)
        {
            return (T)ActiveRecordMediator.FindOne(typeof(T), dc);
        }

        public T[] FindAll()
        {
            return (T[])ActiveRecordMediator.FindAll(typeof(T));
        }

        public T[] FindAll(string OrderByProperty)
        {
            DetachedCriteria dc = DetachedCriteria.For(typeof(T));
            dc.AddOrder(Order.Asc(OrderByProperty));
            return (T[])ActiveRecordMediator.FindAll(typeof(T), dc);
        }

        public T[] FindAll(DetachedCriteria dc)
        {
            return (T[])ActiveRecordMediator.FindAll(typeof(T), dc);
        }

        public T[] FindAll(Order order)
        {
            DetachedCriteria dc = DetachedCriteria.For(typeof(T));
            dc.AddOrder(order);
            return (T[])ActiveRecordMediator.FindAll(typeof(T), dc);
        }

        public T[] FindAllByProperty(string property, object value)
        {
            return (T[])ActiveRecordMediator.FindAllByProperty(typeof(T), property, value);
        }

        public T[] FindAllByProperty(string orderByProperty, string property, object value)
        {
            return (T[])ActiveRecordMediator.FindAllByProperty(typeof(T), orderByProperty, property, value);
        }

        public T[] FindAllByPropertyInsensitiveLike(string orderByProperty, string property, string value)
        {
            DetachedCriteria dc = DetachedCriteria.For(typeof(T));
            dc.AddOrder(Order.Asc(orderByProperty));
            dc.Add(Expression.InsensitiveLike(property, value, MatchMode.Anywhere));
            return (T[])ActiveRecordMediator.FindAll(typeof(T), dc); ;
        }

        public void Save(T instance)
        {
            ActiveRecordMediator.Save(instance);
        }

        public void Delete(T instance)
        {
            ActiveRecordMediator.Delete(instance);
        }
    }
}