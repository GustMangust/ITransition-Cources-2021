using CourceProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourceProject.Data.Repository {
  public interface IRepository {
    Work GetWork(int id);
    List<Work> GetAllWorks(int id);
    void AddWork(Work work);
    void RemoveWork(int id);
    void UpdateWork(Work work);
    Task<bool> SaveChangesAsync();
  }
}
