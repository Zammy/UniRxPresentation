using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SyncPlayerPositionWithState : MonoBehaviour
{
    void Update()
    {
        StateManager.GetState().PlayerPosition
            .Subscribe(pos => transform.localPosition = pos)
            .AddTo(this);
    }
}
