using BackspaceGaming.Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackspaceGaming.Repository
{
    public class AuthenticationRepository:RepositoryBase<Authentication>, IAuthenticationRepository
    {
        private readonly DbContext _context;
        public AuthenticationRepository(DbContext context):base(context)
        {
            this._context = context;
        }
    }
}
