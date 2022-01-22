using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Main3 : MonoBehaviour
{
    [Header("Set in Unity")]
    [SerializeField]
    Button Button;

    void Start()
    {
        Button.OnClickAsObservable()
            .Buffer(TimeSpan.FromMilliseconds(500))
            .Select(clicks => clicks.Count)
            .Where(clicks => clicks >= 2)
            .Subscribe(_ => Debug.Log("Double click!"))
            .AddTo(gameObject);
    }
}
