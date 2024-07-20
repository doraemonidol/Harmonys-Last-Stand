using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MockUp;
using Common;
using UnityEngine.AI;

namespace Presentation.Maestro
{
    public class PIllusionCarnival : MaestroSkill
    {
        [SerializeField] private float radius = 2;
        [SerializeField] private float speed;
        [SerializeField] private GameObject prefab;
        [SerializeField] private List<Renderer> renderers;
        private List<GameObject> _illusions = new List<GameObject>();
        private float currentAngle;

        [SerializeField] private RotateToMouseScript rotateToMouseScript;

        public override IEnumerator StartCasting()
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                ["preCastVfx"] = preCastVfx
            };
            state = SkillState.Casting;
            animator.SetTrigger(EnemyActionType.CastSpell1);
            yield return new WaitForSeconds(1.8f);
            StartCoroutine(StartPrecastVFX());
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].enabled = false;
            }

            Quaternion targetAngle = rotateToMouseScript.GetRotation();
            for (int i = 0; i < 5; i++)
            {
                float angle = i * Mathf.PI * 2 / 5 + targetAngle.eulerAngles.y;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;
                GameObject illusion = Instantiate(prefab, new Vector3(x, 0, z) + target.transform.position , Quaternion.identity);
                _illusions.Add(illusion);
            }
            
            // navMeshAgent.transform.position = _illusions[1].transform.position;
            //
            // Destroy(_illusions[1]);
            // _illusions.RemoveAt(1);
            
            // oldSpeed = navMeshAgent.speed;
            for (int i = 0; i < _illusions.Count; i++)
            {
                NavMeshAgent agent = _illusions[i].GetComponent<NavMeshAgent>();
                // agent.speed = speed;
                // agent.acceleration = navMeshAgent.acceleration;
                // agent.angularSpeed = navMeshAgent.angularSpeed;
                // agent.stoppingDistance = navMeshAgent.stoppingDistance;
                
                StartCoroutine(_illusions[i].GetComponent<IllusionController>().ShowIllusion(data));
            }
            
            // StartCoroutine(StartPrecastVFX());
            yield return new WaitForSeconds(0.3f);
            // for (int i = 0; i < renderers.Count; i++)
            // {
            //     renderers[i].enabled = true;
            // }
            
            navMeshAgent.isStopped = false;

            // yield return new WaitForSeconds(1f);

        }

        public override IEnumerator StartHitting()
        {
            throw new System.NotImplementedException();
        }


        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            // rotateToMouseScript = target.GetComponent<RotateToMouseScript>();
        }
        
        // Update is called once per frame
        public override void Update()
        {
            if (state == SkillState.Casting)
            {
                var navMeshAgent0 = _illusions[0].GetComponent<NavMeshAgent>();
                if (!navMeshAgent0.pathPending)
                {
                    // Debug.Log("Maestro Path is not pending");
                    // Debug.Log(navMeshAgent.remainingDistance + " " + navMeshAgent.stoppingDistance);
                    if (navMeshAgent0.remainingDistance <= navMeshAgent0.stoppingDistance)
                    {
                        // Debug.Log("Maestro Stopping distance");
                        // if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                        {
                            var center = target.transform.position;
                            var angle = 4 * Mathf.PI * Time.deltaTime;

                            // move navMeshAgent of illusion around target a radius of radius an angle of 0.1 * Mathf.PI
                            for (int i = 0; i < _illusions.Count; i++)
                            {
                                var currentPosition = _illusions[i].transform.position;
                                var x = (currentPosition.x - center.x) * Mathf.Cos(angle) -
                                    (currentPosition.z - center.z) * Mathf.Sin(angle) + center.x;
                                var z = (currentPosition.x - center.x) * Mathf.Sin(angle) +
                                    (currentPosition.z - center.z) * Mathf.Cos(angle) + center.z;
                                _illusions[i].GetComponent<NavMeshAgent>().SetDestination(new Vector3(x, 0, z));
                            }


                        }

                    }
                }
            }
            else
            {
                // navMeshAgent.isStopped = false;
            }
        }
    }
}
