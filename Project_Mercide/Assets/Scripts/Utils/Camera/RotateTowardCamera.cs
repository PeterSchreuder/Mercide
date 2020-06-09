using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardCamera : MonoBehaviour
{
    private Transform _target;
    private Vector3 _direction;

    private void Start()
    {
        _target = Camera.main.transform;
        RotateTowardTarget();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateTowardTarget();
    }

    private void RotateTowardTarget()
    {
        _direction = Vector3.RotateTowards(transform.position, _target.position, 1f, 1f).AddZ(180f);
        transform.rotation = Quaternion.LookRotation(_direction);
    }
}
