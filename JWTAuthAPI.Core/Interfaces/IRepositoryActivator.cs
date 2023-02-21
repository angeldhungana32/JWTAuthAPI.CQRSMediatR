﻿namespace JWTAuthAPI.Core.Interfaces
{
    public interface IRepositoryActivator : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
    }
}
