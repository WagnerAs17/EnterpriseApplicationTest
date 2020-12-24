using AutoMapper;
using Volvo.Frota.API.Utils.AutoMapper;

namespace Volvo.Frota.Test.Business
{
    public class BusinessTest
    {
        private IMapper _mapper;

        public IMapper Mapper
        {
            get 
            { 
                if(_mapper == null)
                {
                    _mapper = new Mapper(AutoMapperUtils.GetConfigurationMappings());
                }

                return _mapper; 
            }
        }
    }
}
