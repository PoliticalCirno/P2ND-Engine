
using ComponentSystem;

namespace Engine.Game
{
    class ComponentBaseSystem<T> where T : Component
    {
        protected static List<T> components = new List<T>();

        public static void Register(T component)
        {
            components.Add(component);
        }

        public static void Update(float gameTime)
        {
            foreach(T component in components)
            {
                component.Update(gameTime);
            }
        }
    }

    class TransformSyetem : ComponentBaseSystem<Transform>{ }
    class Mesh3DSyetem : ComponentBaseSystem<Mesh3D>{ }
}