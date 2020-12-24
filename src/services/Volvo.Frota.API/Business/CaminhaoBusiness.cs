using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volvo.Frota.API.Business.Interfaces;
using Volvo.Frota.API.Data.Repositories.Interfaces;
using Volvo.Frota.API.Dtos;
using Volvo.Frota.API.Dtos.Pagination;
using Volvo.Frota.API.Models;
using Volvo.Frota.API.Utils.Pagination;
using Volvo.Frota.API.Utils.Result;

namespace Volvo.Frota.API.Business
{
    public class CaminhaoBusiness : ICaminhaoBusiness
    {
        private readonly ICaminhaoRepository _caminhaoRepository;
        private readonly IMapper _mapper;
        public CaminhaoBusiness
        (
            ICaminhaoRepository caminhaoRepository,
            IMapper mapper
        )
        {
            _caminhaoRepository = caminhaoRepository;
            _mapper = mapper;
        }
        public async Task<Result> GetAllPaginatedAsync(PaginationFilterDto paginationFilter)
        {
            var pagination = await _caminhaoRepository.GetAllPaginated(_mapper.Map<PaginationFilter>(paginationFilter));
            
            return OperationResult.OK(pagination.ToResult<Caminhao, CaminhaoDto>(_mapper));
        }

        public async Task<Result> GetByIdAsync(int id)
        {
            var caminhao = await _caminhaoRepository.GetByIdAsync(id);

            if (caminhao == null)
                return OperationResult.NotFound();

            return OperationResult.OK(_mapper.Map<CaminhaoDto>(caminhao));
        }

        public async Task<Result> AddAsync(NewCaminhaoDto caminhaoDto)
        {
            if (!caminhaoDto.EhValido()) 
                return OperationResult.ValidationError(caminhaoDto.ObterValidationResult());

            var caminhao = _mapper.Map<Caminhao>(caminhaoDto);
            caminhao.AnoFabricacao = DateTime.Now.Year;

            var id = await _caminhaoRepository.CreateAsync(caminhao);

            return OperationResult.Created(id);
        }

        public async Task<Result> UpdateAsync(UpdateCaminhaoDto caminhaoDto)
        {
            if (!caminhaoDto.EhValido()) 
                return OperationResult.ValidationError(caminhaoDto.ObterValidationResult());

            var caminhao = _mapper.Map<Caminhao>(caminhaoDto);

            var propertiesUpdate = PropertyToUpdate(caminhao);

            await _caminhaoRepository.PartialUpdateAsync(caminhao, propertiesUpdate);

            return OperationResult.NoContent();
        }

        public async Task<Result> RemoveAsync(int id)
        {
            var caminhao = await _caminhaoRepository.GetByIdAsync(id);

            if (caminhao == null)
                return OperationResult.NotFound();

            await _caminhaoRepository.RemoveAsync(caminhao);

            return OperationResult.NoContent();
        }

        private string[] PropertyToUpdate(Caminhao caminhao)
        {
            var properties = caminhao.GetType().GetProperties().Select(x => x.Name);

            var propertiesUpdate = properties.Where(x => 
                !x.Equals("AnoFabricacao") && !x.Equals("Id"));

            return propertiesUpdate.ToArray();
        }
    }
}
