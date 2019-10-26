using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions.Trackable;
using URF.Core.EF.Trackable;
using System.Linq;
using System.Threading.Tasks;
using BackspaceGaming.Repostiroy.Interface;

namespace BackspaceGaming.Repository
{


    public class RepositoryBase<TEntity> : TrackableRepository<TEntity>, IRepositoryBase<TEntity> where TEntity : class, ITrackable
    {
        private readonly DbContext _context;

        public RepositoryBase(DbContext context) : base(context)
        {
            this._context = context;
        }
        // Example: adding synchronous Find, scope: application-wide
        public TEntity Find(object[] keyValues, CancellationToken cancellationToken)
        {
           
            return this.Context.Find<TEntity>(keyValues) as TEntity;
        }
  
        public async Task<System.Collections.Generic.List<T>> CreateQuery<T>(string sql, CancellationToken cancellationToken)
        {
            List<T> items = new List<T>();
            //using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            //{
            //    SqlCommand command = connection.CreateCommand();
            //    command.CommandText = sql;
            //    connection.Open();
            //    System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties().Where(p => p.GetMethod.IsVirtual==false).ToArray(); ;//System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public
            //    using (SqlDataReader reader = command.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            var item = Activator.CreateInstance<T>();
            //            foreach (System.Reflection.PropertyInfo property in properties)
            //            {
            //                try
            //                {
            //                    if (property.PropertyType.Name == "Nullable`1")
            //                        property.SetValue(item, Convert.ChangeType(reader[property.Name], Nullable.GetUnderlyingType(property.PropertyType)), null);
            //                    else if(!property.PropertyType.BaseType.FullName.Contains("URF.Core.EF.Trackable"))
            //                        property.SetValue(item, Convert.ChangeType(reader[property.Name], property.PropertyType), null);
            //                }
            //                catch (Exception ex)
            //                {

            //                }

            //            }
            //            items.Add(item);
            //        }
            //        reader.Close();
            //    }
            //}

            return await System.Threading.Tasks.Task.FromResult(items);
        }

        public  async Task<int> SaveChangesAsync() {
           return await this.Context.SaveChangesAsync();
        }
    }
}
