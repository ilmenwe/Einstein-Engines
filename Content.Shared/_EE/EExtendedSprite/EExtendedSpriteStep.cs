using Robust.Shared.Graphics.RSI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Content.Shared._EE.EExtendedSprite;

public sealed class EExtendedSpriteStep
{
    [DataField]
    public int DepthWeight = 1;

    [DataField]
    public PrototypeLayerData? LayerData;
}
