using System.Numerics;
using System;
using System.Linq;
using System.Text;
using static Raylib_cs.Raylib;
using System.Drawing;
using Engine_Scenestates;
using Engine_Logics;
using Engine_Logics.Sub.PlayerCon;
using Engine_Render;
using System.Security.Cryptography.X509Certificates;
using Engine_Resource;
using Raylib_cs;

/****************************************
P2Engine

Part of a project being made by CSoft With Codename:
P2D Radiant.

CURRENT UPDATE: "Cavelier"

****************************************/
namespace RDN
{
    class Program
    {
        
        public static int Main()
        {
            // Initialization
            //--------------------------------------------------------------------------------------
            const int screenWidth = 2320;
            const int screenHeight = 1380;
            Environment.SetEnvironmentVariable("CUDA_VISIBLE_DEVICES", "0");
            SetConfigFlags(Raylib_cs.ConfigFlags.Msaa4xHint);
            InitWindow(screenWidth, screenHeight, "P2ND");
            Scenestate.States.SwitcScene(4);
            SetTargetFPS(60);
                while (!WindowShouldClose())
                {
                    ControlCorrespondant.UpdatePlayerLogic();
		    Rlgl.EnableDepthTest();
                    Shadercl.ShaderUpdateRuntimePrePBR();    
                    Render.Rend_Unified();
                }
            CloseWindow();

            return 0;
    }
    }
}
