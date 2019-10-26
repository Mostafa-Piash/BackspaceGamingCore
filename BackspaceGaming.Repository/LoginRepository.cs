using BackspaceGaming.Entity.Model;
using BackspaceGaming.Repostiroy.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BackspaceGaming.Repository
{
    public class LoginRepository : RepositoryBase<Users> ,ILoginRepository
    {
        private readonly DbContext _context;
        public LoginRepository(DbContext context):base(context)
        {
            this._context = context;
        }


 
    }
}
