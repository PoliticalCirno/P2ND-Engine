using System.Numerics;
using Engine.Game;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace ComponentSystem
{
    class Mesh3D : Component
    {
    public Model model;
    public int[] meshAssigned = Array.Empty<int>();
    public int[] meshAssignedInMemory = Array.Empty<int>();
    public int[] materialAssigned = Array.Empty<int>();
    public int[] materialAssignedInMemory = Array.Empty<int>();
    
        public Mesh3D()
        {
            Mesh3DSystem.Register(this);
        }

        
        public override void Update(float gameTime)
        {
            if(entity.HasComponent<Mesh3DTrans>() == false)
            {            
                Transform t = entity.GetComponent<Transform>();
                DrawModel(model, t.position, t.scale.X, Raylib_cs.Color.White);
                //Console.WriteLine($"\n\n\n\n\nI SHOULD BE WORKING \n\n\n\n\n");
            }
        }


    }

}