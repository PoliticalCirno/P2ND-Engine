using System.Diagnostics;
using static Raylib_cs.Raylib;
using Engine_Scenestates;
using Engine.Render;
using Engine.Resource;
using Raylib_cs;
using Engine.Logics.Sub.PlayerCon.FirstPerson;
using Engine.Render.Optimization.Culling;

/****************************************
P2Engine

Changed team name from CSoft to WFIO

Part of a project being made by WFIO With Codename:
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
	        GpuDetection.RunDetection();
            SetConfigFlags(Raylib_cs.ConfigFlags.Msaa4xHint);
            InitWindow(screenWidth, screenHeight, "P2ND");
            Scenestate.States.SwitcScene(4);
            SetTargetFPS(120);
                while (!WindowShouldClose())
                {
                    Process currentProcess = Process.GetCurrentProcess();
                    ControlCorrespondant.UpdatePlayerLogic();
		            Rlgl.EnableDepthTest();
                    Shadercl.ShaderUpdateRuntimePrePBR();    
                    Renders.Rend_Unified();
                    long privateMemoryBytes = currentProcess.PrivateMemorySize64;
                    double privateMemoryMB = privateMemoryBytes / 1024.0 / 1024.0;
                    //Console.WriteLine($"Current RAM usage: {privateMemoryMB:F2} MB");
                    Engine.Logics.Dev.Variables.memory = privateMemoryMB;
                }
            CloseWindow();

            return 0;
    }
    }
}
