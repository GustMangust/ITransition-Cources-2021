using CourceProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourceProject.Data.Repository {
  public class Repository : IRepository {
    private AppDBContext ctx;
    public Repository(AppDBContext ctx) {
      this.ctx = ctx;
    }
    public void AddWork(Work work) {
      ctx.Works.Add(work);
    }
    public List<Work> GetAllWorks(int id) {
      return ctx.Works.ToList();
    }
    public Work GetWork(int id) {
      return ctx.Works.FirstOrDefault(x => x.Id == id);
    }
    public void RemoveWork(int id) {
      ctx.Remove(GetWork(id));
    }
    public void UpdateWork(Work work) {
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
