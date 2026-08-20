using ComponentSystem;
using static Raylib_cs.Raylib;
using Engine_Scenestates;
using Raylib_cs;
using Engine.Resource;

namespace Engine.Render.MeshSystem
{
    public class MeshQueue
    {
        public Model model;

        public MeshQueue(string modeladdr)
        {
            model = LoadModel(modeladdr);
        }
    }

    public class MeshMemorySystem
    {
        static List<MeshQueue> meshes = new();
        public static void CheckActiveMeshesInScene()
        {
            for(int i = 0; i < Scenestate.entities.Count; i++)
            {
                if(Scenestate.entities[i].HasComponent<Mesh3D>() == false) 
                continue;

                var meshGet = Scenestate.entities[i].GetComponent<Mesh3D>();
                var meshAddress = Game.Resource.Load.IO.Assets.models;
                
                if(meshGet.mdlLoaded == true)
                continue;

                if(meshAddress[meshGet.meshAssigned].isLoaded == true)
                {
                    meshGet.model = meshes[meshAddress[meshGet.meshAssigned].locationInMem].model;
                }
                else
                {
                    meshes.Add(new MeshQueue(meshAddress[meshGet.meshAssigned].MdlQ));
                    meshAddress[meshGet.meshAssigned].locationInMem = meshes.Count - 1;
                    meshAddress[meshGet.meshAssigned].isLoaded = true;
                    meshGet.model = meshes[meshAddress[meshGet.meshAssigned].locationInMem].model;
                }
            }
        }
    }   
}