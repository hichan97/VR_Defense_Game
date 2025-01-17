using UnityEngine;
using UnityEngine.Events;

public class Mob : MonoBehaviour
{
    public UnityEvent OnCreated;
    public UnityEvent OnDestroyed;

    private bool isDestroyed;

    public float DestroyDelay = 1f;

    private void Start()
    {

        OnCreated?.Invoke();
    }

    public void Destroy()
    {
        if (isDestroyed)
            return;
        isDestroyed = true;

        Destroy(gameObject, DestroyDelay);

        OnDestroyed?.Invoke();
    }
}
