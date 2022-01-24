using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class Explosion : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    int Damage = 5;

    void Start()
    {
        gameObject.AddComponent<ObservableTrigger2DTrigger>()
            .OnTriggerStay2DAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(1))
            .Subscribe(other =>
            {
                if (other.gameObject.tag == "Player")
                {
                    StateManager.GetState()
                        .PlayerHealth.Value -= Damage;
                }
            }).AddTo(this);
    }
}
