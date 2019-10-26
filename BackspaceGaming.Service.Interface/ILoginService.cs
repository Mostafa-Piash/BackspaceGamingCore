using BackspaceGaming.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using URF.Core.Abstractions;

namespace BackspaceGaming.Service.Interface
{
    public interface ILoginService:IServiceBase<Users>
    {
        Task<IEnumerable<Users>> Get();
        Task<bool> Add(Users insertModel , IUnitOfWork unitOfWork);
    }
}
