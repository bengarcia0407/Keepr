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
      string sql = @"SELECT * FROM keeps k 
        WHERE k.creatorId = @userId 
        ORDER BY k.id desc;";
      List<Keep> keeps = _db.Query<Keep>(sql, new { userId }).ToList();
      return keeps;
    }

    internal List<Keep> GetAllKeepsByVaultId(int vaultId)
    {
      string sql = @"SELECT 
        k.*, a.*
        FROM vaultKeeps vk 
        JOIN keeps k ON vk.keepId = k.id
        JOIN accounts a ON k.creatorId = a.id
        WHERE vk.vaultId = @id;";
        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile)=>{
          keep.Creator = profile;
          return keep;
        }, new {vaultId}).ToList();
        return keeps;
    }

    internal Keep CreateKeep(Keep keepData)
    {
      string sql = @"INSERT INTO keeps
        (creatorId, name, description, img)
        VALUES
        (@creatorId, @name, @description, @img);
        SELECT LAST_INSERT_ID();";
        int id = _db.ExecuteScalar<int>(sql, keepData);
        keepData.Id = id;
        return keepData;
    }

    internal Keep EditKeep(Keep edit)
    {
      string sql = @"
      UPDATE keeps SET
      name = @name,
      description = @description,
      img = @img,
      views = @views
      WHERE id = @id;";
      _db.Execute(sql, edit);
      return edit;
    }

    internal void DeleteKeep(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id;";
      _db.Execute(sql, new {id});
    }
  }
}