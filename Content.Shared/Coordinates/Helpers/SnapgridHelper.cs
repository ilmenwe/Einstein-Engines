using System.Numerics;
using Robust.Shared.Map;
using Robust.Shared.Map.Components;

namespace Content.Shared.Coordinates.Helpers
{
    public static class SnapgridHelper
    {
        public static EntityCoordinates SnapToGrid(this EntityCoordinates coordinates, IEntityManager? entMan = null, IMapManager? mapManager = null)
        {
            IoCManager.Resolve(ref entMan, ref mapManager);

            var transformSystem = entMan.System<SharedTransformSystem>();
            var gridId = transformSystem?.GetGrid(coordinates);

            if (gridId == null)
            {

                var mapPos = transformSystem?.ToMapCoordinates(coordinates);
                var mapX = (int)Math.Floor(mapPos!.Value.X) + 0.5f;
                var mapY = (int)Math.Floor(mapPos!.Value.Y) + 0.5f;
                mapPos = new MapCoordinates(new Vector2(mapX, mapY), mapPos!.Value.MapId);
                ;
                return new(coordinates.EntityId,transformSystem!.GetMapCoordinates(coordinates.EntityId).Position);
            }

            var grid = entMan.GetComponent<MapGridComponent>(gridId.Value);
            var tileSize = grid.TileSize;
            var localPos = coordinates.WithEntityId(gridId.Value).Position;
            var x = (int)Math.Floor(localPos.X / tileSize) + tileSize / 2f;
            var y = (int)Math.Floor(localPos.Y / tileSize) + tileSize / 2f;
            var gridPos = new EntityCoordinates(gridId.Value, new Vector2(x, y));
            return gridPos.WithEntityId(coordinates.EntityId);
        }

        public static EntityCoordinates SnapToGrid(this EntityCoordinates coordinates, MapGridComponent grid)
        {
            var tileSize = grid.TileSize;

            var localPos = coordinates.Position;

            var x = (int)Math.Floor(localPos.X / tileSize) + tileSize / 2f;
            var y = (int)Math.Floor(localPos.Y / tileSize) + tileSize / 2f;

            return new EntityCoordinates(coordinates.EntityId, x, y);
        }
    }
}
