using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMove : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    float Speed = 1;

    static TopDownMove()
    {
        _buttonToDirection = new Dictionary<KeyCode, Vector2>()
        {
            { KeyCode.A, Vector2.left },
            { KeyCode.D, Vector2.right },
            { KeyCode.W, Vector2.up },
            { KeyCode.S, Vector2.down },
        };
    }

    void Update()
    {
        var direction = Vector2.zero;
        foreach (var btnDir in _buttonToDirection)
        {
            if (Input.GetKey(btnDir.Key))
            {
                direction += btnDir.Value;
            }
        }
        direction.Normalize();

        var propPos = StateManager.GetState().PlayerPosition;
        propPos.Value = propPos.Value + (direction * Speed * Time.deltaTime);
    }

    static readonly Dictionary<KeyCode, Vector2> _buttonToDirection;
}
