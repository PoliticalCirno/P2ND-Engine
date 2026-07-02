using System.Numerics;
using System;
using System.Linq;
using System.Text;
using static Raylib_cs.Raylib;
using System.Drawing;
using Engine.Game.Objects;
using Engine.Resource;
using Raylib_cs;
using Engine.Logics.Sub.PlayerCon.FirstPerson;


namespace Engine_Scenestates
{
    public class Scenestate
    {
        public static List<Engine.Resource.Mdl> modl = new List<Mdl>();
        public static List<Engine.Game.Objects.GameObjects.Props> prop = new List<GameObjects.Props>();
        public static List<Engine.Game.Objects.GameObjects.Brush> brsh = new List<GameObjects.Brush>();
        public static List<Engine.Game.Objects.GameObjects.Lights> ligt = new List<GameObjects.Lights>();
        

        private static int state_id = 0; // 0 = splashscreen, 1 = menu, 2 = loading thread, 3 = ingame, 4 = test ingame[WILL DEPRECATE SOON]
        public class States
        {

            public static void SwitcScene(int sc)
            {
                state_id = sc;
                bool loadcycle = true;
                
                if (loadcycle == true)
                    Maps.LoadAll();
                
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
                    ligt.Add(new GameObjects.Lights(new Vector3(100f, 0, 0f), new Vector3(0f, 0.0f, 0.0f), false, Light_types.Point, Raylib_cs.Color.Blue, 0.0f, Shadercl.Mat_PBR));  
                    //new Vector3(5.5f, -1.0f, 5.0f)
                    ligt.Add(new GameObjects.Lights(new Vector3(1f, 2f, 1f), new Vector3(10.5f, 0.0f, 8.0f), true, Light_types.Directorional, Raylib_cs.Color.SkyBlue, 5.0f, Shadercl.Mat_PBR));
                    //ligt.Add(new GameObjects.Lights(new Vector3(1f, 0f, 1f), new Vector3(10.5f, 1.0f, 4.0f), true, Light_types.Point, Raylib_cs.Color.RayWhite, 0.0f, Shadercl.Mat_PBR));  at_PBR));  
                    ligt.Add(new GameObjects.Lights(new Vector3(0f, 5f, -10f), new Vector3(-10f, 0f, 8.0f), true, Light_types.Directorional, Raylib_cs.Color.Violet, 9.0f, Shadercl.Mat_PBR));  
                    //Engine.Game.Objects.GameObjects.Lights.UpdateShaderValues(ligt[0], Shadercl.Mat_PBR);
                    Engine.Game.Objects.GameObjects.Lights.UpdateShaderValues(ligt[2], Shadercl.Mat_PBR);
                    Engine.Game.Objects.GameObjects.Lights.UpdateShaderValues(ligt[1], Shadercl.Mat_PBR);
                    prop.Add(new GameObjects.Props(modl[0].mdl, 0x000002, Prop_types.Prop_Static, "gag", false, 0x000000, new Vector3(0.0f, 0f, 0.0f), Vector3.Zero, 1.0f));
                    ControlCorrespondant.SetGameDefault(new Vector3(0.0f, 0.0f, 0.0f));
                    Console.WriteLine("DONE______________________________________________________________!\n");
                    
                }
            }

            public static void LoadPrereq()
            {
                //modl.Add(new Mdl(LoadModel("resources/models/test/test_mdl.glb"), 0x000001, 0x000000));
                //modl.Add(new Mdl(LoadModel("resources/models/test/test_scene_part1.glb"), 0x000002, 0x000000, LoadTexture("resources/models/test/diffuse.png"), LoadTexture("resources/models/test/normal.png"), Shaders.Mat_PBR_Metallic));
                //modl.Add(new Mdl(LoadModel("resources/models/test/test_scene_part2.glb"), 0x000003, 0x000000, LoadTexture("resources/models/test/demotex/conc.jpg"), LoadTexture("resources/models/test/demotex/concnorm.jpg"), LoadTexture("resources/models/test/demotex/concmra.png"),Shaders.Mat_PBR_Metallic));
                modl.Add(new Mdl(LoadModel("resources/models/prereq/Def.glb"), 0x000003, 0x000000, LoadTexture("resources/models/prereq/default/DefaultTextureDiffuse.png"), LoadTexture("resources/models/test/demotex/concnorma.jpg"), LoadTexture("resources/models/prereq/default/DefaultTexture.png"),Shaders.Mat_PBR_Metallic));
                //modl.Add(new Mdl(LoadModel("resources/models/test/test_stress.glb"), 0x000001, 0x000000));
            }
        
        }

        public static int StateId() { return state_id; }
    }
}
