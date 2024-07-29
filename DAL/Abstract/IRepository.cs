using DAL.Entity;
using DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T _p);
        List<T> GetList();
        T GetItemById(int _id);
        bool Delete(int _id);
        T Update(T _p);
        void UpdateObject(T _newEntity, ref T _oldEntity);
    }
}
