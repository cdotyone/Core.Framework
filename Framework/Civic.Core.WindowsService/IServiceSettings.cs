using System.Windows.Forms;

namespace Civic.Core.WindowsService
{
    public interface IServiceSettings
    {
        Form SettingsForm { get; }
    }
}
