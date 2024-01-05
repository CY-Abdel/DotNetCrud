using Microsoft.EntityFrameworkCore;
using TicketsCRUD.Core.Entities;

namespace TicketsCRUD.Core.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
