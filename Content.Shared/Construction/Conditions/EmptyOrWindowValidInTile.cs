using Content.Shared.Maps;
using JetBrains.Annotations;
using Robust.Shared.Map;

namespace Content.Shared.Construction.Conditions
{
    [UsedImplicitly]
    [DataDefinition]
    public sealed partial class EmptyOrWindowValidInTile : IConstructionCondition
    {
        [DataField("tileNotBlocked")]
        private TileNotBlocked _tileNotBlocked = new();

        public bool Condition(EntityUid user, EntityLookupSystem lookupSystem, EntityCoordinates location, Direction direction, TurfSystem? turfSystem = null)
        {
            var result = false;

            foreach (var entity in lookupSystem.GetEntitiesInRange(location, 1.0f, LookupFlags.Approximate | LookupFlags.Static))
            {
                if (IoCManager.Resolve<IEntityManager>().HasComponent<SharedCanBuildWindowOnTopComponent>(entity))
                    result = true;
            }

            if (!result)
                result = _tileNotBlocked.Condition(user, lookupSystem, location, direction, IoCManager.Resolve<TurfSystem>());

            return result;
        }

        public ConstructionGuideEntry GenerateGuideEntry()
        {
            return new ConstructionGuideEntry
            {
                Localization = "construction-guide-condition-empty-or-window-valid-in-tile"
            };
        }
    }
}
