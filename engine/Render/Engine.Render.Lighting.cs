using System.Numerics;
using Engine.Game.Objects;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace Engine.Render.LightingEngine
{
        public class Lighting
    {
        public static void UpdateLight(Shader shad, GameObjects.Lights lights)
        {

                SetShaderValue(shad, lights.enabledLoc, lights.Enabled, ShaderUniformDataType.Int);
                SetShaderValue(shad, lights.typeLoc, lights.Type, ShaderUniformDataType.Int);


                SetShaderValue(shad, lights.positionLoc, lights.Position, ShaderUniformDataType.Vec3);
                
                SetShaderValue(shad,lights.targetLoc, lights.Target, ShaderUniformDataType.Vec3);
                SetShaderValue(shad, lights.colorLoc, lights.Colour, ShaderUniformDataType.Vec4);
                SetShaderValue(shad, lights.intensityLoc, lights.Intensity, ShaderUniformDataType.Float);
        }
    }

}