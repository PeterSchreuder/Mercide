using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [SerializeField]
    private bool startOnTarget = true;

    [SerializeField]
    private Transform targetPosition;

    [SerializeField]
    private float followSpeed = 0.05f;

    private void Start()
    {
        if (startOnTarget)
            FlyToPosition(transform.position, targetPosition.position, 60f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 _position1 = transform.position;
        Vector3 _position2 = targetPosition.position;

        FlyToPosition(_position1, _position2, followSpeed);
    }

    /// <summary>
    /// Flies this object from position A to B
    /// </summary>
    /// <param name="_position1">Position A</param>
    /// <param name="_position2">Position B</param>
    /// <param name="_followSpeed">Speed</param>
    void FlyToPosition(Vector3 _position1, Vector3 _position2, float _followSpeed)
    {
        transform.position = Vector3.Lerp(_position1, _position2, _followSpeed);
    }
}
