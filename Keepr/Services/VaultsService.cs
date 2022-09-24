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
      return _vaultsRepo.GetAllProfileVaults(id);
    }

    internal Vault GetVaultById(int id1, string id2)
    {
      throw new NotImplementedException();
    }
  }
}