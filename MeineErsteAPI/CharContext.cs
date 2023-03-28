using MeineErsteAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MeineErsteAPI
{
    public class CharContext : DbContext
    {
        public DbSet<Charakter> Charaktere { get; set; }

        public CharContext(DbContextOptions<CharContext> options) : base(options)
        {

        }

    }
}
