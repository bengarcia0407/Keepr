using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;

    public AccountController(AccountService accountService, KeepsService keepsService, VaultsService vaultsService)
    {
      _accountService = accountService;
      _keepsService = keepsService;
      _vaultsService = vaultsService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("keeps")]
    [Authorize]
    public async Task<ActionResult<List<Keep>>> GetAccountKeeps()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<Keep> keeps = _keepsService.GetAllProfileKeeps(userInfo.Id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // STUB get all keeps created by me
    // https://localhost:5001/account/keeps
    // [HttpGet("/keeps")]
    // [Authorize]
    // public async Task<ActionResult<List<Keep>>> GetAllAccountKeeps()
    // {
    //   try
    //   {
    //     Account user = await HttpContext.GetUserInfoAsync<Account>();
    //     List<Keep> keeps = _keepsService.GetAllProfileKeeps(user.Id);
    //     return Ok(keeps);
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }

    // STUB get all Vaults created by me
    [HttpGet("vaults")]
    [Authorize]
    public async Task<ActionResult<List<Vault>>> GetAllAccountVaults()
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        // yes, I did pass my own id twice... this made more sense than writing a new method
        List<Vault> vaults = _vaultsService.GetAllProfileVaults(user.Id, user.Id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }

}