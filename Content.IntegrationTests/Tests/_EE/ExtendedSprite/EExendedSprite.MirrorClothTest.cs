using Content.Client.Clothing;
using Content.Server.Clothing;
using Content.Shared._EE.EExtendedSprite;
using Content.Shared._EE.ExtendedSprite;
using Content.Shared.Clothing.EntitySystems;
using Robust.Client.GameObjects;
using Robust.Shared.GameObjects;
using Robust.Shared.Log;
using Robust.Shared.Map;
using Robust.Shared.Timing;
using Robust.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Robust.Server.Player;

namespace Content.IntegrationTests.Tests.EExtendedSprite;


//public static class SpriteComponentExtension
//{
//    public static Task PrintAll(this SpriteComponent spriteComponent, TextWriter testContext)
//    {
//        return testContext.WriteLineAsync(
//    $"{spriteComponent.ToString()}");

//    }
//}


[TestFixture]
[TestOf(typeof(SharedEExtendedSpriteSystem))]
public sealed class EExtendedSpriteClothTest : RobustIntegrationTest
{

    [Test]
    public async Task CompareSpriteComponentOutput()
    {

        TestContext.Out.WriteLine("1 HELLO!!!!!");
        await using var pair = await PoolManager.GetServerClient(new PoolSettings { Connected = true, DummyTicker = false, });
        var server = pair.Server;
        var client = pair.Client;
        var serverEntityManager = server.ResolveDependency<IEntityManager>();
        var clientEntityManager = client.ResolveDependency<IEntityManager>();
        var serverMapManager = server.ResolveDependency<IMapManager>();
        var clientMapManager = client.ResolveDependency<IMapManager>();
        var clientSession = client.Session;
        var serverSession = server.ResolveDependency<IPlayerManager>().Sessions.Single();

        var mapSystem = server.System<SharedMapSystem>();
        var serverGameTiming = server.ResolveDependency<IGameTiming>();
        var clientGameTiming = client.ResolveDependency<IGameTiming>();
        EntityUid gridId = default;
        MapId mapId = new(-1);
        TestContext.Out.WriteLine("3 HELLO!!!!!");

        await client.WaitIdleAsync();
        await server.WaitIdleAsync();



        TestContext.Out.WriteLine("4 HELLO!!!!!");

        await client.WaitPost(() => { });


        ClothingSystem _serverClothingSystem = null;

        NetEntity? entityA = null;
        NetEntity? entityB = null;


        await server.WaitAssertion(() =>
        {
            _serverClothingSystem = server.System<ServerClothingSystem>();
            mapSystem.CreateMap(out mapId);
            var grid = serverMapManager.CreateGridEntity(mapId);
            gridId = grid.Owner;
            var internalEntityA = serverEntityManager.SpawnEntity(null, new MapCoordinates(0, 0, mapId), null);
            entityA = serverEntityManager.GetNetEntity(internalEntityA);

            var internalEntityB = serverEntityManager.SpawnEntity("MobHuman", new MapCoordinates(0.0f, 0.0f, mapId));
            entityB = serverEntityManager.GetNetEntity(internalEntityB);
            serverEntityManager.AddComponent<EExtendedSpriteComponent>(internalEntityA);
        });
        var serverEnt = serverSession.AttachedEntity!.Value;
        var clientEnt = clientSession!.AttachedEntity!.Value;

        await pair.SyncTicks();
        //IEnumerable<(EntityUid entitiyUid, IComponent component)> clientSpriteComponents = [];
        //IEnumerable<(EntityUid entitiyUid, IComponent component)> clientEExtendedSpriteComponents = [];
        //await client.WaitAssertion(() =>
        //{

        //    //var internalEntityA = clientEntityManager.GetEntity(entityA);
        //    //var internalEntityB = clientEntityManager.GetEntity(entityB);

        //    //TestContext.Out.WriteLine("6 HELLO!!!!!");
        //    //clientEntityManager.AddComponent<SpriteComponent>(internalEntityA.Value);
        //    clientSpriteComponents = clientEntityManager.GetAllComponents(typeof(SpriteComponent));
        //    clientEExtendedSpriteComponents = clientEntityManager.GetAllComponents(typeof(EExtendedSpriteComponent));
        //    foreach (var spriteComponent in clientSpriteComponents)
        //    {
        //        ((SpriteComponent)spriteComponent.component).Print();
        //    }
        //});

        await pair.RunTicksSync(50);

        var clientSpriteComponents = clientEntityManager.GetAllComponents(typeof(SpriteComponent));
        foreach (var spriteComponent in clientSpriteComponents)
        {
            ((SpriteComponent) spriteComponent.Component).Print(TestContext.Out);
        }
        var serverEExtendedSpriteComponent = serverEntityManager.GetAllComponents(typeof(EExtendedSpriteComponent));

        Assert.Multiple(() =>
        {
            //Assert.That(clientSpriteComponents.Count(), Is.EqualTo(2));
            //Assert.That(clientEExtendedSpriteComponents.Count(), Is.EqualTo(1));
            //Assert.That(serverSpriteComponents.Count(), Is.EqualTo(0));
            //Assert.That(serverEExtendedSpriteComponent.Count(), Is.EqualTo(1));

            TestContext.Out.WriteLine("HELLO ASSERT!!!!!");
        });
        List<SpriteComponent> components = [];
        TestContext.Out.WriteLine("7 HELLO!!!!!");

        TestContext.Out.WriteLine("8 HELLO!!!!!");
        pair.Client.Stop();
        pair.Server.Stop();

        TestContext.Out.WriteLine("9 HELLO!!!!!");


    }
}
