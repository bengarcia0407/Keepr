using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;

    public ProfilesController(AccountService accountService, KeepsService keepsService, VaultsService vaultsService)
    {
      _accountService = accountService;
      _keepsService = keepsService;
      _vaultsService = vaultsService;
    }
    [HttpGet("{id}")]
    public ActionResult<Profile> GetById(string id){
        try
            {
                Profile profile = _accountService.GetProfile(id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
    }
                // STUB get all keeps created by a profile id
    [HttpGet("{userId}/keeps")]
    public ActionResult<List<Keep>> GetAllProfileKeeps(string userId)
    {
      try
      {
        List<Keep> keeps = _keepsService.GetAllProfileKeeps(userId);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

        // STUB get all Vaults created by a profile id
    [HttpGet("{userId}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetAllProfileVaults(string userId)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        List<Vault> vaults = _vaultsService.GetAllProfileVaults(userId, user.Id);
        vaults = vaults.FindAll(v => v.IsPrivate == false || v.CreatorId == user.Id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}