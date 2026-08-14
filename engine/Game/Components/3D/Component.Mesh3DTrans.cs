using System.Numerics;
using Engine.Game;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace ComponentSystem
{
    class Mesh3DTrans : Component
    {     

        public Mesh3DTrans()
        {
            Mesh3DTransSystem.Register(this);
        }
        public override void Update(float gameTime)
        {        
            //Console.WriteLine(entity.HasComponent<Mesh3D>());
            if(entity.HasComponent<Mesh3D>() == true)
            {
                Mesh3D m3d = entity.GetComponent<Mesh3D>();
                Transform t = entity.GetComponent<Transform>();
                DrawModel(m3d.model, t.position, t.scale.X, Raylib_cs.Color.White);
            }
            else
            {
                
            }
                
        }


    }

}