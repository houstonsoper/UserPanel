using userpanel.api.Models;
using Microsoft.AspNetCore.Mvc;
using userpanel.api.Repositories;

namespace userpanel.api.Controllers;

[ApiController]
[Route("[controller]")]

public class UserGroupController : Controller
{
    private readonly IUserGroupRepository _userGroupRepository;

    public UserGroupController(IUserGroupRepository userGroupRepository)
    {
        _userGroupRepository = userGroupRepository;
    }

    [HttpGet("/UserGroups")]
    public async Task<IActionResult> GetAllUserGroups()
    {
        var userGroups = await _userGroupRepository.GetAllUserGroups();
        return Ok(userGroups);
    }
}