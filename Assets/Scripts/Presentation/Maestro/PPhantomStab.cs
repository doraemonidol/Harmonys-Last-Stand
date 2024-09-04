using System.Collections;
using System.Collections.Generic;
using Common;
using DTO;
using Logic.Facade;
using MockUp;
using UnityEngine;

namespace Presentation.Maestro
{
    public class PPhantomStab : MaestroSkill
    {
        [SerializeField] private List<Renderer> renderers;
        private float currentAngle;
        [SerializeField] private float dangerRange;
        [SerializeField] private float speed;
        private float oldSpeed;
        private Vector3 initialPosition;
        
        public override void Update()
        {
            if (state == SkillState.Casting)
            {
                if (Time.time > endCastingTime)
                {
                    state = SkillState.Idle;
                    for (int i = 0; i < renderers.Count; i++)
                    {
                        renderers[i].enabled = true;
                    }
                }
                
                if (Vector3.Distance(target.transform.position, navMeshAgent.transform.position) < attackRange)
                {
                    state = SkillState.PostCasting;
                    StartCoroutine(StartHitting());
                } else {
                    if (currentAngle > 0)
                    {
                        // Debug.Log("Maestro Moving to target");
                        // check whether navMeshAgent arrived at the target
                        if (!navMeshAgent.pathPending)
                        {
                            // Debug.Log("Maestro Path is not pending");
                            // Debug.Log(navMeshAgent.remainingDistance + " " + navMeshAgent.stoppingDistance);
                            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + 6)
                            {
                                // Debug.Log("Maestro Stopping distance");
                                // if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                                {
                                    // Debug.Log("Maestro Stopped");
                                    currentAngle -= speed * Time.deltaTime;
                                    // Debug.Log(currentAngle);
                                    var targetPosition = target.transform.position;
                                    var position = initialPosition;
                                    // Debug.Log(position);
                                    var angle = Vector3.Angle(position - targetPosition, Vector3.right);

                                    var distance = Vector3.Distance(targetPosition, position);

                                    var newR = distance * (currentAngle / (2 * Mathf.PI));
                                    // Debug.Log("New R: " + newR);
                                    // Find the new point that is distance away from the target position and rotated by (2 * Mathf.PI - currentAngle)
                                    Vector3 newPosition =
                                        newR * new Vector3(Mathf.Cos(currentAngle), 0, Mathf.Sin(currentAngle)) +
                                        targetPosition;

                                    //
                                    // Vector3 newPosition = 
                                    //     distance * (currentAngle / (2 * Mathf.PI)) * 
                                    //     new Vector3(Mathf.Cos(currentAngle), 0, Mathf.Sin(currentAngle)) + targetPosition;
                                    //
                                    // // Rotate newPosition around the position -angle
                                    // var x = newPosition.x - targetPosition.x;
                                    // var z = newPosition.z - targetPosition.z;
                                    // newPosition.x = - x * Mathf.Cos(angle) + z * Mathf.Sin(angle) + targetPosition.x;
                                    // newPosition.z = - x * Mathf.Sin(angle) - z * Mathf.Cos(angle) + targetPosition.z;
                                    //
                                    // Instantiate a sphere at newPosition
                                    // GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                                    // sphere.transform.position = newPosition;
                                    // sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);   


                                    navMeshAgent.SetDestination(newPosition);
                                }
                            }
                        }

                    }
                    else
                    {
                        navMeshAgent.SetDestination(target.transform.position);
                        navMeshAgent.isStopped = false;
                        // navMeshAgent.speed = oldSpeed;
                        // state = SkillState.Idle;
                    }
                }
            }
        }

        public override IEnumerator StartHitting()
        {
            Debug.Log("Maestro Phantom Stab hitting");
            navMeshAgent.SetDestination(target.transform.position);
            // navMeshAgent.isStopped = true;
            navMeshAgent.speed = oldSpeed;
            
            StartCoroutine(StartPrecastVFX());
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].enabled = true;
            }
            
            // animator.SetTrigger(EnemyActionType.SpecialAttack);
            yield return new WaitForSeconds(1.2f);
            
            // Debug.Log("Maestro Phantom Stab hit distance: " + Vector3.Distance(target.transform.position, navMeshAgent.transform.position));
            
            if (Vector3.Distance(target.transform.position, navMeshAgent.transform.position) < dangerRange)
            {
                // Send villain result
                var eventd = new EventDto
                {
                    Event = "GET_ATTACKED",
                    ["attacker"] = MaestroLogicHandle,
                    ["target"] = target.GetComponent<AureliaMockUp>().LogicHandle,
                    ["context"] = null,
                    ["skill"] = this.LogicHandle
                };
                
                LogicLayer.GetInstance().Observe(eventd);
            }
            
            state = SkillState.Idle;
        }
        
        public override IEnumerator StartCasting()
        {
            oldSpeed = navMeshAgent.speed;
            initialPosition = navMeshAgent.transform.position;
            
            currentAngle = 2 * Mathf.PI;
            animator.SetTrigger(EnemyActionType.CastSpell1);
            yield return new WaitForSeconds(1.8f);
            StartCoroutine(StartPrecastVFX());
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].enabled = false;
            }
            navMeshAgent.SetDestination(navMeshAgent.transform.position);
            
            navMeshAgent.speed = speed;
            navMeshAgent.isStopped = false;
            state = SkillState.Casting;
            endCastingTime = Time.time + timeout;
        }
    }
}