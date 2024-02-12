using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MobManager : MonoBehaviour
{
    private static MobManager instace;
    public static MobManager Instance
    {
        get
        {
            if (instace == null)
                instace = GameObject.FindObjectOfType<MobManager>();
            return Instance;
        }
    }

    public UnityEvent<Mob> OnSpawn, OnDestroy;

    private List<Mob> mobs = new List<Mob>();

    private void Awake()
    {
        instace = this;
    }

    public void OnSpawned(Mob mob)
    {
        mobs.Add(mob);
        OnSpawn?.Invoke(mob);
    }

    public void OnDestroyed(Mob mob)
    {
        if(mobs.Remove(mob))
        {
            OnDestroy?.Invoke(mob);
        }
    }

    public void DestroyAll()
    {
        while(mobs.Count > 0)
        {
            mobs[0]?.Destroy();
        }
    }
}
