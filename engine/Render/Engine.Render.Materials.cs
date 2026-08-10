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

        public MaterialsQueue(string Alb, string norm, string mra, string shadr, float aInt, float rouInt, float ambInt)
        {
            this.albedo = LoadTexture(Alb);
            this.normal = LoadTexture(norm);
            this.mrao = LoadTexture(mra);
            if(shadr == "PBR")
            {
                this.shader = Shadercl.Mat_PBR;
            }
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
                for(int n = 0; n < materialref.materialAssigned.Length; n++)
                {
                    if(materialref.materialAssignedInMemory.Length == 0)
                    {
                        var Loadqueue = Engine.Game.Resource.Load.IO.Assets.mat[materialref.materialAssigned[n]];
                        materials.Add(new MaterialsQueue(Loadqueue.Albedo, Loadqueue.Normal, Loadqueue.Mrao, Loadqueue.Shader, Loadqueue.AlbedoIntensity, Loadqueue.RoughnessIntensity, Loadqueue.AmbientIntensity));
                        materialref.materialAssignedInMemory[n] = materials.Count;
                    }
                    else
                    {
                        materialref.model.Materials[n].Shader = materials[materialref.materialAssignedInMemory[n]].shader;
                        materialref.model.Materials[n].Maps[(int)MaterialMapIndex.Albedo].Texture = materials[materialref.materialAssignedInMemory[n]].albedo;
                        materialref.model.Materials[n].Maps[(int)MaterialMapIndex.Normal].Texture = materials[materialref.materialAssignedInMemory[n]].normal;
                        materialref.model.Materials[n].Maps[(int)MaterialMapIndex.Metalness].Texture = materials[materialref.materialAssignedInMemory[n]].mrao;
            
                    }
                }
            }
        }        
    }
}