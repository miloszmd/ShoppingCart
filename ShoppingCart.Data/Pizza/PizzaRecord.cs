﻿using System.Diagnostics.CodeAnalysis;
using FluentNHibernate.Mapping;

namespace ShoppingCart.Data.Pizza
{
    public class PizzaRecord
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class PizzaRecordMap : ClassMap<PizzaRecord>
    {
        public PizzaRecordMap()
        {
            Table("pizzas");
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}