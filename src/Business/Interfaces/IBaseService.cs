using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IBaseService<TEntity> : IDisposable where TEntity : Entity
    {
        
    }
}
