using AutoMapper;
using Core.Models;
using Core.Repositories;
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
    public IEnumerable<OrderState> Get()
    {
      var orderStates = _orderStatesRepository.GetAll();
      return _mapper.Map<IEnumerable<OrderState>>(orderStates);
    }

    /// <summary>
    /// Retrieves the specified order state
    /// </summary>
    /// <returns></returns> 
    [HttpGet("{id}")]
    public OrderState Get(int id)
    {
      var orderStates = _orderStatesRepository.GetByKey(id);
      return _mapper.Map<OrderState>(orderStates);
    }   
  }
}
