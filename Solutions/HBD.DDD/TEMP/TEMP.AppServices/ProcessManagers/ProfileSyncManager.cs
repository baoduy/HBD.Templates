using System;
using System.Threading.Tasks;
using HBD.Services.StateManagement;
using TEMP.AppServices.ProcessManagers.States;

namespace TEMP.AppServices.ProcessManagers;

internal sealed class ProfileSyncManager : IProfileSyncManager
{
    private readonly IStateManager<ProfileSyncManager> _stateManager;

    public ProfileSyncManager(IStateManager<ProfileSyncManager> stateManager) => _stateManager = stateManager;

    public async Task RunAsync()
    {
        var state = _stateManager.Get<ProfileState>();
        var value = await state.GetValueAsync();
        value.LastProcessed = DateTime.Now;

        await state.CommitAsync();
    }
}