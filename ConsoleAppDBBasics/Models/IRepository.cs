using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDBBasics.Models
{
    public interface IRepository<T>
    {
        T Create(T item);
        T Read(int id);
        PaginationList<T> Read(int page, int perPage, string sortByColumn, string sortByOrder);
        bool Update(T item);
        bool Delete(int id);
        int Count();
    }
}
