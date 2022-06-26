using HBD.Services.StateManagement;
using TEMP.AppServices.ProcessManagers.States;

namespace TEMP.AppServices.ProcessManagers;

internal sealed class ProfileSyncManager : IProfileSyncManager
{
    private readonly IStateService<ProfileState> _state;

    public ProfileSyncManager(IStateManager<ProfileSyncManager> stateManager) 
        => _state = stateManager.Get<ProfileState>();

    public async Task RunAsync()
    {
        var value = await _state.GetValueAsync();
        value.LastProcessed = DateTime.Now;

        await _state.CommitAsync();
    }
}