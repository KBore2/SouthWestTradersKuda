﻿using AutoMapper;
using Core.Authorization;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Controllers
{

  /// <summary>
  /// Provides operations to manage order states
  /// </summary>
  [ApiController]
  [SwaggerTag("Provides operations to manage order states")]
  [Produces("application/json")]
  [Route("[controller]")]
  [Authorize]
  public class OrderStatesController: ControllerBase
  {
    private readonly IOrderStatesRepository _orderStatesRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialises the constructor
    /// </summary>
    /// <param name="orderStatesRepository"></param>
    /// <param name="mapper"></param>
    public OrderStatesController(IOrderStatesRepository orderStatesRepository, IMapper mapper)
    {
      _orderStatesRepository = orderStatesRepository;
      _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a set of order states
    /// </summary>
    /// <returns></returns> 
    [HttpGet]
    public async Task<IEnumerable<OrderState>> Get()
    {
      var orderStates = await _orderStatesRepository.GetCachedOrderStates();
      return _mapper.Map<IEnumerable<OrderState>>(orderStates);
    }

    /// <summary>
    /// Retrieves the specified order state
    /// </summary>
    /// <returns></returns> 
    [HttpGet("{orderStateId}")]
    public async Task<OrderState> Get(int orderStateId)
    {
      var orderStates = await _orderStatesRepository.GetCachedOrderStatesByKey(orderStateId);
      return _mapper.Map<OrderState>(orderStates);
    }   
  }
}
