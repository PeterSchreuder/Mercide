using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [SerializeField]
    private bool startOnTarget = true;

    [SerializeField]
    private Transform targetPosition = null;

    [SerializeField]
    private float followSpeedX = 2f, followSpeedY = 1f;

    private void Start()
    {
        if (startOnTarget)
            FlyToPosition(transform.position, targetPosition.position, 60f, 60f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 _position1 = transform.position;
        Vector3 _position2 = targetPosition.position;

        FlyToPosition(_position1, _position2, followSpeedX * Time.deltaTime, followSpeedY * Time.deltaTime);
    }

    /// <summary>
    /// Flies this object from position A to B
    /// </summary>
    /// <param name="_position1">Position A</param>
    /// <param name="_position2">Position B</param>
    /// <param name="_followSpeed">Speed</param>
    void FlyToPosition(Vector3 _position1, Vector3 _position2, float _followSpeedX, float _followSpeedY)
    {
        transform.position = transform.position.ChangeX(Mathf.Lerp(_position1.x, _position2.x, _followSpeedX));
        transform.position = transform.position.ChangeY(Mathf.Lerp(_position1.y, _position2.y, _followSpeedY));
    }
}
