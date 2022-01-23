using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Main4 : MonoBehaviour
{
    [Header("Set in Unity")]
    [SerializeField]
    SmartImage[] Images;

    [SerializeField]
    Dropdown[] Dropdowns;

    [SerializeField]
    Button RequestButton;

    [SerializeField]
    Button RandomizeButton;

    void Start()
    {
        // var throttledClick = Observable.ThrottleFirst(RequestButton.OnClickAsObservable(), TimeSpan.FromSeconds(1));
        RequestButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                var requests = Dropdowns
                    .Select(dropdown => (Doggos)dropdown.value)
                    .Select(doggo => DogCeoService.GetRandomDogTexture(doggo));
                Observable.WhenAll(requests)
                    .Subscribe(textures =>
                    {
                        for (int i = 0; i < textures.Length; i++)
                        {
                            Images[i].SetImageData(textures[i]);
                        }
                    })
                    .AddTo(this);
            })
            .AddTo(this);

        RandomizeButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                foreach (var dropdown in Dropdowns)
                {
                    dropdown.GetComponent<LoadDoggosToDropDown>().Randomize();
                }
            })
            .AddTo(this);
    }
}
