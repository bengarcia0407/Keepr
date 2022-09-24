using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class KeepsRepository
    {
        private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Keep> GetAllKeeps()
    {
      string sql = "SELECT k.*, a.* FROM keeps k JOIN accounts a ON a.id = k.creatorId ORDER BY k.id desc;;";
      List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => {
        keep.Creator = profile;
        return keep;
      }).ToList();
      return keeps;
    }
  }
}