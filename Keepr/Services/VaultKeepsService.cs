using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vaultKeepsRepo;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo)
    {
      _vaultKeepsRepo = vaultKeepsRepo;
    }

    internal List<VaultKeep> GetAll()
    {
      return _vaultKeepsRepo.GetAll();
    }
    internal VaultKeep GetById(int id, Account user)
    {
      VaultKeep vaultKeep = _vaultKeepsRepo.GetById(id);
      if (vaultKeep.CreatorId == null){
        throw new Exception("unable to find VaultKeep with that Id");
      }
      if (vaultKeep.CreatorId != user.Id){
        throw new Exception("unauthorized");
      }
      return vaultKeep;
    }
    internal VaultKeep AddKeepToVault(VaultKeep vaultKeepData, Vault vault)
    {
        // this will increase keptcount for all users, how do I prevent this from happening when the creator adds it to their own vault?
      if (vaultKeepData.CreatorId != vault.CreatorId){
        throw new Exception("you cannot add keeps to vaults you do not own.");
      }
      return _vaultKeepsRepo.AddKeepToVault(vaultKeepData);
    }

    internal string RemoveKeepFromVault(int id, Account user)
    {
      VaultKeep vaultKeep = GetById(id, user);
      _vaultKeepsRepo.RemoveKeepFromVault(id);
      return "keep removed from vault";
    }

  }
}