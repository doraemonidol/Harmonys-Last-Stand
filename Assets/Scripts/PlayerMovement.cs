using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;

    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _dashDistance = 20f;
    [SerializeField] private float _dashDuration = 1f;
    [SerializeField] private float _dashCooldown = 5f;
    [SerializeField] private float _rotationSpeed = 0.5f;
    private bool _canDash = true;
    private bool _isDashing = false;
    private bool _isMousePressed = false;
    private bool _wasMousePressed = false;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private Rigidbody _rigidbody;
    private NavMeshAgent _agent;

    private Vector3 _currentDirection = Vector3.forward;

    void Start()
    {
        GetComponent();
    }

    void GetComponent()
    {
        animator = GetComponent<Animator>();
        Debug.Log("Check ani", animator);
    }

    void Update()
    {
        // Check for mouse press and release
        checkMouse();
        ProcessTranslation();
    }
    void checkMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isMousePressed = true;
            animator.SetBool("isRunning", false);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isMousePressed = false;
        }
    }   

    private void ProcessTranslation()
    {
        //if (_isDashing || _isMousePressed) return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            _currentDirection = CalculateDirection(horizontal, vertical);
            Console.WriteLine(_currentDirection);
            animator.SetBool("isRunning", true);

            transform.Translate(_currentDirection * _moveSpeed * Time.deltaTime, Space.World);

          
            if (!_wasMousePressed) 
            {
                Quaternion targetRotation = Quaternion.LookRotation(_currentDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            // StartCoroutine(Dash());
            Debug.Log("Animation, checkkkkkk", animator);
             animator.SetBool("isRunning", false);
            animator.SetBool("IsDash", true);
           
        }
        else
        {
            animator.SetBool("IsDash", false);
        }

        _wasMousePressed = _isMousePressed; // Track the state of mouse press for the next frame
    }

    private Vector3 CalculateDirection(float horizontal, float vertical)
    {
        if (horizontal > 0 && vertical > 0)
            return new Vector3(1, 0, 1).normalized;
        if (horizontal > 0 && vertical < 0)
            return new Vector3(1, 0, -1).normalized;
        if (horizontal < 0 && vertical > 0)
            return new Vector3(-1, 0, 1).normalized;
        if (horizontal < 0 && vertical < 0)
            return new Vector3(-1, 0, -1).normalized;
        if (horizontal > 0)
            return Vector3.right;
        if (horizontal < 0)
            return Vector3.left;
        if (vertical > 0)
            return Vector3.forward;
        if (vertical < 0)
            return Vector3.back;

        return Vector3.zero;
    }

    private IEnumerator Dash()
    {
        _isDashing = true;
        _canDash = false;
        //animator.SetBool("isDash", true);

        Vector3 dashEndPosition = transform.position + _currentDirection * _dashDistance;

        float dashStartTime = Time.time;
        while (Time.time < dashStartTime + _dashDuration)
        {
            transform.position = Vector3.Lerp(transform.position, dashEndPosition, (Time.time - dashStartTime) / _dashDuration);
            yield return null;
        }

        //animator.SetBool("isDash", false);
        _isDashing = false;
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }
}