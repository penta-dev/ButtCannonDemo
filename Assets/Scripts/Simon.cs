using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simon : MonoBehaviour
{
    public enum Status { Ready, Flying, Dead, OnGround };
    public Status _status = Status.Ready;

    private Vector2 _velocity;

    private Vector3 _initialPos;
    private Quaternion _initialEuler;

    [SerializeField] private float _gravity;
    [SerializeField] private float _rotationSpeed;

    void Start()
    {
        _initialPos = GetComponent<Transform>().position;
        _initialEuler = GetComponent<Transform>().rotation;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_status == Status.Flying | _status == Status.Dead)
        {
            float dt = Time.deltaTime;
            float x0 = GetComponent<Transform>().position.x;
            float y0 = GetComponent<Transform>().position.y;
            _velocity.y += -0.5f * _gravity * dt;

            float x = x0 + _velocity.x * dt;
            float y = y0 + _velocity.y * dt;

            GetComponent<Transform>().position = new Vector3(x, y, 0);

            if(y < -3)
            {
                _status = Status.OnGround;
            }
        }
        if(_status == Status.Flying)
        {
            GetComponent<Transform>().Rotate(new Vector3(0, 0, 1), _rotationSpeed * Time.deltaTime);
        }
    }
    public void AddVelocity(Vector2 velocity)
    {
        if (_status == Status.Ready | _status == Status.OnGround)
        {
            GetComponent<Transform>().position = _initialPos;
            GetComponent<Transform>().rotation = _initialEuler;
            gameObject.SetActive(true);

            _velocity = velocity * 3;
            _status = Status.Flying;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (_status == Status.Flying)
            {
                _velocity = Vector2.zero;
                _status = Status.Dead;
                Debug.Log("Detect collision with obstacle.");
            }
        }
        if (collision.gameObject.CompareTag("Boost"))
        {
            if (_status == Status.Flying)
            {
                _velocity.y = 6;
                Debug.Log("Detected collision with boost.");
            }
        }
    }
}
