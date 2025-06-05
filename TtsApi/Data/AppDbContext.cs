using Microsoft.EntityFrameworkCore;
using TtsApi.Entities;

namespace TtsApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Voice> Voices => Set<Voice>();
}
