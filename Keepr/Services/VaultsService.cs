using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vaultsRepo;

    public VaultsService(VaultsRepository vaultsRepo)
    {
      _vaultsRepo = vaultsRepo;
    }

    internal List<Vault> GetAllProfileVaults(string id, string userId)
    {
      List<Vault> vaults = _vaultsRepo.GetAllProfileVaults(userId);
      vaults = vaults.FindAll(v => v.IsPrivate == false || v.CreatorId == userId);
      return vaults;
    }

    internal Vault GetVaultById(int vaultId, string userId)
    {
      throw new NotImplementedException();
    }

    internal Vault CreateVault(Vault vaultData)
    {
      throw new NotImplementedException();
    }

    internal Vault EditVault(Vault vaultData, Account user)
    {
      throw new NotImplementedException();
    }

    internal string DeleteVault(int id, Account user)
    {
      throw new NotImplementedException();
    }
  }
}