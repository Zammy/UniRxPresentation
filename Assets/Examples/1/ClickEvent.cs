using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;

public class ClickEvent : MonoBehaviour
{
    void Awake()
    {
        _clickSubject = new Subject<Unit>();

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        _clickSubject.OnCompleted();
    }

    public IObservable<Unit> OnButtonClickedCounterStream
    {
        get
        {
            return _clickSubject;
        }
    }

    protected virtual void OnClick()
    {
        _clickSubject.OnNext(Unit.Default);
    }

    protected Subject<Unit> _clickSubject;
}
