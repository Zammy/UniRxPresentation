using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class SmartImage : MonoBehaviour
{
    void Awake()
    {
        _image = GetComponent<RawImage>();
    }

    void Start()
    {
        _startWidth = _image.rectTransform.sizeDelta.x;
    }

    public void SetImageData(Texture2D tex2d)
    {
        _image.texture = tex2d;
        _image.SetNativeSize();
        var size = _image.rectTransform.sizeDelta;
        if (size.x > _startWidth)
        {
            var ratio = _startWidth / size.x;
            size.x *= ratio;
            size.y *= ratio;
            _image.rectTransform.sizeDelta = size;
        }
    }


    RawImage _image;
    float _startWidth;
}
