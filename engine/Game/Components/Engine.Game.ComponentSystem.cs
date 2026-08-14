
using ComponentSystem;

namespace Engine.Game
{
    class ComponentBaseSystem<T> where T : Component
    {
        protected static List<T> components = new List<T>();

        public static void Register(T component)
        {
            components.Add(component);
            Console.WriteLine($"\n\n\n\n\nINFO: ECS: Registered {typeof(T).Name}. Total count: {components.Count} \n\n\n\n\n");
        
        }

        public static void Update(float gameTime)
        {
            foreach(T component in components)
            {
                component.Update(gameTime);
            }
        }
    }

    class TransformSystem : ComponentBaseSystem<Transform>{ }
    class Mesh3DSystem : ComponentBaseSystem<ComponentSystem.Mesh3D>{ }
    class Mesh3DTransSystem : ComponentBaseSystem<ComponentSystem.Mesh3DTrans>{ }
}