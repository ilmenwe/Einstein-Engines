using System.Linq;

namespace Content.Shared._EE.EExtendedSprite;

public sealed partial class EExtendedSpritePart
{
    public int DepthWeight = 1;
    public Dictionary<string, List<EExtendedSpriteStep>> Steps { get; set; } = [];

    public void AddStep(string stepName, int weightedDepth, PrototypeLayerData data)
    {
        if (!Steps.TryGetValue(stepName, out var stepList))
        {
            Steps.Add(stepName, stepList = []);
        }
        stepList.Add(new EExtendedSpriteStep { DepthWeight = weightedDepth, LayerData = data });
    }

    public IEnumerable<PrototypeLayerData> OrderedLayerDataList()
    {
        var parts = Steps.SelectMany(st => st.Value);
        return parts.OrderBy(step => step.DepthWeight).Select(step => step.LayerData!);
    }
}
