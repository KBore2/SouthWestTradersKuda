using AutoMapper;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapper
{
  /// <summary>
  /// Provides operations to manage mapping profiles
  /// </summary>
  public class MapperProfile: Profile
  {

    /// <summary>
    /// Initialises the mapping profiles
    /// </summary>
    public MapperProfile()
    {
      //Entity -> Domain (DTO)
      CreateMap<Data.Products.Context.Product, Product>();
      CreateMap<Data.Products.Context.Order, Order>();
      CreateMap<Data.Products.Context.Stock, Stock>();
      CreateMap<Data.Products.Context.OrderState, OrderState>();

      //Domain (DTO) -> Entity
      CreateMap<Product, Data.Products.Context.Product>();
      CreateMap<ProductRequest, Data.Products.Context.Product>();
      CreateMap<Order, Data.Products.Context.Order>();
      CreateMap<Stock, Data.Products.Context.Stock>();
      CreateMap<OrderState, Data.Products.Context.OrderState>();
    }
  }
}
