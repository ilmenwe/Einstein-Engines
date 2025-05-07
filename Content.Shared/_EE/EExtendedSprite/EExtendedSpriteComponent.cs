using Content.Shared._EE.EExtendedSprite;
using Content.Shared.Clothing.EntitySystems;
using Content.Shared.Inventory;
using Robust.Shared.GameStates;

namespace Content.Shared._EE.ExtendedSprite;

/// <summary>
/// a component having all the same fields as the default sprite, making them instant interchangeable but going trough a proxy system adapting parameters and additional values. 
/// </summary>

[NetworkedComponent]
[RegisterComponent]
[Access(typeof(SharedEExtendedSpriteSystem))]
public sealed partial class EExtendedSpriteComponent : Component
{
    private Dictionary<string, EExtendedSpritePart> SpriteParts = [];

    //public void CopyToSpriteComponent(in SpriteComponent component)
    //{

    //    //var parts = _spriteParts
    //    //    .Select(extendedSprite => extendedSprite.Value)
    //    //    .OrderBy(s => s.DepthWeight);

    //    //parts.Select(part => part.Steps).Select(step => step.Values)
    //    //    .OrderBy(ste => ste.

    //    //)
    //    //{


    //    //}

    //}
}
