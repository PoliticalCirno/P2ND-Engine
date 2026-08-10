//In the current situation, I need less work of trying to manage a multiple behavior per object
//In which case, I'll be implementing ECS type entity system.
namespace Engine.Game;

class Entity
{
    public int Id { get; set; }

    List<Component> components = new();

     public void AddComponent(Component component)
    {
        components.Add(component);
        component.entity = this;
    }

    public T GetComponent<T>() where T : Component
    {
        foreach(Component component in components)
        {
            if(component.GetType().Equals(typeof(T)))
            {
                return (T)component;
            }
        }
        return null;
    }
}