using System.Threading.Tasks;

namespace TEMP.AppServices.ProcessManagers;

public interface IProfileSyncManager
{
    Task RunAsync();
}