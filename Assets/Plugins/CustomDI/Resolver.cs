using System;
using UnityEngine;

public class Resolver : MonoBehaviour
{
    [SerializeField]
    private Installer _installer;

    private void Awake()
    {
        var attachs = gameObject.GetComponents<Component>();
        foreach (var attach in attachs)
        {
            var type = attach.GetType();
            _installer.Resolve(attach);
        }
    }
}
