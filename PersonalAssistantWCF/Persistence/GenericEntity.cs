using NHibernate.Criterion;
using System;

namespace CastleActiveRecord.Persistence
{
    [Serializable]
    public class GenericEntity<T>
    {
        public static int Count(DetachedCriteria detachedCriteria)
        {
            GenericDao<T> dao = new GenericDao<T>();
            return dao.Count(detachedCriteria);
        }

        public static void Save(T instance)
        {
            GenericDao<T> dao = new GenericDao<T>();
            dao.Save(instance);
        }

        public static void Delete(T instance)
        {
            GenericDao<T> dao = new GenericDao<T>();
            dao.Delete(instance);
        }

        public static bool Exists()
        {
            GenericDao<T> dao = new GenericDao<T>();
            return dao.Exists();
        }

        public static bool Exists(DetachedCriteria detachedCriteria)
        {
            GenericDao<T> dao = new GenericDao<T>();
            return dao.Exists(detachedCriteria);
        }

        public static bool Exists(string propriedade, object valor)
        {
            DetachedCriteria dc = DetachedCriteria.For(typeof(T));
            dc.Add(Expression.Eq(propriedade, valor));
            GenericDao<T> dao = new GenericDao<T>();
            return dao.Exists(dc);
        }

        public static T Find(int id)
        {
            GenericDao<T> dao = new GenericDao<T>();
            return dao.Find(id);
        }

        public static T FindOne(DetachedCriteria dc)
        {
            GenericDao<T> dao = new GenericDao<T>();
            return dao.FindOne(dc);
        }

        public static T[] FindAll()
        {
            GenericDao<T> dao = new GenericDao<T>();
            return dao.FindAll();
        }

        public static T[] FindAll(string OrderByProperty)
        {
            GenericDao<T> dao = new GenericDao<T>();
            return dao.FindAll(OrderByProperty);
        }

        public static T[] FindAll(DetachedCriteria detachedCriteria)
        {
            GenericDao<T> dao = new GenericDao<T>();
            return dao.FindAll(detachedCriteria);
        }

        public static T[] FindAll(DetachedCriteria detachedCriteria, Order order)
        {
            detachedCriteria.AddOrder(order);

            GenericDao<T> dao = new GenericDao<T>();
            return dao.FindAll(detachedCriteria);
        }

        public static T[] FindAll(Order order)
        {
            GenericDao<T> dao = new GenericDao<T>();
            return dao.FindAll(order);
        }

        public static T[] FindAllByProperty(string property, object value)
        {
            DetachedCriteria dc = DetachedCriteria.For(typeof(T));

            if (value == null)
                dc.Add(Expression.IsNull(property));
            else
                dc.Add(Expression.Eq(property, value));

            GenericDao<T> dao = new GenericDao<T>();
            return dao.FindAll(dc);
        }

        public static T[] FindAllByProperty(string orderByProperty, string property, object value)
        {
            DetachedCriteria dc = DetachedCriteria.For(typeof(T));

            if (value == null)
                dc.Add(Expression.IsNull(property));
            else
                dc.Add(Expression.Eq(property, value));

            dc.AddOrder(Order.Asc(orderByProperty));

            GenericDao<T> dao = new GenericDao<T>();
            return dao.FindAll(dc);
        }

        public static T[] FindAllByPropertyInsensitiveLike(string orderByProperty, string property, string value)
        {
            GenericDao<T> dao = new GenericDao<T>();
            return dao.FindAllByPropertyInsensitiveLike(orderByProperty, property, value);
        }
    }
}