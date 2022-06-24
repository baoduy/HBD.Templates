using HBD.Services.StateManagement;
using MediatR.AppServices.ProcessManagers.States;

namespace MediatR.AppServices.ProcessManagers;

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