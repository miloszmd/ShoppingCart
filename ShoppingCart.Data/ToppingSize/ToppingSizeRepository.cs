﻿using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Topping;

namespace ShoppingCart.Data.ToppingSize
{
    public class ToppingSizeRepository : IToppingSizeRepository
    {
        private readonly IDatabase _database;

        public ToppingSizeRepository() : this(new NhibernateDatabase()) { }

        public ToppingSizeRepository(IDatabase database)
        {
            _database = database;
        }

        public GetToppingSizeResponse GetByIds(List<int> extraToppingIds, int sizeId)
        {
            var response = new GetToppingSizeResponse();

            try
            {
                response.ToppingSize = _database.Query<ToppingSizeRecord>().Where(x => extraToppingIds.Contains(x.Topping.Id) && x.Size.Id == sizeId).ToList();
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    Message = "Something went wrong when retrieving ToppingRecords from database."
                });
            }

            return response;
        }
    }
}