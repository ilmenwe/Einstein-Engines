using Robust.Client.GameObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Robust.Client.GameObjects;

public static class SpriteComponentExtended
{
    public static void Print(this SpriteComponent spriteComponent, TextWriter textOut)
    {
        //var logger = Logger.GetSawmill(nameof(SpriteComponent));
        int layerCount = 0;
        
        foreach (var layer in spriteComponent.AllLayers)
        {

            string output = @$"
                State = {layer?.RsiState.Name}
                Layer# = {layerCount++}
                AntimationFrame = {layer?.AnimationFrame}
                AnimationTime= {layer?.AnimationTime}
                AutoAnimated = {layer?.AutoAnimated}
                Color = {layer?.Color}
                DirOffset = {layer?.DirOffset}
                PixelSize = {layer?.PixelSize}
                Rotation = {layer?.Rotation}
                Scale = {layer?.Scale}
                Visible= {layer?.Visible}

";
            textOut.WriteLine(output);
        }
        
    }
}
