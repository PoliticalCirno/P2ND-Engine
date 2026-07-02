using System.Numerics;
using BepuUtilities;
using Engine.Game.Objects;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace Engine.Render.Optimization.Culling
{
    class Simple
    {
        public static unsafe void Debug(Camera3D cam, Vector3 objPos)
        {
            var dp = Vector3.Dot(GetCameraForward(&cam), Vector3.Subtract(objPos , cam.Position));
            Engine.Logics.Dev.Variables.targetObjectDp = (float)Math.Round(dp, 3);
            
        }
    }
}