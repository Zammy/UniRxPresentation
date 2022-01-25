using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateTowardMouse : MonoBehaviour
{
    void Awake()
    {
        _groundPlane = new Plane(Vector3.back, Vector3.zero);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDistance;
        if (_groundPlane.Raycast(ray, out hitDistance))
        {
            var hitPoint = ray.GetPoint(hitDistance);
            var direction = hitPoint - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    Plane _groundPlane;
}
