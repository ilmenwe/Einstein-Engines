using Content.Shared.Maps;
using Content.Shared.Physics;
using JetBrains.Annotations;
using Robust.Shared.Map;

namespace Content.Shared.Construction.Conditions;

[UsedImplicitly]
[DataDefinition]
public sealed partial class TileNotBlocked : IConstructionCondition
{
    [DataField("filterMobs")] private bool _filterMobs = false;
    [DataField("failIfSpace")] private bool _failIfSpace = true;
    [DataField("failIfNotSturdy")] private bool _failIfNotSturdy = true;

    public bool Condition(EntityUid user, EntityLookupSystem entityLookup, EntityCoordinates location, Direction direction, TurfSystem? turfSystem = null)
    {
        var tileRef = location.GetTileRef();

        if (tileRef == null)
        {
            return false;
        }

        if (tileRef.Value.IsSpace() && _failIfSpace)
        {
            return false;
        }

        if (!tileRef.Value.GetContentTileDefinition().Sturdy && _failIfNotSturdy)
        {
            return false;
        }
        CollisionGroup mask = _filterMobs
            ? CollisionGroup.MobMask
            : CollisionGroup.Impassable;
        return !turfSystem!.IsTileBlocked(tileRef.Value, mask);
    }

    public ConstructionGuideEntry GenerateGuideEntry()
    {
        return new ConstructionGuideEntry
        {
            Localization = "construction-step-condition-tile-not-blocked",
        };
    }
}
