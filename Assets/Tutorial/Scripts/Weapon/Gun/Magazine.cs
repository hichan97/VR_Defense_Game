using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Magazine : MonoBehaviour
{
    public int maxBullets = 20;
    public float chargintTime = 2f;

    private int currentBullets;
    private int CurrentBullets
    {
        get => currentBullets;
        set
        {
            if (value < 0)
                currentBullets = 0;
            else if (value > maxBullets)
                currentBullets = maxBullets;
            else
                currentBullets = value;

            OnbulletsChanged?.Invoke(currentBullets);
            OnChargeChanged?.Invoke((float)currentBullets / maxBullets);
        }
    }
    public UnityEvent OnReloadStart;
    public UnityEvent OnreloadEnd;

    public UnityEvent<int> OnbulletsChanged;
    public UnityEvent<float> OnChargeChanged;

    private void Start()
    {
        CurrentBullets = maxBullets;   
    }

    public bool Use(int amount = 1)
    {
        if (CurrentBullets >= amount)
        {
            CurrentBullets -= amount;
            return true;
        }
        else
            return false;
    }
    
    public void StartReload()
    {
        if (currentBullets == maxBullets)
            return;

        StopAllCoroutines();
        StartCoroutine(ReloadProcess());
    }

    public void StopReload()
    {
        StopAllCoroutines();
    }

    private IEnumerator ReloadProcess()
    {
        OnReloadStart?.Invoke();

        var beginTime = Time.time;
        var beginBullets = currentBullets;
        var enoughPercent = 1f;
        var enoughCharginTime = chargintTime * enoughPercent;

        while (true)
        {
            var t = (Time.time - beginTime) / enoughCharginTime;
            if (t >= 1f)
                break;

            currentBullets = (int)Mathf.Lerp(beginBullets, maxBullets, t);

            yield return null;
        }

        currentBullets = maxBullets; 
        OnreloadEnd?.Invoke();
    
    }

}
