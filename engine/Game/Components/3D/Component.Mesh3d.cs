using System.Numerics;
using Engine.Game;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace ComponentSystem
{
    class Mesh3D : Component
    {
        public Model model;
        public int[] meshAssigned = {0x000000}; //On second thought, it'd be better if LOD was its own component
        public int[] meshAssignedInMemory = {0x000000};
        public int[] materialAssigned = {0x000000};
        public int[] materialAssignedInMemory = {0x000000};

        public virtual void Update(float gameTime)
        {
                Transform t = entity.GetComponent<Transform>();
                DrawModel(model, t.position, t.scale.X, Raylib_cs.Color.White);
        }

        public Mesh3D()
        {
            Mesh3DSyetem.Register(this);
        }

    }

}