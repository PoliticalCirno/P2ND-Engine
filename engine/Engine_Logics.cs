using System.Numerics;
using System;
using System.Linq;
using System.Text;
using static Raylib_cs.Raylib;
using System.Drawing;
using Engine_Scenestates;
using Game_Objects;
using Raylib_cs;

namespace Engine_Logics
{
    public class Menu
    {

    }

    public class Game
    {
        // Player control logic moved to sub logic playercontrol.cs

        public static void DeleteThisFunc()
        {
            for (int i = 0; i <= Scenestate.prop.Count - 1; i++)
            {
                Scenestate.prop[i].Position = Vector3.Lerp(Scenestate.prop[i].Position, Engine_Logics.Sub.PlayerCon.ControlCorrespondant.camfps.Position, 1.0f * GetFrameTime());
            }
        }
    }
}

