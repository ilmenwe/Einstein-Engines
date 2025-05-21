using Content.Client.Eui;
using Content.Shared.Administration;
using Content.Shared.Eui;

namespace Content.Client.Administration.UI;

public sealed class JobWhitelistsEui : BaseEui
{
    private JobWhitelistsWindow _window;

    public JobWhitelistsEui()
    {
        _window = new JobWhitelistsWindow();
        _window.OnClose += () => SendMessage(new CloseEuiMessage());
        _window.OnSetJob += (id, whitelisted) => SendMessage(new SetJobWhitelistedMessage(id, whitelisted));
    }

    public override void HandleState(EuiStateBase state)
    {
        if (state is not JobWhitelistsEuiState cast)
            return;

        _window.HandleState(cast);
    }

    public override void Opened()
    {
        base.Opened();

        _window.OpenCentered();
    }

    public override void Closed()
    {
        base.Closed();

        _window.Close();
    }
}
