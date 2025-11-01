using System.Numerics;
using System;
using System.Linq;
using System.Text;
using static Raylib_cs.Raylib;
using System.Drawing;
using Engine_Scenestates;
using Game_Objects;
using Raylib_cs;

namespace Engine_Logics.Sub.PlayerCon
{
    public class ControlCorrespondant
    {
        public static float playerspeed = 0.0f;
        public static float playerspeedside = 0.0f;
        public static Vector3 playerpos;
        public static Camera3D camfps;
        public static float mouseSensitivity = 0.2f;
        private static float phi; //1 
        private static float theta; //2 
        public static void SetGameDefault(Vector3 player)
        {
            DisableCursor();
            playerpos = player;
            camfps = new();
            camfps.Position = new Vector3(playerpos.X, playerpos.Y + 2, playerpos.Z);
            camfps.FovY = 45.0f;
            camfps.Projection = CameraProjection.Perspective;
            camfps.Target = new Vector3(1.0f, 0.0f, 0.0f);
            camfps.Up = new Vector3(0.0f, 5.0f, 0.0f);
        }

        static void UpdatePlayerMovementSmooth(float slowplusplus, float leftplusplus)
        {
            var normtarg = camfps.Position - camfps.Target;
            var right = Vector3.Cross(camfps.Up, normtarg);
            normtarg.Y = 0;
            camfps.Position = Vector3.Subtract(camfps.Position, Vector3.Normalize(right) * leftplusplus);
            camfps.Position = Vector3.Subtract(camfps.Position, (Vector3.Normalize(normtarg) * slowplusplus));

        }

       /* static void Sideways(float leftplusplus)
        {
            var normtarg = camfps.Position - camfps.Target;
            normtarg.Y = 0;
            var right = Vector3.Cross(camfps.Up, normtarg);
            camfps.Position = Vector3.Add(camfps.Position,  Vector3.Normalize(right) * leftplusplus);
        }*/

        public static void DeleteThisFunc()
        {
            for (int i = 0; i <= Scenestate.prop.Count - 1; i++)
            {
                Scenestate.prop[i].Position = Vector3.Lerp(Scenestate.prop[i].Position, camfps.Position, 1.0f * GetFrameTime());
            }
        }

        public static void SideCon()
        {
            if (IsKeyDown(KeyboardKey.A))
            {
                if (playerspeedside < 4.0f * GetFrameTime())
                {
                    playerspeedside += 1.0f * GetFrameTime();
                }

            }

            if (IsKeyDown(KeyboardKey.D))
            {
                if (playerspeedside > -4.0f * GetFrameTime())
                {
                    playerspeedside -= 1.0f * GetFrameTime();
                }

            }

            else if (IsKeyUp(KeyboardKey.A) && IsKeyUp(KeyboardKey.D))
            {
                if (playerspeedside > 0.00f)
                {
                    playerspeedside -= 0.5f * GetFrameTime();
                }

                if (playerspeedside < 0.00f)
                {
                    playerspeedside += 0.5f * GetFrameTime();
                }

                if (playerspeedside < 0.01f && playerspeedside > -0.01)
                {
                    playerspeedside = 0.0f;
                }
            }
        }

        public static void ForCon()
        {
                        if (IsKeyDown(KeyboardKey.W))
            {
                if (playerspeed < 4.0f * GetFrameTime())
                {
                    playerspeed += 2.0f * GetFrameTime();
                }

            }

            if (IsKeyDown(KeyboardKey.S))
            {
                if (playerspeed > -4.0f * GetFrameTime())
                {
                    playerspeed -= 2.0f * GetFrameTime();
                }

            }

            else if (IsKeyUp(KeyboardKey.W) && IsKeyUp(KeyboardKey.S))
            {
                if (playerspeed > 0.00f)
                {
                    playerspeed -= 0.5f * GetFrameTime();
                }

                if (playerspeed < 0.00f)
                {
                    playerspeed += 0.5f * GetFrameTime();
                }

                if (playerspeed < 0.01f && playerspeed > -0.01f)
                {
                    playerspeed = 0.0f;
                }
            }
        }

        public static void UpdatePlayerLogic()
        {

            Vector2 mousedelta = GetMouseDelta();
            phi -= mousedelta.Y * mouseSensitivity * GetFrameTime();
            theta -= mousedelta.X * mouseSensitivity * GetFrameTime();
            SetMousePosition(1920 / 2, 1080 / 2);
            phi = (float)Math.Clamp(phi, -MathF.PI / 2.0f + 0.45, MathF.PI / 2.0f - 0.45f);
            float phi2 = (float)Math.Clamp(phi, -MathF.PI / 2.0f + 0.045, MathF.PI / 2.0f - 0.45f);
            Vector3 Alt = new();
            Alt.Z = MathF.Cos(phi2) * MathF.Cos(theta);
            Alt.X = MathF.Cos(phi2) * MathF.Sin(theta);
            Alt.Y = MathF.Sin(phi2);

            camfps.Target = Vector3.Add(camfps.Position, Alt);

            SideCon();
            ForCon();
            //Console.WriteLine(playerspeed);
            UpdatePlayerMovementSmooth(playerspeed, playerspeedside);
            //Console.WriteLine(playerspeedside);
        }
    }
}

