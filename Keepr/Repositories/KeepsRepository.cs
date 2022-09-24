using System;
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
      string sql = "SELECT k.*, a.* FROM keeps k JOIN accounts a ON a.id = k.creatorId ORDER BY k.id desc;";
      List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => {
        keep.Creator = profile;
        return keep;
      }).ToList();
      return keeps;
    }

    internal Keep GetKeepById(int id)
    {
      string sql = @"SELECT 
        COUNT(vaultId) AS kept,
        k.*,
        a.*
        FROM keeps k 
        LEFT JOIN vaultKeeps vk ON k.id = vk.keepId
        JOIN accounts a ON k.creatorId = a.id 
        WHERE k.id = @id;";
        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => {
          keep.Creator = profile;
          return keep;
        }, new {id}).FirstOrDefault();
        return keep;
    }

    internal List<Keep> GetAllProfileKeeps(string userId)
    {
      throw new NotImplementedException();
    }

    internal List<Keep> GetAllKeepsByVaultId(int vaultId)
    {
      throw new NotImplementedException();
    }

    internal Keep CreateKeep(Keep keepData)
    {
      throw new NotImplementedException();
    }

    internal Keep EditKeep(Keep original)
    {
      throw new NotImplementedException();
    }

    internal void DeleteKeep(int id)
    {
      throw new NotImplementedException();
    }
  }
}