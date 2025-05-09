using Robust.Server.GameObjects;
using Robust.Shared.EntitySerialization.Systems;
using Robust.Shared.Map;
using Robust.Shared.Map.Components;
using Robust.Shared.Network;
using Robust.Shared.Player;
using Robust.Shared.Utility;

namespace Content.Server.Administration.Systems;

/// <summary>
/// This handles the administrative test arena maps, and loading them.
/// </summary>
public sealed class AdminTestArenaSystem : EntitySystem
{
    [Dependency] private readonly IMapManager _mapManager = default!;
    [Dependency] private readonly MapLoaderSystem _map = default!;
    [Dependency] private readonly MetaDataSystem _metaDataSystem = default!;

    public const string ArenaMapPath = "/Maps/Test/admin_test_arena.yml";

    public Dictionary<NetUserId, EntityUid> ArenaMap { get; private set; } = new();
    public Dictionary<NetUserId, EntityUid?> ArenaGrid { get; private set; } = new();

    public (EntityUid Map, EntityUid? Grid) AssertArenaLoaded(ICommonSession admin)
    {
        if (ArenaMap.TryGetValue(admin.UserId, out var arenaMap) && !Deleted(arenaMap) && !Terminating(arenaMap))
        {
            if (ArenaGrid.TryGetValue(admin.UserId, out var arenaGrid) && !Deleted(arenaGrid) && !Terminating(arenaGrid.Value))
            {
                return (arenaMap, arenaGrid);
            }
            else
            {
                ArenaGrid[admin.UserId] = null;
                return (arenaMap, null);
            }
        }

        ResPath arenaResPath = new ResPath(ArenaMapPath);
        ArenaMap[admin.UserId] = _mapManager.GetMapEntityId(_mapManager.CreateMap());
        _metaDataSystem.SetEntityName(ArenaMap[admin.UserId], $"ATAM-{admin.Name}");
        MapId mapId = Comp<MapComponent>(ArenaMap[admin.UserId]).MapId;
        if (_map.TryLoadMapWithId(mapId, arenaResPath, out var map,out var grids3))
        {
            if(admin.AttachedEntity != null)
            {
                _metaDataSystem.SetEntityName(admin.AttachedEntity.Value, $"ATAG-{admin.Name}");
                ArenaGrid[admin.UserId] = admin.AttachedEntity.Value;
            }
        }
        else
        {
            ArenaGrid[admin.UserId] = null;
        }

        return (ArenaMap[admin.UserId], ArenaGrid[admin.UserId]);
    }
}
