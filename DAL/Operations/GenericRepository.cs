using DAL.Abstract;
using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Operations
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        DataContext context = new DataContext();
        DbSet<T> _object;

        public GenericRepository()
        {
                _object = context.Set<T>();
        }
        public T Create(T _p)
        {
            try
            {
                _object.Add(_p);
                int numberOfInserted = context.SaveChanges();
                return numberOfInserted > 0 ? _p : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int _id)
        {
            try
            {
                var category = _object.FirstOrDefault(x => x.Id == _id);
                if (category != null)
                {
                    _object.Remove(category);
                    int deleted = context.SaveChanges();
                    if (deleted > 0) return true;
                    else return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public T GetItemById(int _id)
        {
            try
            {
                var category = _object.FirstOrDefault(x => x.Id == _id);
                return category;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<T> GetList()
        {
            try
            {
                var categoryList = _object.ToList();
                return categoryList;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public T Update(T _p)
        {
            try
            {
                var oldCategory = _object.FirstOrDefault(u => u.Id == _p.Id);
                if (oldCategory != null)
                {
                    UpdateObject(_p, ref oldCategory);
                    var numberOfUpdated = context.SaveChanges();
                    return numberOfUpdated > 0 ? _p : null;
                }

                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateObject(T _newEntity, ref T _oldEntity)
        {
            try
            {

                foreach (PropertyInfo CarDriverPropInfo in _newEntity.GetType().GetProperties().ToList())
                {
                    _oldEntity.GetType().GetProperty(CarDriverPropInfo.Name).SetValue(_oldEntity, _newEntity.GetType().GetProperty(CarDriverPropInfo.Name).GetValue(_newEntity));
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
