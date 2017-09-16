using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalayer
{
    public interface IRepository<T>
    {
        void Add(T instance);

        void Update(T instance);

        void Delete(Guid id);

        T GetById(Guid id);

        IEnumerable<T> GetAll();
    }
}
