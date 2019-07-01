using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadArquivoWebApi.Models;

namespace UploadArquivoWebAPI.Context
{
    public class ArquivoContext : DbContext
    {
        public DbSet<Arquivo> Arquivo { get; set; }

        public ArquivoContext(DbContextOptions<ArquivoContext> options):base(options)
        {

        }


    }
}
