using Content.Client.Atmos.EntitySystems;
using Content.Client.Resources;
using Content.Shared.Atmos.Components;
using Content.Shared.Singularity.Components;
using Content.Shared.Species;
using Microsoft.VisualBasic;
using Robust.Client.GameObjects;
using Robust.Client.Graphics;
using Robust.Client.Player;
using Robust.Client.ResourceManagement;
using Robust.Client.UserInterface.CustomControls;
using Robust.Shared.ContentPack;
using Robust.Shared.Enums;
using Robust.Shared.Map.Components;
using Robust.Shared.Map;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;
using Robust.Shared.Utility;
using System.Numerics;

namespace Content.Client.Heat;

public sealed class HeatOverlay : Overlay
{
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
    [Dependency] private readonly IResourceCache _resourceCache = default!;
    //[Dependency] private readonly IMapManager _mapManager = default!;
    [Dependency] private readonly SharedMapSystem _mapSystem = default!;

    public override OverlaySpace Space => OverlaySpace.WorldSpace;
    public override bool RequestScreenTexture => true;
    private readonly ShaderInstance _heatShader;
    private readonly Texture _heatDistortTexture;
    //public float CurrentPower = 0.0f;
    float _currentTime = 0.0f;

    public HeatOverlay()
    {
        IoCManager.InjectDependencies(this);
        _heatShader = _prototypeManager.Index<ShaderPrototype>("Heat").InstanceUnique();
        _heatDistortTexture = _resourceCache.GetTexture(new ResPath("/Textures/heat.png"));

    }

    private readonly Vector2[] _positions = new Vector2[128];
    private readonly float[] _intensities = new float[128];
    private const float MaxDistance = 20f;
    private const int MaxCount = 128;





    protected override void FrameUpdate(FrameEventArgs args)
    {
        _currentTime += args.DeltaSeconds;
    }

    protected override bool BeforeDraw(in OverlayDrawArgs args)
    {
        TestFunc(args);
        return true;
        //if (args.Viewport.Eye == null)
        //    return false;
        //if (_transformSystem is null && !_entityManager.TrySystem(out _transformSystem))
        //    return false;


        //var atmospheres = _entityManager.EntitySysManager.GetEntitySystem<AtmosphereSystem>();

        //_count = 0;
        //var query = _entityManager.EntityQueryEnumerator<GasAnalyzerComponent, TransformComponent>();
        //while (query.MoveNext(out var uid, out var gasAnalyzer, out var xform))
        //{
        //    if (xform.MapID != args.MapId)
        //        continue;

        //    var mapPos = _transformSystem.GetWorldPosition(uid);

        //    // is the distortion in range?
        //    if ((mapPos - args.WorldAABB.ClosestPoint(mapPos)).LengthSquared() > MaxDistance * MaxDistance)
        //        continue;
        //    var indices = new Vector2i(x, y);

        //    var tile = atmospheres.GetTileMixture(gridId, null, indices, true);

        //    // To be clear, this needs to use "inside-viewport" pixels.
        //    // In other words, specifically NOT IViewportControl.WorldToScreen (which uses outer coordinates).
        //    var tempCoords = args.Viewport.WorldToLocal(mapPos);
        //    tempCoords.Y = args.Viewport.Size.Y - tempCoords.Y; // Local space to fragment space.

        //    _positions[_count] = tempCoords;
        //    _intensities[_count] = gasAnalyzer.;
        //    _count++;

        //    if (_count == MaxCount)
        //        break;
        //}

        //return (_count > 0);
    }


    void TestFunc(in OverlayDrawArgs args)
    {
        //var data = _mapSystem.GetTilesIntersecting(args.MapUid,args.);


        //    _mapManager.FindGridsIntersecting(args.MapId, args.WorldAABB, null,
        //        static (EntityUid uid, MapGridComponent grid,
        //            ref (Box2Rotated WorldBounds,
        //                DrawingHandleWorld drawHandle,
        //                int gasCount,
        //                Texture[][] frames,
        //                int[] frameCounter,
        //                Texture[][] fireFrames,
        //                int[] fireFrameCounter,
        //                ShaderInstance shader,
        //                EntityQuery<GasTileOverlayComponent> overlayQuery,
        //                EntityQuery<TransformComponent> xformQuery) state) =>
        //        {
        //            if (!state.overlayQuery.TryGetComponent(uid, out var comp) ||
        //                !state.xformQuery.TryGetComponent(uid, out var gridXform))
        //            {
        //                return true;
        //            }
        //        });


        //        var pos = _transformSystem.GetWorldPositionRotationMatrixWithInv(gridXform);

        //        var (_, _, worldMatrix, invMatrix) = gridXform.GetWorldPositionRotationMatrixWithInv();
        //        state.drawHandle.SetTransform(worldMatrix);
        //        var floatBounds = invMatrix.TransformBox(state.WorldBounds).Enlarged(grid.TileSize);
        //        var localBounds = new Box2i(
        //            (int) MathF.Floor(floatBounds.Left),
        //            (int) MathF.Floor(floatBounds.Bottom),
        //            (int) MathF.Ceiling(floatBounds.Right),
        //            (int) MathF.Ceiling(floatBounds.Top));

        //        // Currently it would be faster to group drawing by gas rather than by chunk, but if the textures are
        //        // ever moved to a single atlas, that should no longer be the case. So this is just grouping draw calls
        //        // by chunk, even though its currently slower.

        //        state.drawHandle.UseShader(null);
        //        foreach (var chunk in comp.Chunks.Values)
        //        {
        //            var enumerator = new GasChunkEnumerator(chunk);

        //            while (enumerator.MoveNext(out var gas))
        //            {
        //                if (gas.Opacity == null!)
        //                    continue;

        //                var tilePosition = chunk.Origin + (enumerator.X, enumerator.Y);
        //                if (!localBounds.Contains(tilePosition))
        //                    continue;

        //                for (var i = 0; i < state.gasCount; i++)
        //                {
        //                    var opacity = gas.Opacity[i];
        //                    if (opacity > 0)
        //                        state.drawHandle.DrawTexture(state.frames[i][state.frameCounter[i]], tilePosition, Color.White.WithAlpha(opacity));
        //                }
        //            }
        //        }

        //        // And again for fire, with the unshaded shader
        //        state.drawHandle.UseShader(state.shader);
        //        foreach (var chunk in comp.Chunks.Values)
        //        {
        //            var enumerator = new GasChunkEnumerator(chunk);

        //            while (enumerator.MoveNext(out var gas))
        //            {
        //                if (gas.FireState == 0)
        //                    continue;

        //                var index = chunk.Origin + (enumerator.X, enumerator.Y);
        //                if (!localBounds.Contains(index))
        //                    continue;

        //                var fireState = gas.FireState - 1;
        //                var texture = state.fireFrames[fireState][state.fireFrameCounter[fireState]];
        //                state.drawHandle.DrawTexture(texture, index);
        //            }
        //        }

        //        return true;
        //    ;

        //drawHandle.UseShader(null);
        //drawHandle.SetTransform(Matrix3x2.Identity);

        //}


        //protected override void Draw(in OverlayDrawArgs args)
        //{
        //    if (ScreenTexture == null)
        //        return;

        //    var handle = args.WorldHandle;
        //    _heatShader.SetParameter("SCREEN_TEXTURE", ScreenTexture);
        //    _heatShader.SetParameter("currentTime", _currentTime);
        //    _heatShader.SetParameter("DISTORT_TEXTURE", _heatDistortTexture);

        //    handle.UseShader(_heatShader);
        //    handle.DrawRect(args.WorldBounds, Color.White);
        //    handle.UseShader(null);
    }
    protected override void Draw(in OverlayDrawArgs args)
    {
        if (ScreenTexture == null)
            return;

        var handle = args.WorldHandle;
        _heatShader.SetParameter("SCREEN_TEXTURE", ScreenTexture);
        _heatShader.SetParameter("currentTime", _currentTime);
        _heatShader.SetParameter("DISTORT_TEXTURE", _heatDistortTexture);

        handle.UseShader(_heatShader);
        handle.DrawRect(args.WorldBounds, Color.White);
        handle.UseShader(null);
    }


}
