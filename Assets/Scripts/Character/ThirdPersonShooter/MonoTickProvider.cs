using System;
using UnityEngine;

public class MonoTickProvider : MonoBehaviour,ITickProvider
{
    public event Action OnTick;
    public float GetDeltaTime()
    {
        return Time.deltaTime;
    }

    private void Update()
    {
        OnTick?.Invoke();
    }
}
