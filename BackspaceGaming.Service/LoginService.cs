using BackspaceGaming.Entity.Model;
using BackspaceGaming.Repository;
using BackspaceGaming.Repostiroy.Interface;
using BackspaceGaming.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using URF.Core.Abstractions;
using URF.Core.Services;

namespace BackspaceGaming.Service
{
    public class LoginService :ServiceBase<Users>, ILoginService
    {
        private readonly ILoginRepository _repository;


        public LoginService(ILoginRepository repository, IUnitOfWork unitOfWork) :base(repository)
        {
            this._repository = repository;
        }

        public async Task<bool> Add(Users insertModel, IUnitOfWork unitOfWork)
        {
            try
            {
                _repository.Insert(insertModel);
                var res = await unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<IEnumerable<Users>> Get()
        {
            try
            {
                return await _repository.Query().SelectAsync();
            }
            catch (Exception ex)
            {
                return new List<Users>();
            }
        }
        
    }
}
