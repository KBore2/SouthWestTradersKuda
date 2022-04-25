using AutoMapper;
using Core.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mocks
{
  internal class MapperMock
  {
    private IMapper _mapper;

    public IMapper GetMapper()
    {
      var mappingConfig = new MapperConfiguration(mc =>
      {
        mc.AddProfile(new MapperProfile());
      });

      _mapper = mappingConfig.CreateMapper();

      return _mapper;
    }
  }
}
