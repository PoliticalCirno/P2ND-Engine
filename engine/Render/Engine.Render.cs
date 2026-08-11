using static Raylib_cs.Raylib;
using Engine.Game.Objects;
using Engine.Logics.Sub.PlayerCon.FirstPerson;
using Engine_Scenestates;
using Raylib_cs;
using Engine.Resource;
using Engine.Game;
using System.Numerics;
using ComponentSystem;

namespace Engine.Render
{
    public class Renders
    {
        private class Rend_2D
        {

        }

        private static class Rend_3D
        {
            public static void RenderBrush()
            {
                    Shadercl.ShaderUpdateRuntimeDuringPBR();
            }

            public static void Render_3D()
            {
                Lighting.UpdateLight(Shadercl.Mat_PBR, Scenestate.ligt[0]);
                Lighting.UpdateLight(Shadercl.Mat_PBR, Scenestate.ligt[1]);
                //Console.WriteLine(Scenestate.ligt[0].Position);
                BeginMode3D(ControlCorrespondant.camfps);
                RenderBrush();
                //DrawModel(Scenestate.entities[0].GetComponent<Mesh3D>().model, Vector3.Zero, 1, Raylib_cs.Color.White);
                TransformSystem.Update(GetFrameTime());
                Mesh3DSystem.Update(GetFrameTime());
                if(Engine.Logics.Dev.Variables.targetObjectDp > 2)
                {
                    DrawCube(new System.Numerics.Vector3(4, 5, 1) , 1, 1, 1, Raylib_cs.Color.Red);
                }

                    DrawCube(new System.Numerics.Vector3(20.5f, 1.0f, 5.0f) , 1, 1, 1, Raylib_cs.Color.Red);
                DrawGrid(20, 1.0f);
                EndMode3D();
            }
            
        }

        private class Rend_UI
        {
            public static void Render_FpsCounter()
            {
                var mdp = Engine.Logics.Dev.Variables.targetObjectDp;
                DrawText($"Current fps: {GetFPS()}", 10, 10, 80, Raylib_cs.Color.DarkBlue);
                DrawText($"Dp: {mdp}", 10, 80, 50, Raylib_cs.Color.DarkBlue);
                DrawText($"Mem: {Engine.Logics.Dev.Variables.memory:F2}MB", 10, 120, 50, Raylib_cs.Color.DarkBlue);



            }
        }

        public static void Rend_Unified()
        {
            UpdateCamera(ref ControlCorrespondant.camfps, CameraMode.Custom);
            BeginDrawing();
            BeginBlendMode(BlendMode.Alpha);//TODO: figure out why the hell transparency keeps clipping everything behind it.
            ClearBackground(Raylib_cs.Color.White);
            Rend_3D.Render_3D();
            Rend_UI.Render_FpsCounter();
            EndDrawing();
        }
    }

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
