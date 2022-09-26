using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;

    public VaultsController(VaultsService vaultsService, KeepsService keepsService)
    {
      _vaultsService = vaultsService;
      _keepsService = keepsService;
    }

// STUB get one vault by its id, 
    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> GetVaultById(int id) { 
        try
      {
         Account user = await HttpContext.GetUserInfoAsync<Account>();
         Vault vault = _vaultsService.GetVaultById(id, user?.Id);
         return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
// STUB get all keeps associated with the active vault
    [HttpGet("{vaultId}/keeps")]
    // [Authorize]
    public async Task<ActionResult<List<Keep>>> GetAllKeepsByVaultId(int vaultId)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        List<Keep> keeps = _keepsService.GetAllKeepsByVaultId(vaultId, user?.Id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData){
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        vaultData.CreatorId = user.Id;
        Vault vault = _vaultsService.CreateVault(vaultData);
        vault.Creator = user;
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> EditVault(int id, [FromBody] Vault vaultData){
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        vaultData.Id = id;
        Vault vault = _vaultsService.EditVault(vaultData, user);
        vault.Creator = user;
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteVault(int id){
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        string message = _vaultsService.DeleteVault(id, user);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}