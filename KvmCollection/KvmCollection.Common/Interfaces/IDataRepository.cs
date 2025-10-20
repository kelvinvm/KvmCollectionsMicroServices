using KvmCollection.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvmCollection.Common.Interfaces;

public interface IDataRepository<TObj> 
{
    Task<IEnumerable<TObj>> GetAllAsync();
    Task<TObj> CreateAsync(TObj entity);
    Task<TObj> ReadAsync(int key);
    Task<TObj> UpdateAsync(TObj entity);
    Task DeleteAsync(TObj entity);
}
