using ComponentSystem;
using static Raylib_cs.Raylib;
using Engine_Scenestates;
using Raylib_cs;
using Engine.Resource;
namespace Engine.Render.MaterialSystem
{
    class MaterialsQueue
    {
        public static int preloadedCheck = 0x000000;
        public int ActiveMaterials = 0;
        public Texture2D albedo;
        public Shader shader;
        public Texture2D normal;
        public Texture2D mrao;
        public float AlbedoIntensity;
        public float RoughnessIntensity;
        public float AmbientIntensity;
        public float EmissionIntensity;
        public string test;

        public MaterialsQueue(string Alb, string norm, string mra, string shadr, float aInt, float rouInt, float ambInt, string testcheck)
        {
            this.albedo = LoadTexture(Alb);
            this.normal = LoadTexture(norm);
            this.mrao = LoadTexture(mra);
        this.shader = Shadercl.Mat_PBR;
            
            test = testcheck;
        }

    }

    class MaterialMemorySystem
    {
        public static List<MaterialsQueue> materials = new();
        public static unsafe void checkActiveMaterialInScene()
        {
            for(int i = 0; i < Scenestate.entities.Count; i++)
            {
                var materialref = Scenestate.entities[i].GetComponent<Mesh3D>();
                if(materialref == null || materialref.materialAssigned == null)
                continue;

                for(int n = 0; n < materialref.materialAssigned.Length; n++)
                {
                    if(materialref.materialAssignedInMemory.Length == 0)
                    {
                        Console.WriteLine("\n\n\n\n\n\n\n" + $"CurrentVal: {materialref.materialAssigned[n]}\n\n\n\n\n\n");
                        var Loadqueue = Engine.Game.Resource.Load.IO.Assets.mat[materialref.materialAssigned[n]];
                        materials.AddRange(new MaterialsQueue(Loadqueue.Albedo, Loadqueue.Normal, Loadqueue.Mrao, Loadqueue.Shader, Loadqueue.AlbedoIntensity, Loadqueue.RoughnessIntensity, Loadqueue.AmbientIntensity, "checking"));
                        materialref.materialAssignedInMemory = materialref.materialAssignedInMemory.Append(1).ToArray();                        
                        materialref.materialAssignedInMemory[n] = materials.Count - 1;
                        Console.WriteLine("\n\n\n\n\n\n\n" + $"CurrentCount: {materialref.model.MaterialCount} {n + 1} {materials.Count} {materialref.materialAssignedInMemory.Length} {materials[0].test}\n\n\n\n\n\n");
                        materialref.model.Materials[n + 1].Shader = materials[0].shader;
                        materialref.model.Materials[n + 1].Maps[(int)MaterialMapIndex.Albedo].Color = Raylib_cs.Color.White;
                        materialref.model.Materials[n + 1].Maps[(int)MaterialMapIndex.Albedo].Texture = materials[materialref.materialAssignedInMemory[n]].albedo;
                        materialref.model.Materials[n + 1].Maps[(int)MaterialMapIndex.Normal].Texture = materials[materialref.materialAssignedInMemory[n]].normal;
                        materialref.model.Materials[n + 1].Maps[(int)MaterialMapIndex.Metalness].Texture = materials[materialref.materialAssignedInMemory[n]].mrao;
                         Console.WriteLine("------------------------------------------RenderSuccess");


                    }
                    else
                    {
                        materialref.model.Materials[n + 1].Shader = materials[materialref.materialAssignedInMemory[n]].shader;
                        materialref.model.Materials[n + 1].Maps[(int)MaterialMapIndex.Albedo].Texture = materials[materialref.materialAssignedInMemory[n]].albedo;
                        materialref.model.Materials[n + 1].Maps[(int)MaterialMapIndex.Normal].Texture = materials[materialref.materialAssignedInMemory[n]].normal;
                        materialref.model.Materials[n + 1].Maps[(int)MaterialMapIndex.Metalness].Texture = materials[materialref.materialAssignedInMemory[n]].mrao;
            
                    }
                }
            }
        }        
    }
}