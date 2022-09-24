using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
    {
    private readonly KeepsRepository _keepsRepo;
    private readonly VaultsService _vaultsService;

    public KeepsService(KeepsRepository keepsRepo, VaultsService vaultsService)
    {
      _keepsRepo = keepsRepo;
      _vaultsService = vaultsService;
    }

    internal List<Keep> GetAllKeeps()
    {
      return _keepsRepo.GetAllKeeps();
    }

    internal List<Keep> GetAllProfileKeeps(string userId)
    {
      return _keepsRepo.GetAllProfileKeeps(userId);
    }

    internal List<Keep> GetAllKeepsByVaultId(int vaultId, string userId)
    {
      // nullcheck
      Vault vault = _vaultsService.GetVaultById(vaultId, userId);
      // authcheck
      if (vault.CreatorId != userId){
        throw new Exception("only the vault creator may access this vault");
      }
      return _keepsRepo.GetAllKeepsByVaultId(vaultId);
    }
    internal Keep GetKeepById(int id)
    {
      Keep keep = _keepsRepo.GetKeepById(id);
      if (keep == null){
        throw new Exception ("unable to find keep with that Id");
      }
      keep.Views++;
      _keepsRepo.EditKeep(keep);
      return keep;
    }

    internal Keep CreateKeep(Keep keepData)
    {
      return _keepsRepo.CreateKeep(keepData);
    }

    internal Keep EditKeep(Keep keepData, Account user)
    {
      Keep original = GetKeepById(keepData.Id);
      if (original.CreatorId != user.Id){
        throw new Exception("you are not authorized to edit this keep");
      }
      original.Name = keepData.Name ?? original.Name;
      original.Description = keepData.Description ?? original.Description;
      original.Img = keepData.Img ?? original.Img;
      original.Views = keepData.Views ?? original.Views;
      return _keepsRepo.EditKeep(original);
    }

    internal string DeleteKeep(int id, Account user)
    {
      Keep keep = GetKeepById(id);
      if (keep.CreatorId != user.Id){
        throw new Exception("you are not authorized to delete that keep");
      }
      _keepsRepo.DeleteKeep(id);
      return $"the keep: {keep.Name} has been permanently deleted";
    }

  }
}