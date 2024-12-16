using ChatMinimalApi.DbStuff.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatMinimalApi.DbStuff
{
    public class ChatDbContext : DbContext
    {
        public const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Net18Chat\";Integrated Security=True;";

        public DbSet<Message> Messages { get; set; }

        public ChatDbContext(DbContextOptions<ChatDbContext> contextOptions)
            : base(contextOptions) { }
    }
}
