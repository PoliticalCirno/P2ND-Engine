using System.Numerics;
using System;
using System.Linq;
using System.Text;
using static Raylib_cs.Raylib;
using System.Drawing;
using Game_Objects;
using Engine_Logics;
using Engine_Logics.Sub.PlayerCon;
using Engine_Resource;
using Raylib_cs;


namespace Engine_Scenestates
{
    public class Scenestate
    {
        public static List<Engine_Resource.Mdl> modl = new List<Mdl>();
        public static List<Game_Objects.GameObjects.Props> prop = new List<GameObjects.Props>();
        public static List<Game_Objects.GameObjects.Brush> brsh = new List<GameObjects.Brush>();
        public static List<Game_Objects.GameObjects.Lights> ligt = new List<GameObjects.Lights>();
        

        private static int state_id = 0; // 0 = splashscreen, 1 = menu, 2 = loading thread, 3 = ingame, 4 = test ingame[WILL DEPRECATE SOON]
        public class States
        {

            public static void SwitcScene(int sc)
            {
                state_id = sc;
                bool loadcycle = true;
                if (loadcycle == true)
                {
                    Maps.LoadAll();
                }
                loadcycle = false;
            }
        }

        public class Maps // 0 = testmap, 1 = nmap
        {

            public static void LoadAll()
            {
                prop.Clear();
                brsh.Clear();
                Shadercl.InitializeShaderPBR();
                LoadPrereq();
                if (state_id == 4)
                {
                    ligt.Add(new GameObjects.Lights(new Vector3(1.0f, 1.9f, 1.0f), new Vector3(0.0f, 0.0f, 0.0f), true, Light_types.Point, Raylib_cs.Color.Blue, 100.0f, Shadercl.Mat_PBR));
                    ligt.Add(new GameObjects.Lights(new Vector3(0.0f, 30.5f, 40.0f), new Vector3(0.0f, 0.0f, 0.0f), true, Light_types.Point, Raylib_cs.Color.White, 1000.0f, Shadercl.Mat_PBR));  
                    Game_Objects.GameObjects.Lights.UpdateShaderValues(ligt[0], Shadercl.Mat_PBR);
                    Game_Objects.GameObjects.Lights.UpdateShaderValues(ligt[1], Shadercl.Mat_PBR);
                    //brsh.Add(new GameObjects.Brush() { brushScale = new Vector3(20, 20, 5), brushPos = new Vector3(0.0f, -2.5f, 0.0f) });
                    //brsh.Add(new GameObjects.Brush() { brushScale = new Vector3(5, 5, 5), brushPos = new Vector3(0.0f, 14.0f, 10.0f) });
                    //prop.Add(new GameObjects.Props(modl[1].mdl, 0x000001, Prop_types.Prop_Static, "FwwvCube", false, 0x000000, new Vector3(0.0f, -0.1f, 20.0f), Vector3.Zero, 1.0f));
                    prop.Add(new GameObjects.Props(modl[2].mdl, 0x000002, Prop_types.Prop_Static, "gag", false, 0x000000, new Vector3(0.0f, -1f, 10.0f), Vector3.Zero, 1.0f));
                    ControlCorrespondant.SetGameDefault(new Vector3(0.0f, 0.0f, 0.0f));
                    Console.WriteLine("DONE______________________________________________________________!\n");
                    //Idfk how im gonna keep this from crashing when it doesn't detect index so for now just ensure there are atleast 2 brushes.
                }
            }

            public static void LoadPrereq()
            {
                modl.Add(new Mdl(LoadModel("resources/models/test/test_mdl.glb"), 0x000001, 0x000000));
                modl.Add(new Mdl(LoadModel("resources/models/test/test_scene_part1.glb"), 0x000002, 0x000000, LoadTexture("resources/models/test/diffuse.png"), LoadTexture("resources/models/test/normal.png"), Shaders.Mat_PBR_Metallic));
                modl.Add(new Mdl(LoadModel("resources/models/test/test_scene_part2.glb"), 0x000003, 0x000000, LoadTexture("resources/models/test/demotex/conc.jpg"), LoadTexture("resources/models/test/demotex/concnorm.jpg"), LoadTexture("resources/models/test/demotex/concmra.png"),Shaders.Mat_PBR_Metallic));
            }
        
        }

        public static int StateId()
        {
            return state_id;
        }
    }
}
