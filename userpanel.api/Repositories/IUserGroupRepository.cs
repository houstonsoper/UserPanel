using userpanel.api.Models;

namespace userpanel.api.Repositories;

public interface IUserGroupRepository
{
    Task<IEnumerable<UserGroup>> GetAllUserGroups();
}