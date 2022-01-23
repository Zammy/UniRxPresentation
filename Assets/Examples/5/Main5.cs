using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Main5 : MonoBehaviour
{
    [Header("Set in Unity")]
    [SerializeField]
    Button LoadButton;
    [SerializeField]
    Button SaveButton;

    void Start()
    {
        LoadButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                StateManager.Instance.LoadState()
                    .SubscribeOnMainThread()
                    .Subscribe(_ =>
                    {
                        Debug.Log("Loaded state");
                    })
                    .AddTo(this);
            })
            .AddTo(this);

        SaveButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                StateManager.Instance.SaveState()
                    .SubscribeOnMainThread()
                    .Subscribe(_ =>
                    {
                        Debug.Log("Saved state");
                    })
                    .AddTo(this);
            })
            .AddTo(this);

        StateManager.GetState()
            .PlayerHealth
            .Subscribe(playerHealth => Debug.LogFormat("Player health: {0}", playerHealth))
            .AddTo(this);

        StateManager.GetState()
            .PlayerPositiong
            .Subscribe(playerPos => Debug.LogFormat("Player position: {0}", playerPos))
            .AddTo(this);
    }

}
