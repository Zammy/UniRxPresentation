using UnityEngine;
using UniRx;

[RequireComponent(typeof(Rigidbody2D))]
public class SyncPlayerPositionWithState : MonoBehaviour
{
    void Start()
    {
        _body = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        StateManager.GetState().PlayerPosition
            .Subscribe(pos => transform.localPosition = pos)
            .AddTo(this);
    }

    Rigidbody2D _body;
}
