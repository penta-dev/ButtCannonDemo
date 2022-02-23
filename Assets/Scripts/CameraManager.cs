using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Simon _simon;
    private Vector3 _initialPos;
    void Start()
    {
        _initialPos = gameObject.GetComponent<Transform>().position;
    }

    void Update()
    {
        var pos = _simon.GetComponent<Transform>().position;
        var transform = gameObject.GetComponent<Transform>();
        if (_simon._status == Simon.Status.Flying)
        {
            if (transform.position.x < pos.x)
            {
                transform.position = new Vector3(pos.x, transform.position.y, transform.position.z);
            }
        }
        if (_simon._status == Simon.Status.Ready | _simon._status == Simon.Status.OnGround)
        {
            transform.position = _initialPos;
        }
    }
}
