using AppCore.Entities.Errors;
using InfraStructure.DataBase;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.Errors
{
    internal class ErrorLoggerRepository : IErrorLoggerRepository
    {
        //private readonly AppDbContext _context;
        //public ErrorLoggerRepository(AppDbContext context)
        //{
        //    _context = context;
        //}
        private readonly DbContextFactory _contextFactory;

        public ErrorLoggerRepository(DbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        /// <summary>
        /// ثبت خطا ها در دیتابیس
        /// </summary>
        /// <param name="errorLogger"></param>
       // public async Task SaveError(ErrorLogger errorLogger) => await _context.ErrorLogger.AddAsync(errorLogger);
        public async Task SaveError(ErrorLogger error)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.ErrorLogger.Add(error);
                await context.SaveChangesAsync();
            }
        }
    }
}
