using Microsoft.Extensions.DependencyInjection;
using MySpotify.DAL.Interfaces;
using MySpotify.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpotify.BLL.Infrastructure
{
    public static class UnitOfWorkServiceExtensions
    {//регистрация интерфейса с dal IUnitOfWork  EFUnitOfWork
        public static void AddUnitOfWorkService(this IServiceCollection services) => services.AddScoped<IUnitOfWork, EFUnitOfWork>();

    }
}
