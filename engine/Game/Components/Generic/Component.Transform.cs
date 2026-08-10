using System.Numerics;
using Engine.Game;

namespace ComponentSystem
{
    class Transform : Component
    {
        public Vector3 position = Vector3.Zero;
        public Vector3 rotation = Vector3.Zero;
        public Vector3 scale = new Vector3(1, 1, 1);
    }

}