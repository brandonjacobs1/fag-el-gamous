using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class CustomUserRoleStore : IUserRoleStore<IdentityUser>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public CustomUserRoleStore(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task AddToRoleAsync(IdentityUser user, string roleName, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            throw new InvalidOperationException($"Role '{roleName}' not found.");
        }

        await _userManager.AddToRoleAsync(user, role.Name);
    }

    public async Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return await _userManager.CreateAsync(user);
    }

    public async Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return await _userManager.DeleteAsync(user);
    }

    public void Dispose()
    {
        // Dispose any resources here
    }

    public async Task<IdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<IdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        return await _userManager.FindByNameAsync(normalizedUserName);
    }

    public async Task<string> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_userManager.NormalizeName(user.UserName));
    }

    public async Task<IList<string>> GetRolesAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return await Task.FromResult(user.Id);
    }

    public async Task<string> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return await Task.FromResult(user.UserName);
    }

    public async Task<IList<IdentityUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            throw new InvalidOperationException($"Role '{roleName}' not found.");
        }

        var users = await _userManager.GetUsersInRoleAsync(role.Name);
        return users.ToList();
    }

    public async Task<bool> IsInRoleAsync(IdentityUser user, string roleName, CancellationToken cancellationToken)
    {
        return await _userManager.IsInRoleAsync(user, roleName);
    }

    public async Task RemoveFromRoleAsync(IdentityUser user, string roleName, CancellationToken cancellationToken)
    {
        await _userManager.RemoveFromRoleAsync(user, roleName);
    }

    public async Task SetNormalizedUserNameAsync(IdentityUser user, string normalizedName, CancellationToken cancellationToken)
    {
        _userManager.NormalizeName(user.UserName);
        await Task.CompletedTask;
    }

    public async Task SetUserNameAsync(IdentityUser user, string userName, CancellationToken cancellationToken)
    {
        user.UserName = userName;
        await Task.CompletedTask;
    }

    public async Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return await _userManager.UpdateAsync(user);
    }
}
