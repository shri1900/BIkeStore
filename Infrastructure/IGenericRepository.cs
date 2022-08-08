﻿using System.Collections.Generic;

namespace BikeStore.Infrastructure
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
    }
}
