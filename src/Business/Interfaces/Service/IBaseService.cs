using Business.Models;
using System;

namespace Business.Interfaces
{
    public interface IBaseService<TEntity> : IDisposable where TEntity : Entity
    {
        
    }
}
