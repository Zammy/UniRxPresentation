using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Main2 : MonoBehaviour
{
    [Header("Set in Unity")]
    [SerializeField]
    GameObject ViewPresenter;

    void Start()
    {
        var viewPresenter = ViewPresenter.GetComponent<IViewPresenter>();

        viewPresenter.OnClick()
            .Subscribe(_ => Debug.Log("Button clicked"));

        viewPresenter.OnInputFieldChanged()
            .Subscribe(text => Debug.LogFormat("InputField changed: {0}", text));

        viewPresenter.OnToggleChanged()
            .Subscribe(isOn => Debug.LogFormat("Toggle changed: {0}", isOn));

        viewPresenter.OnDropdownChanged()
            .Subscribe(newValue => Debug.LogFormat("Dropdown changed:{0}", newValue));
    }
}
