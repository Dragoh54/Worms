using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 offset = new Vector3(0f, 0f, -5f);
    Vector3 velocity = Vector3.zero;

    [SerializeField] float smoothTime = 0.25f;
    [SerializeField] Transform _defaultTarget;

    Transform _currentTarget;

    private void Start()
    {
        _currentTarget = _defaultTarget;
    }

    private void Update()
    {
        if (_currentTarget == null)
        {
            StartCoroutine(WaitCoroutine(2f));
        }

        if (_currentTarget != null)
        {
            Vector3 targetPosition = _currentTarget.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    public void ChangeTarget(GameObject target)
    {
        _currentTarget = target.transform;
    }

    public void ReturnDefaultTarget()
    {
        _currentTarget = _defaultTarget;
    }


    IEnumerator WaitCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        ReturnDefaultTarget();
    }
}
