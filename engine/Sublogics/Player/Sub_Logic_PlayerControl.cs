using System.Numerics;
using static Raylib_cs.Raylib;
using Engine_Scenestates;
using Raylib_cs;

namespace Engine_Logics.Sub.PlayerCon.FirstPerson
{
    public class PlayerBody
    {
        
    }

    public class ControlCorrespondant
    {
        public static float playerspeed = 0.0f;
        public static float playerspeedside = 0.0f;
        public static Vector3 playerpos;
        public static Camera3D camfps;
        public static float mouseSensitivity = 0.5f;
        private static float phi; //1 
        private static float theta; //2 
        private static float phi2;

        private static Vector2 mouseDelta;
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
            //camfps.Position = Vector3.Subtract(camfps.Position, Vector3.Normalize(right) * leftplusplus  * GetFrameTime());
            //camfps.Position = Vector3.Subtract(camfps.Position, ((Vector3.Normalize(normtarg) * slowplusplus * GetFrameTime())));
            camfps.Position = Vector3.Lerp(camfps.Position,Vector3.Subtract(camfps.Position, Vector3.Normalize(right) * leftplusplus), 1.0f * GetFrameTime());
            camfps.Position = Vector3.Lerp(camfps.Position, Vector3.Subtract(camfps.Position, ((Vector3.Normalize(normtarg) * slowplusplus))), 1.0f * GetFrameTime());


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
                if (playerspeedside < 4.0f)
                {
                    playerspeedside += 1.0f;
                }

            }

            if (IsKeyDown(KeyboardKey.D))
            {
                if (playerspeedside > -4.0f)
                {
                    playerspeedside -= 1.0f;
                }

            }

            else if (IsKeyUp(KeyboardKey.A) && IsKeyUp(KeyboardKey.D))
            {
                if (playerspeedside > 0.00f)
                {
                    playerspeedside -= 0.5f;
                }

                if (playerspeedside < 0.00f)
                {
                    playerspeedside += 0.5f;
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
                if (playerspeed < 8.0f)
                {
                    playerspeed += 2.0f;
                }

            }

            if (IsKeyDown(KeyboardKey.S))
            {
                if (playerspeed > -8.0f)
                {
                    playerspeed -= 2.0f;
                }

            }

            else if (IsKeyUp(KeyboardKey.W) && IsKeyUp(KeyboardKey.S))
            {
                if (playerspeed > 0.00f)
                {
                    playerspeed -= 0.5f;
                }

                if (playerspeed < 0.00f)
                {
                    playerspeed += 0.5f;
                }

                if (playerspeed < 0.01f && playerspeed > -0.01f)
                {
                    playerspeed = 0.0f;
                }
            }
        }

        public static void UpdatePlayerLogic()
        {

            mouseDelta = Vector2.Lerp(mouseDelta, GetMouseDelta(), 19.1f * GetFrameTime());
            phi -= mouseDelta.Y * mouseSensitivity * GetFrameTime();
            theta -= mouseDelta.X * mouseSensitivity * GetFrameTime();
            SetMousePosition(1920 / 2, 1080 / 2);
            phi = (float)Math.Clamp(phi, -MathF.PI / 2.0f + 0.45, MathF.PI / 2.0f - 0.45f);
            //phi = float.Lerp(phi, (float)Math.Clamp(phi, -MathF.PI / 2.0f + 0.45, MathF.PI / 2.0f - 0.45f), 0.1f);
            //phi2 = float.Lerp(phi2, (float)Math.Clamp(phi, -MathF.PI / 2.0f + 0.045, MathF.PI / 2.0f - 0.45f), 0.1f);
            phi2 = (float)Math.Clamp(phi, -MathF.PI / 2.0f + 0.045, MathF.PI / 2.0f - 0.45f);

            Vector3 Alt = new();
            Alt.Z = MathF.Cos(phi2) * MathF.Cos(theta);
            Alt.X = MathF.Cos(phi2) * MathF.Sin(theta);
            Alt.Y = MathF.Sin(phi2);

            //Alt.Z = float.Lerp(Alt.Z, MathF.Cos(phi2) * MathF.Cos(theta), 0.005f);
            //Alt.X = float.Lerp(Alt.X, MathF.Cos(phi2) * MathF.Sin(theta), 0.005f);
            //Alt.Y = float.Lerp(Alt.Y, MathF.Sin(phi2), 0.05f);


            UpdatePlayerMovementSmooth(playerspeed, playerspeedside);
            //camfps.Target = Vector3.Lerp(camfps.Target, Vector3.Add(camfps.Position, Alt), 29.5f * GetFrameTime());
             camfps.Target = Vector3.Add(camfps.Position, Alt);

            SideCon();
            ForCon();
            //Console.WriteLine(playerspeed);
            //Console.WriteLine(playerspeedside);
        }
    }
}

