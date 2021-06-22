using CourceProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourceProject.Data.Repository {
  public class Repository : IRepository {
    private AppDbContext ctx;
    public Repository(AppDbContext ctx) {
      this.ctx = ctx;
    }
    public void AddFanfic(Fanfic work) {
      ctx.Fanfics.Add(work);
    }
    public List<Fanfic> GetAllFanfics(int id) {
      return ctx.Fanfics.ToList();
    }
    public Fanfic GetFanfic(int id) {
      return ctx.Fanfics.FirstOrDefault(x => x.Id == id);
    }
    public void RemoveFanfic(int id) {
      ctx.Remove(GetFanfic(id));
    }
    public void UpdateFanfic(Fanfic work) {
      ctx.Update(work);
    }
    public async Task<bool> SaveChangesAsync() {
      if(await ctx.SaveChangesAsync() > 0) {
        return true;
      }
      return false;
    }
  }
}
