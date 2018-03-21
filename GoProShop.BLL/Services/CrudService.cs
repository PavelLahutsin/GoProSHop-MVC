using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.Interfaces;
using IdProvider = GoProShop.DAL.Entities.IdProvider;
using IdProviderDto = GoProShop.BLL.DTO.IdProvider;

namespace GoProShop.BLL.Services
{
    public class CrudService<TEntity, TModel> : BaseService, ICrudService<TEntity, TModel>
        where TEntity : IdProvider
        where TModel : IdProviderDto
    {
        protected CrudService(IUnitOfWork uow) 
            : base(uow)
        {
        }

        public async Task CreateAsync(TModel model)
        {
            _uow.Repository<TEntity>().Create(Mapper.Map<TEntity>(model));
            await _uow.Commit();
        }

        public async Task<ResponseDTO> UpdateAsync(TModel model, string itemName)
        {
            var entity =   await _uow.Repository<TEntity>().UpdateAsync(Mapper.Map<TEntity>(model));

            if (entity == null)
                return new ResponseDTO(false, $"Данного {itemName}а не существует в базе данных");

            await _uow.Commit();

            return new ResponseDTO(true, $"{itemName} успешно обновлен в базе данных");
        }

        public async Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            var entities =  await _uow.Repository<TEntity>().Entities.Where(filter).ToListAsync();
            var dtos = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities);

            return dtos;
        }

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            var entities = await _uow.Repository<TEntity>().Entities.ToListAsync();
            var dtos = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities);

            return dtos;
        }

        public IEnumerable<TModel> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            var entities =  _uow.Repository<TEntity>().Entities.Where(filter).ToList();
            var dtos = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities);

            return dtos;
        }

        public IEnumerable<TModel> GetAll()
        {
            var entities = _uow.Repository<TEntity>().Entities.ToList();
            var dtos = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities);

            return dtos;
        }

        public async Task<TModel> GetAsync(int id)
        {
            var entity = await _uow.Repository<TEntity>().GetAsync(id);
            var dto = Mapper.Map<TModel>(entity);

            return dto;
        }

        public async Task<ResponseDTO> DeleteAsync(int id, string itemName)
        {
            var feedback = await _uow.Repository<TEntity>().GetAsync(id);

            if (feedback == null)
                return new ResponseDTO(false, $"Данного {itemName}а не существует в базе данных");

            await _uow.Repository<TEntity>().DeleteAsync(feedback);
            await _uow.Commit();

            return new ResponseDTO(true, $"{itemName} успешно удален из базы данных");
        }
    }
}
