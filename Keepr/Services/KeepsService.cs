using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
    {
                private readonly KeepsRepository _keepsRepo;

    public KeepsService(KeepsRepository keepsRepo)
    {
      _keepsRepo = keepsRepo;
    }

    internal List<Keep> GetAllKeeps()
    {
      return _keepsRepo.GetAllKeeps();
    }

    internal List<Keep> GetAllProfileKeeps(string userId)
    {
      throw new NotImplementedException();
    }

    internal Keep GetKeepById(int id)
    {
      throw new NotImplementedException();
    }
  }
}