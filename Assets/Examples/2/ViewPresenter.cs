using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ViewPresenter : MonoBehaviour, IViewPresenter
{
    [Header("Set in Unity")]
    [SerializeField]
    Button Button;

    [SerializeField]
    InputField InputField;

    [SerializeField]
    Toggle Toggle;

    [SerializeField]
    Dropdown Dropdown;


    public IObservable<Unit> OnClick()
    {
        return Button.OnClickAsObservable();
    }

    public IObservable<string> OnInputFieldChanged()
    {
        return InputField.onEndEdit.AsObservable<string>();
    }

    public IObservable<bool> OnToggleChanged()
    {
        return Toggle.onValueChanged.AsObservable<bool>();
    }

    public IObservable<int> OnDropdownChanged()
    {
        return Dropdown.onValueChanged.AsObservable<int>();
    }
}

public interface IViewPresenter
{
    IObservable<Unit> OnClick();
    IObservable<string> OnInputFieldChanged();
    IObservable<bool> OnToggleChanged();
    IObservable<int> OnDropdownChanged();
}
