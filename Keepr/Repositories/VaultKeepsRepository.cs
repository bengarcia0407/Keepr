using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<VaultKeep> GetAll()
    {
      string sql = "SELECT * FROM vaultKeeps;";
      List<VaultKeep> vaultKeeps = _db.Query<VaultKeep>(sql).ToList();
      return vaultKeeps;
    }

    internal VaultKeep AddKeepToVault(VaultKeep vaultKeepData)
    {
      string sql = @"INSERT INTO vaultKeeps
      (creatorId, vaultId, keepId)
      VALUES
      (@creatorId, @vaultId, @keepId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
      vaultKeepData.Id = id;
      return vaultKeepData;
    }

    internal VaultKeep GetById(int id)
    {
      string sql = "SELECT * FROM vaultKeeps WHERE id = @id;";
      VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new {id}).FirstOrDefault();
      return vaultKeep;
    }

    internal void RemoveKeepFromVault(int id)
    {
      string sql = "DELETE FROM vaultKeeps WHERE id = @id;";
      _db.Execute(sql, new {id});
    }
  }
}