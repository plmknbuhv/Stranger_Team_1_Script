using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field:SerializeField] public InputReader PlayerInput { get; private set; }
    
    private Dictionary<Type, IPlayerComponent> _components;

    private void Awake()
    {
        _components = new Dictionary<Type, IPlayerComponent>();
        
        GetComponentsInChildren<IPlayerComponent>().ToList()
            .ForEach(compo => _components.Add(compo.GetType(), compo));
        
        _components.Add(PlayerInput.GetType(), PlayerInput);
        
        _components.Values.ToList().ForEach(compo => compo.Initialize(this));
    }

    public T GetCompo<T>() where T : class
    {
        Type componentType = typeof(T);
        if (_components.TryGetValue(componentType, out IPlayerComponent component))
        {
            return component as T;
        }

        return default;
    }
}
