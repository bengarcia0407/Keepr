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
    [Authorize]
    public async Task<ActionResult<Vault>> GetVaultById(int id) { 
        try
      {
         Account user = await HttpContext.GetUserInfoAsync<Account>();
         Vault vault = _vaultsService.GetVaultById(id, user.Id);
         return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}