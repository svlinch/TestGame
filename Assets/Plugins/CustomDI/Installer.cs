using UnityEngine;

public class Installer : MonoBehaviour
{
    protected Container _container;

    public void Resolve(Component component)
    {
        _container.Resolve(component);
    }

    public void Bind(Component component)
    {
        _container.Register(component);
    }

    public void SetContainer(Container container)
    {
        _container = container;
    }
}
