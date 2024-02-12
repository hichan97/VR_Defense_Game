using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Prefabs;

    public bool PlayOnStart = true;
    public float StartFactor = 1f;
    public float AdditiveFactor = 0.1f;
    public float DelayPerSpawngroup = 3f;

    private void Start()
    {
        if (PlayOnStart)
            Play();
    }

    public void Play()
    {
        StartCoroutine(Process());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator Process()
    {
        var factor = StartFactor;
        var wfs = new WaitForSeconds(DelayPerSpawngroup);

        while(true)
        {
            yield return wfs;       //wfs만큼 대기

            yield return StartCoroutine(SpawnProcess(factor)); //SpawnProcess 실행 후 대기 

            factor += AdditiveFactor;
        }
    }

    private IEnumerator SpawnProcess(float factor)
    {
        var count = Random.Range(factor, factor * 2f);
        
        for(int i = 0; i < count; i++)
        {
            Spawn();

            if (Random.value < 0.2f)
                yield return new WaitForSeconds(Random.Range(0.01f, 0.02f));
        }
    }

    private void Spawn()
    {
        Instantiate(Prefabs, transform.position, transform.rotation, transform);
    }    
}
