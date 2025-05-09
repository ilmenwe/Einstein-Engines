using System.Linq;
using Content.Server.Administration;
using Content.Shared.Administration;
using Robust.Server.GameObjects;

using Robust.Shared.Console;
using Robust.Shared.ContentPack;
using Robust.Shared.EntitySerialization.Components;
using Robust.Shared.EntitySerialization.Systems;
using Robust.Shared.Map;
using Robust.Shared.Utility;

namespace Content.Server.Maps;

/// <summary>
/// Loads every map and resaves it into the data folder.
/// </summary>
[AdminCommand(AdminFlags.Mapping)]
public sealed class ResaveCommand : LocalizedCommands
{
    [Dependency] private readonly IEntityManager _entManager = default!;
    [Dependency] private readonly IMapManager _mapManager = default!;
    [Dependency] private readonly MapSystem _mapSystem = default!;
    [Dependency] private readonly IResourceManager _res = default!;

    public override string Command => "resave";

    public override void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        var loader = _entManager.System<MapLoaderSystem>();

        foreach (var fn in _res.ContentFindFiles(new ResPath("/Maps/")))
        {
            var mapEntity = _mapSystem.CreateUninitializedMap();

            
            loader.TryLoadMapWithId(mapEntity.Comp1.MapId, fn, out _, out _);

            // Process deferred component removals.
            _entManager.CullRemovedComponents();

            var mapUid = _mapManager.GetMapEntityId(mapEntity.Comp1.MapId);
            var mapXform = _entManager.GetComponent<TransformComponent>(mapUid);

            if (_entManager.HasComponent<LoadedMapComponent>(mapUid) || mapXform.ChildCount != 1)
            {
                loader.TrySaveMap(mapEntity.Comp1.MapId, fn);
            }
            else if (mapXform.ChildEnumerator.MoveNext(out var child))
            {
                loader.TrySaveEntity(child, fn);
            }

            _mapManager.DeleteMap(mapEntity.Comp1.MapId);
        }
    }
}
