using CourceProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourceProject.Data.Repository {
    public interface IRepository {
        Fanfic GetFanfic(int id);
        List<Fanfic> GetAllFanfics(int id);
        void AddFanfic(Fanfic fanfic);
        void RemoveFanfic(int id);
        void UpdateFanfic(Fanfic fanfic);
        Task<bool> SaveChangesAsync();
    }
}
