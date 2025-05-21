using Content.Client.Heat;
using Content.Shared.Eye.Blinding.Components;
using Robust.Client.Graphics;
using Robust.Client.Player;
using Robust.Shared.Player;

namespace Content.Client.Eye.Blinding;

public sealed class HeatSystem : EntitySystem
{
    [Dependency] private readonly IOverlayManager _overlayMan = default!;
    private HeatOverlay _overlay = default!;

    public override void Initialize()
    {
        base.Initialize();


        _overlay = new();
        _overlayMan.AddOverlay(_overlay);
    }
}
