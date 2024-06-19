using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<Type> : MonoBehaviour where Type : PooledObject
{
    [SerializeField] private Type _object;

    protected Queue<Type> Pool { get; private set; } = new Queue<Type>();
    protected Queue<Type> BusyObjects { get; private set; } = new Queue<Type>();

    public virtual Type Get()
    {
        Type newObject;

        if (Pool.Count == 0)
            newObject = Instantiate(_object);
        else
            newObject = Pool.Dequeue();

        BusyObjects.Enqueue(newObject);

        return newObject;
    }

    public virtual void Release(Type objectToRelease)
    {
        Pool.Enqueue(objectToRelease);
    }

    public virtual void Reset()
    {
        foreach (Type freeObject in Pool)
            Destroy(freeObject.gameObject);

        foreach (Type beingUsedObject in BusyObjects)
            Destroy(beingUsedObject.gameObject);

        Pool.Clear();
        BusyObjects.Clear();
    }
}