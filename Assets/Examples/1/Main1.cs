using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Main1 : MonoBehaviour
{
    [SerializeField]
    ClickEvent ClickEvent;

    [SerializeField]
    ClickEvent ClickEventWithError;

    void Start()
    {
        if (ClickEvent.gameObject.activeSelf)
        {

            ClickEvent.OnButtonClickedCounterStream
                .Subscribe(
                    _ => Debug.Log("Button: Click!"),
                    () => Debug.Log("Button destroyed. Completed.")
                )
                .AddTo(gameObject);
        }

        if (ClickEventWithError.gameObject.activeSelf)
        {

            ClickEventWithError.OnButtonClickedCounterStream
                .Subscribe(
                    _ => Debug.Log("ButtonWithErr: Click!"),
                    ex => Debug.LogError(ex),
                    () => Debug.Log("Button with error destroyed. Completed.")
                )
                .AddTo(gameObject);
        }
    }
}
