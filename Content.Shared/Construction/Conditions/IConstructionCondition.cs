using Content.Shared.Maps;
using Robust.Shared.Map;

namespace Content.Shared.Construction.Conditions
{
    public interface IConstructionCondition
    {
        ConstructionGuideEntry? GenerateGuideEntry();
        bool Condition(EntityUid user, EntityLookupSystem lookupSystem, EntityCoordinates location, Direction direction, TurfSystem? turfSystem = null);
    }
}
