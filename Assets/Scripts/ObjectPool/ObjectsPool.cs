using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool<T> where T : MonoBehaviour
{
    private T _prefab { get; }    
    private Transform _container; 
    private List<T> _pool;
    public bool AutoExpand { get; set; } = false;
    public List<T> Elements => _pool;
    
    public ObjectsPool(T prefab, int count, Transform container)
    {
        _prefab = prefab;
        _container = container;

        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(_prefab, _container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        _pool.Add(createdObject);
        return createdObject;
    }

    public List<T> GetActiveObjects(ref List<T> activeObjects)
    {   
        activeObjects.Clear();

        foreach (var obj in _pool)
        {
            if (obj.gameObject.activeInHierarchy)
            {
                 activeObjects.Add(obj);              
            }            
        }

        return activeObjects;        
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var obj in _pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                element = obj;
                obj.gameObject.SetActive(true);
                return true;
            }            
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
        {
            return element;
        }

        if (AutoExpand)
        {
            return CreateObject(true);
        }

        throw new System.Exception("There is no free elements in pool");
    }
}
