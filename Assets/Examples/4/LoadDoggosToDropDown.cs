using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class LoadDoggosToDropDown : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    bool RandomDoggoPLZ;

    void Awake()
    {
        _dropdown = GetComponent<Dropdown>();
    }

    void Start()
    {
        var names = Enum.GetNames(typeof(Doggos));
        var doggosOptions = names.Select(doggo => new Dropdown.OptionData(doggo));
        foreach (var option in doggosOptions)
        {
            _dropdown.options.Add(option);
        }

        if (RandomDoggoPLZ)
        {
            Randomize();
        }

    }

    public void Randomize()
    {
        var names = Enum.GetNames(typeof(Doggos));
        _dropdown.value = UnityEngine.Random.Range(0, names.Length);
    }

    Dropdown _dropdown;
}
