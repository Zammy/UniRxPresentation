using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Main : MonoBehaviour
{
    [SerializeField]
    ClickEvent ClickEvent;

    [SerializeField]
    ClickEvent ClickEventWithError;

    void Start()
    {
        ClickEvent.OnButtonClickedCounterStream
            .Subscribe(
                _ => Debug.Log("Button: Click!"),
                () => Debug.Log("Button destroyed. Completed.")
            )
            .AddTo(gameObject);


        ClickEventWithError.OnButtonClickedCounterStream
            .Subscribe(
                _ => Debug.Log("ButtonWithErr: Click!"),
                ex => Debug.LogError(ex),
                () => Debug.Log("Button with error destroyed. Completed.")
            )
            .AddTo(gameObject);
    }
}
