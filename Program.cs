using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockedPowerFULL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SpreadsheetGear.Factory.SetSignedLicense("SpreadsheetGear.License, Type=Trial, Product=BND, Expires=2021-03-02, Company=Aleksandr Sazonov, Email=alexsaz96@mail.ru, Signature=SIFmlLNTtk+TmZmGIX+tHGLVL3MuT83bLxQmZQDA0ZkA#OGoEMBrIAYM8RXlpORSbk7m2DFZzAod2P6W+tWUep/oA#K");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
