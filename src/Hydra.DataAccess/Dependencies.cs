using Hydra.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.DataAccess
{
    public class Dependencies
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<HydraContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("HydraConnection")));
        }
    }
}
