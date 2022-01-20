using UnityEngine;

public class ClickEventWithError : ClickEvent
{
    protected override void OnClick()
    {
        _clickCounter++;
        if (_clickCounter > 10)
        {
            _clickSubject.OnError(new UnityException("An exception"));
        }
        else
        {
            base.OnClick();
        }
    }

    int _clickCounter = 0;
}
