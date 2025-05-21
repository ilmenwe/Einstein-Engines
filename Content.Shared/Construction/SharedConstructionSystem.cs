using System.Linq;
using Robust.Shared.Map;
using static Content.Shared.Interaction.SharedInteractionSystem;

namespace Content.Shared.Construction
{
    public abstract class SharedConstructionSystem : EntitySystem
    {
        [Dependency] private readonly IMapManager _mapManager = default!;
        [Dependency] private readonly SharedMapSystem _mapSystem = default!;
        /// <summary>
        ///     Get predicate for construction obstruction checks.
        /// </summary>
        public Ignored? GetPredicate(bool canBuildInImpassable, MapCoordinates coords)
        {
            if (!canBuildInImpassable)
                return null;

            if (!_mapManager.TryFindGridAt(coords, out var uid, out var grid))
                return null;

            var ignored = _mapSystem.GetAnchoredEntities(uid, grid, coords).ToHashSet();
            return e => ignored.Contains(e);
        }
    }
}
