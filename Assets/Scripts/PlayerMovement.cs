// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;
// using UnityEngine.Serialization;
//
// public class PlayerMovement : MonoBehaviour
// {
//     [SerializeField] private float _moveSpeed = 10f;
//     [SerializeField] private float _dashDistance = 20f;
//     [SerializeField] private float _dashDuration = 1f;
//     [SerializeField] private float _dashCooldown = 5f;
//     private bool _canDash = true;
//     private bool _isDashing = false;
//     [SerializeField] TrailRenderer _trailRenderer;
//     [SerializeField] private Rigidbody _rigidbody;
//     [SerializeField] private Vector3 _currentDirection = Vector3.forward;
//     private NavMeshAgent _agent;
//     
//     // Start is called before the first frame update
//     void Start()
//     {
//         _rigidbody = GetComponent<Rigidbody>();
//         _trailRenderer = GetComponent<TrailRenderer>();
//         _agent = GetComponent<NavMeshAgent>();
//         _agent.speed = _moveSpeed;
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//         ProcessTranslation();
//     }
//
//     private void ProcessTranslation()
//     {
//         if (_isDashing) return;
//         
//         float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * _moveSpeed;
//         float zValue = Input.GetAxis("Vertical") * Time.deltaTime * _moveSpeed;
//         
//         if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
//         {
//             _currentDirection = new Vector3(0, 0, 0);
//             if (Input.GetAxis("Horizontal") > 0)
//             {
//                 _currentDirection.x = 1;
//             }
//             else if (Input.GetAxis("Horizontal") < 0)
//             {
//                 _currentDirection.x = -1;
//             }
//             if (Input.GetAxis("Vertical") > 0)
//             {
//                 _currentDirection.z = 1;
//             }
//             else if (Input.GetAxis("Vertical") < 0)
//             {
//                 _currentDirection.z = -1;
//             }
//         }
//
//         transform.Translate(xValue, 0, zValue);
//         
//         if (Input.GetButtonDown("Jump"))
//         {
//             if (_canDash)
//             {
//                 StartCoroutine(Dash());
//             }
//         }
//     }
//
//     private IEnumerator Dash()
//     {
//         _isDashing = true;
//         _canDash = false;
//         _trailRenderer.emitting = true;
//         // _rigidbody.useGravity = false;
//         _agent.speed = _dashDistance / _dashDuration;
//         Vector3 dashEndPosition = transform.position + _currentDirection * _dashDistance;
//         dashEndPosition.y -= (float)0.5;
//         Debug.Log(dashEndPosition);
//         _agent.SetDestination(dashEndPosition);
//         yield return new WaitForSeconds(_dashDuration);
//         _agent.speed = _moveSpeed;
//         _agent.ResetPath();
//         _trailRenderer.emitting = false;
//         _isDashing = false;
//         // _rigidbody.useGravity = true;
//         yield return new WaitForSeconds(_dashCooldown);
//         _canDash = true;
//     }
// }
// using System.Collections;
       // using System.Collections.Generic;
       // using UnityEngine;
       // using UnityEngine.AI;
       // using UnityEngine.Serialization;
       //
       // public class PlayerMovement : MonoBehaviour
       // {
       //     [SerializeField] private float _moveSpeed = 10f;
       //     [SerializeField] private float _dashDistance = 20f;
       //     [SerializeField] private float _dashDuration = 1f;
       //     [SerializeField] private float _dashCooldown = 5f;
       //     private bool _canDash = true;
       //     private bool _isDashing = false;
       //     [SerializeField] TrailRenderer _trailRenderer;
       //     [SerializeField] private Rigidbody _rigidbody;
       //     [SerializeField] private Vector3 _currentDirection = Vector3.forward;
       //     private NavMeshAgent _agent;
       //     
       //     // Start is called before the first frame update
       //     void Start()
       //     {
       //         _rigidbody = GetComponent<Rigidbody>();
       //         _trailRenderer = GetComponent<TrailRenderer>();
       //         _agent = GetComponent<NavMeshAgent>();
       //         _agent.speed = _moveSpeed;
       //     }
       //
       //     // Update is called once per frame
       //     void Update()
       //     {
       //         ProcessTranslation();
       //     }
       //
       //     private void ProcessTranslation()
//     {
//         if (_isDashing) return;
//         
//         float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * _moveSpeed;
//         float zValue = Input.GetAxis("Vertical") * Time.deltaTime * _moveSpeed;
//         
//         if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
//         {
//             _currentDirection = new Vector3(0, 0, 0);
//             if (Input.GetAxis("Horizontal") > 0)
//             {
//                 _currentDirection.x = 1;
//             }
//             else if (Input.GetAxis("Horizontal") < 0)
//             {
//                 _currentDirection.x = -1;
//             }
//             if (Input.GetAxis("Vertical") > 0)
//             {
//                 _currentDirection.z = 1;
//             }
//             else if (Input.GetAxis("Vertical") < 0)
//             {
//                 _currentDirection.z = -1;
//             }
//         }
//
//         transform.Translate(xValue, 0, zValue);
//         
//         if (Input.GetButtonDown("Jump"))
//         {
//             if (_canDash)
//             {
//                 StartCoroutine(Dash());
//             }
//         }
//     }
//
//     private IEnumerator Dash()
//     {
//         _isDashing = true;
//         _canDash = false;
//         _trailRenderer.emitting = true;
//         // _rigidbody.useGravity = false;
//         _agent.speed = _dashDistance / _dashDuration;
//         Vector3 dashEndPosition = transform.position + _currentDirection * _dashDistance;
//         dashEndPosition.y -= (float)0.5;
//         Debug.Log(dashEndPosition);
//         _agent.SetDestination(dashEndPosition);
//         yield return new WaitForSeconds(_dashDuration);
//         _agent.speed = _moveSpeed;
//         _agent.ResetPath();
//         _trailRenderer.emitting = false;
//         _isDashing = false;
//         // _rigidbody.useGravity = true;
//         yield return new WaitForSeconds(_dashCooldown);
//         _canDash = true;
//     }
// }
