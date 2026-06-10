using System.Numerics;
using Engine.Game.Objects;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace Engine.Render.LightingEngine
{
        public class Lighting
    {
        public static void UpdateLight(Shader shad, GameObjects.Lights lige)
        {

                SetShaderValue(shad, lige.enabledLoc, lige.Enabled, ShaderUniformDataType.Int);
                SetShaderValue(shad, lige.typeLoc, lige.Type, ShaderUniformDataType.Int);


                SetShaderValue(shad, lige.positionLoc, lige.Position, ShaderUniformDataType.Vec3);
                
                SetShaderValue(shad,lige.targetLoc, lige.Target, ShaderUniformDataType.Vec3);
                SetShaderValue(shad, lige.colorLoc, lige.Colour, ShaderUniformDataType.Vec4);
                SetShaderValue(shad, lige.intensityLoc, lige.Intensity, ShaderUniformDataType.Float);
        }
    }

}