﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        List<Product> getAll();

        List<Product> getAllByCategoryID(int ID);

        List<Product> getByUnitPrice(decimal min, decimal max);
    }
}