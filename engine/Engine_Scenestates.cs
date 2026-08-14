using System.Numerics;
using System;
using System.Linq;
using System.Text;
using static Raylib_cs.Raylib;
using System.Drawing;
using Engine.Game.Objects;
using Engine.Resource;
using Engine.Render.MaterialSystem;
using Raylib_cs;
using Engine.Logics.Sub.PlayerCon.FirstPerson;
using Engine.Game;
using ComponentSystem;


namespace Engine_Scenestates
{
    class Scenestate
    {
        public static List<Engine.Game.Objects.GameObjects.Lights> ligt = new List<GameObjects.Lights>();
        public static List<Entity> entities = new();

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
                    //prop.Add(new GameObjects.Props(modl[0].mdl, 0x000002, Prop_types.Prop_Static, "gag", false, 0x000000, new Vector3(0.0f, 0f, 0.0f), Vector3.Zero, 1.0f));
                    ControlCorrespondant.SetGameDefault(new Vector3(0.0f, 0.0f, 0.0f));
                    Console.WriteLine("DONE______________________________________________________________!\n");
                    
                }
            }

            public static void LoadPrereq()
            {
                Model moddy = LoadModel("resources/models/prereq/Def.glb");
                entities.Add(new Entity());
                ComponentSystem.Transform transform = new ComponentSystem.Transform();
                transform.position = new Vector3(0, 0, 0);
                transform.scale.X = 1;
                
                ComponentSystem.Mesh3D mesh = new Mesh3D();
                mesh.model = moddy;
                mesh.materialAssigned = mesh.materialAssigned.Append(1).ToArray();
                mesh.materialAssigned[0] = 0;
                Console.WriteLine("\n\n\n\n\n\n"+mesh.materialAssigned.Length+"\n\n\n\n\n\n");
                Console.WriteLine("\n\n\n\n\n\n"+mesh.materialAssigned[0]+"\n\n\n\n\n\n");
                entities[0].AddComponent(transform);
                entities[0].AddComponent(mesh);
                MaterialMemorySystem.checkActiveMaterialInScene();
                

                entities.Add(new Entity());
                ComponentSystem.Mesh3DTrans m3t = new();
                ComponentSystem.Transform transforma = new ComponentSystem.Transform();
                ComponentSystem.Mesh3D mesha = new Mesh3D();
                transforma.position = new Vector3(-10, 30, 50);
                transforma.scale.X = 6;
                mesha.model = LoadModel("resources/models/test/LightShaft/LightPierce.glb");
               // mesh.materialAssigned = mesh.materialAssigned.Append(1).ToArray();
               // mesh.materialAssigned[0] = 0;
                //Console.WriteLine("\n\n\n\n\n\nENTCOUNT"+entities.Count+"\n\n\n\n\n\n");
                //Console.WriteLine("\n\n\n\n\n\n"+mesh.materialAssigned[0]+"\n\n\n\n\n\n");
                entities[1].AddComponent(transforma);
                entities[1].AddComponent(mesha);
                entities[1].AddComponent(m3t);


                entities.Add(new Entity());
                ComponentSystem.Mesh3DTrans m3ta = new();
                ComponentSystem.Transform transforms = new ComponentSystem.Transform();
                ComponentSystem.Mesh3D meshs = new Mesh3D();
                transforms.position = new Vector3(-10, 30, 50);
                transforms.scale.X = 6;
                meshs.model = LoadModel("resources/models/test/LightShaft/LightShaft.glb");
               // mesh.materialAssigned = mesh.materialAssigned.Append(1).ToArray();
               // mesh.materialAssigned[0] = 0;
                //Console.WriteLine("\n\n\n\n\n\nENTCOUNT"+entities.Count+"\n\n\n\n\n\n");
                //Console.WriteLine("\n\n\n\n\n\n"+mesh.materialAssigned[0]+"\n\n\n\n\n\n");
                entities[2].AddComponent(transforms);
                entities[2].AddComponent(meshs);
                entities[2].AddComponent(m3ta);

                entities.Add(new Entity());
                
                ComponentSystem.Mesh3DTrans m3tra = new();
                ComponentSystem.Transform transformeis = new ComponentSystem.Transform();
                ComponentSystem.Mesh3D mesher = new Mesh3D();
                transformeis.position = new Vector3(-10, 30, 50);
                transformeis.scale.X = 6;
                mesher.model = LoadModel("resources/models/test/LightShaft/LightShaftVert.glb");      
                //mesher.materialAssignedInMemory[0] = 0;
                Console.WriteLine("\n\n\n\n\n\n"+mesh.materialAssigned.Length+"\n\n\n\n\n\n");
                Console.WriteLine("\n\n\n\n\n\n"+mesh.materialAssigned[0]+"\n\n\n\n\n\n");
                entities[3].AddComponent(transformeis);
                entities[3].AddComponent(mesher);
                entities[3].AddComponent(m3tra);
                               
                mesh = null;
                transform = null;
                mesha = null;
                meshs = null;

               // GC.Collect();

            }
        
        }

        public static int StateId() { return state_id; }
    }
}
