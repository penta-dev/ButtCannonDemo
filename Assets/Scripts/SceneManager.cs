using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private Raptor _raptor;
    [SerializeField] private Simon _simon;

    private bool _isRaptorHeadTouched;
    private Vector2 _touchPos;
    private Vector2 _force;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Handle native touch events
        foreach (Touch touch in Input.touches)
        {
            //print("touch received");
            HandleTouch(touch.fingerId, touch.position, touch.phase);
        }
        // Simulate touch events from mouse events
        if (Input.touchCount == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleTouch(10, Input.mousePosition, TouchPhase.Began);
            }
            if (Input.GetMouseButton(0))
            {
                HandleTouch(10, Input.mousePosition, TouchPhase.Moved);
            }
            if (Input.GetMouseButtonUp(0))
            {
                HandleTouch(10, Input.mousePosition, TouchPhase.Ended);
            }
        }
    }
    private void HandleTouch(int touchFingerId, Vector3 pos, TouchPhase phase)
    {
        if (phase == TouchPhase.Began)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            if (_raptor.CheckHeadTouch(mousePos2D))
            {
                _touchPos = mousePos2D;
                _isRaptorHeadTouched = true;

                Debug.Log("Raptor head touched!, pos = " + _touchPos);
            }
            else
            {
                _isRaptorHeadTouched = false;
            }
        }
        if (phase == TouchPhase.Moved)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            if(_isRaptorHeadTouched)
            {
                float dx = mousePos2D.x - _touchPos.x;
                float dy = mousePos2D.y - _touchPos.y;
                float angle = GetDegree(dx, dy) - 180;
                float force = (float)Math.Sqrt(dx * dx + dy * dy);

                Debug.Log("force =" + force + ", angle = " + angle);

                _force = mousePos2D - _touchPos;
                _raptor.ApplyForce(force);
                //_raptor.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, angle);
            }
        }
        if (phase == TouchPhase.Ended)
        {
            if(_isRaptorHeadTouched)
            {
                _raptor.ShowShotAnimation();
                _simon.AddVelocity(-_force);
            }
        }
    }
    private float GetDegree(float x, float y)
    {
        float value = (float)((Mathf.Atan2(x, -y) / Math.PI) * 180f);
        if (value < 0) value += 360f;
        value -= 90;
        return value;
    }
}
