using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Gem : MonoBehaviour
{

    public event Action OnCollected;

    public UnityEvent OnCollectedUnitiyEvent;
    private IObjectPool<Gem> pool;
    
    public void SetPool(IObjectPool<Gem> newPool)
    {
        pool = newPool;
    }
    public void Collect()
    {
        OnCollected?.Invoke();
        pool.Release(this);
    }

    private void OnEnable()
    {
        OnCollected += scoreManager.Instance.AddScore;
        OnCollectedUnitiyEvent.AddListener(scoreManager.Instance.AddScore);
    }

    private void OnDisable()
    {
        OnCollected -= scoreManager.Instance.AddScore;
        OnCollectedUnitiyEvent.RemoveListener(scoreManager.Instance.AddScore);
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
