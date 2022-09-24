using System;
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
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vaultKeepsService;

    public VaultKeepsController(VaultKeepsService vaultKeepsService)
    {
      _vaultKeepsService = vaultKeepsService;
    }
// I never need to get vaultkeeps directly, i just use them to join vaults to keeps, so my only cruds are create and delete
    [HttpPost]
    [Authorize]
    // the body needs to include the vaultId and the keepId
    public async Task<ActionResult<VaultKeep>> AddKeepToVault([FromBody] VaultKeep vaultKeepData){
        try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        vaultKeepData.CreatorId = user.Id;
        VaultKeep vaultKeep = _vaultKeepsService.AddKeepToVault(vaultKeepData);
        return Ok(vaultKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> RemoveKeepFromVault(int id){
        try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        string message = _vaultKeepsService.RemoveKeepFromVault(id, user);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}