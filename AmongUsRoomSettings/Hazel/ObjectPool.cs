﻿using System.Collections.Concurrent;

namespace AmongUsRoomSettings.Hazel;

public sealed class ObjectPool<T> where T : IRecyclable
{
    private int numberCreated;
    public int NumberCreated => this.numberCreated;

    public int NumberInUse => this.inuse.Count;
    public int NumberNotInUse => this.pool.Count;
    public int Size => this.NumberInUse + this.NumberNotInUse;

#if HAZEL_BAG
        private readonly ConcurrentBag<T> pool = new ConcurrentBag<T>();
#else
    private readonly List<T> pool = new List<T>();
#endif

    // Unavailable objects
    private readonly ConcurrentDictionary<T, bool> inuse = new ConcurrentDictionary<T, bool>();

    /// <summary>
    ///     The generator for creating new objects.
    /// </summary>
    /// <returns></returns>
    private readonly Func<T> objectFactory;

    /// <summary>
    ///     Internal constructor for our ObjectPool.
    /// </summary>
    public ObjectPool(Func<T> objectFactory)
    {
        this.objectFactory = objectFactory;
    }

    /// <summary>
    ///     Returns a pooled object of type T, if none are available another is created.
    /// </summary>
    /// <returns>An instance of T.</returns>
    public T GetObject()
    {
#if HAZEL_BAG
            if (!pool.TryTake(out T item))
            {
                Interlocked.Increment(ref numberCreated);
                item = objectFactory.Invoke();
            }
#else
        T item;
        lock (this.pool)
        {
            if (this.pool.Count > 0)
            {
                var idx = this.pool.Count - 1;
                item = this.pool[idx];
                this.pool.RemoveAt(idx);
            }
            else
            {
                Interlocked.Increment(ref numberCreated);
                item = objectFactory.Invoke();
            }
        }
#endif

        if (!inuse.TryAdd(item, true))
        {
            throw new Exception("Duplicate pull " + typeof(T).Name);
        }

        return item;
    }

    /// <summary>
    ///     Returns an object to the pool.
    /// </summary>
    /// <param name="item">The item to return.</param>
    public void PutObject(T item)
    {
        if (inuse.TryRemove(item, out bool b))
        {
#if HAZEL_BAG
                pool.Add(item);
#else
            lock (this.pool)
            {
                pool.Add(item);
            }
#endif
        }
        else
        {
#if DEBUG
            throw new Exception("Duplicate add " + typeof(T).Name);
#endif
        }
    }
}