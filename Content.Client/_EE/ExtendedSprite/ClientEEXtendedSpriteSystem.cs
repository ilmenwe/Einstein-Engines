using Robust.Client.Graphics;
using Robust.Client.ResourceManagement;

using Content.Shared._EE.EExtendedSprite;

namespace Content.Client._EE.EExtendedSpriteSystem.Systems;

public sealed partial class ClientEExtendedSpriteSystem : SharedEExtendedSpriteSystem
{
    [Dependency] private readonly IResourceCache _resourceCache = default!;
    [Dependency] private readonly IEyeManager _eyeManager = default!;

}
