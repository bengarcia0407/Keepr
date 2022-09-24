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
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _keepsService;

    public KeepsController(KeepsService keepsService)
    {
      _keepsService = keepsService;
    }
    // STUB get all keeps sorted by most recent
    [HttpGet]
    // use authsettled to log people in before page load
    public ActionResult<List<Keep>> GetAllKeeps()
    {
      try
      {
        List<Keep> keeps = _keepsService.GetAllKeeps();
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // STUB get one keep by its ID, this will be stored in the AppState, and increment views by 1
    [HttpGet("{id}")]
    public ActionResult<Keep> GetKeepById(int id)
    {
      try
      {
        Keep keep = _keepsService.GetKeepById(id);
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        keepData.CreatorId = user.Id;
        Keep keep = _keepsService.CreateKeep(keepData);
        keep.Creator = user;
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> EditKeep(int id, [FromBody] Keep keepData)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        keepData.Id = id;
        Keep keep = _keepsService.EditKeep(keepData, user);
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteKeep(int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        string message = _keepsService.DeleteKeep(id, user);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}