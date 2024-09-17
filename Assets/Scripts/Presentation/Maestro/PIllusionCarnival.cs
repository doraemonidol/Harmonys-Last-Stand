using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MockUp;
using Common;
using DTO;
using Logic.Facade;
using UnityEngine.AI;
using UnityEngine.Serialization;

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
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject hitPrefab;

        [SerializeField] private RotateToMouseScript rotateToMouseScript;
        private bool _playerSuccessful = false;

        public override IEnumerator StartCasting()
        {
            _illusions.Clear();
            _playerSuccessful = false;
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                ["preCastVfx"] = preCastVfx,
                ["illusionCarnival"] = this
            };
            state = SkillState.PreCasting;
            animator.SetTrigger(EnemyActionType.CastSpell1);
            yield return new WaitForSeconds(1.8f);
            StartCoroutine(StartPrecastVFX());
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].enabled = false;
            }
            
            var eventd = new EventDto
            {
                Event = "GET_ATTACKED",
                ["attacker"] = MaestroLogicHandle,
                ["target"] = target.GetComponent<AureliaMockUp>().LogicHandle,
                ["context"] = new EventDto
                {
                    ["cxt"] = "pre"
                },
                ["skill"] = this.LogicHandle
            };
            LogicLayer.GetInstance().Observe(eventd);

            Quaternion targetAngle = rotateToMouseScript.GetRotation();
            for (int i = 0; i < 5; i++)
            {
                float angle = i * Mathf.PI * 2 / 5 + targetAngle.eulerAngles.y;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;
                GameObject illusion = Instantiate(prefab, new Vector3(x, 0, z) + target.transform.position , Quaternion.identity);
                _illusions.Add(illusion);
            }

            _illusions[1].GetComponent<IllusionController>().isReal = true;
            
            // navMeshAgent.transform.position = _illusions[1].transform.position;
            //
            // Destroy(_illusions[1]);
            // _illusions.RemoveAt(1);
            
            // oldSpeed = navMeshAgent.speed;
            for (int i = 0; i < _illusions.Count; i++)
            {
                // NavMeshAgent agent = _illusions[i].GetComponent<NavMeshAgent>();
                // agent.speed = speed;
                // agent.acceleration = navMeshAgent.acceleration;
                // agent.angularSpeed = navMeshAgent.angularSpeed;
                // agent.stoppingDistance = navMeshAgent.stoppingDistance;
                
                StartCoroutine(_illusions[i].GetComponent<IllusionController>().ShowIllusion(data));
            }
            
            state = SkillState.Casting;
            endCastingTime = Time.time + timeout;
            
            // StartCoroutine(StartPrecastVFX());
            yield return new WaitForSeconds(0.3f);
            // for (int i = 0; i < renderers.Count; i++)
            // {
            //     renderers[i].enabled = true;
            // }
            
            navMeshAgent.isStopped = false;

            // yield return new WaitForSeconds(1f);

        }

        // Start is called before the first frame update
        public override void OnEnable()
        {
            base.OnEnable();
            // rotateToMouseScript = target.GetComponent<RotateToMouseScript>();
        }
        
        // Update is called once per frame
        public override void Update()
        {
            if (state == SkillState.Casting)
            {
                if (Time.time > endCastingTime)
                {
                    FailCast();
                }
                var center = target.transform.position;
                var angle = speed * Mathf.PI * Time.deltaTime;

                // move navMeshAgent of illusion around target a radius of radius an angle of 0.1 * Mathf.PI
                for (int i = 0; i < _illusions.Count; i++)
                {
                    var currentPosition = _illusions[i].transform.position;
                    var x = (currentPosition.x - center.x) * Mathf.Cos(angle) -
                        (currentPosition.z - center.z) * Mathf.Sin(angle) + center.x;
                    var z = (currentPosition.x - center.x) * Mathf.Sin(angle) +
                        (currentPosition.z - center.z) * Mathf.Cos(angle) + center.z;
                    
                    // Raycast to Ground Layer and get the y
                    // RaycastHit hit;
                    // if (Physics.Raycast(new Vector3(x, 100, z), Vector3.down, out hit, 200, 1 << 8))
                    // {
                    //     x = hit.point.x;
                    //     z = hit.point.z;
                    // }
                    _illusions[i].transform.position = new Vector3(x, this.transform.position.y, z);
                    var nextX = (x - center.x) * Mathf.Cos(angle) -
                        (z - center.z) * Mathf.Sin(angle) + center.x;
                    var nextZ = (x - center.x) * Mathf.Sin(angle) +
                        (z - center.z) * Mathf.Cos(angle) + center.z;
                    _illusions[i].transform.rotation = Quaternion.LookRotation(new Vector3(nextX, this.transform.position.y, nextZ) - _illusions[i].transform.position);
                }
            }
            else
            {
                // navMeshAgent.isStopped = false;
                if (state == SkillState.PostCasting)
                {
                    if (_playerSuccessful)
                    {
                        
                    }

                }
            }
        }

        public void StopCasting()
        {
            
        }
        
        public void FailCast()
        {
            state = SkillState.PostCasting;
            // Aurelia Hit The Fake Illusion
            _playerSuccessful = false;
            Debug.Log("FailCast");
            for (int i = 0; i < _illusions.Count; i++)
            {
                StartCoroutine(_illusions[i].GetComponent<IllusionController>().HideIllusion(true));
            }
            
            Debug.Log("Hid All Illusions");

            StartCoroutine(StartHitting());
        }

        public override IEnumerator StartHitting()
        {
            navMeshAgent.transform.position = target.transform.position + new Vector3(8, 0, 14);
            navMeshAgent.transform.rotation = new Quaternion(0, -144, 0, 0);
            
            yield return new WaitForSeconds(0.9f);
            // Laugh something...
            animator.SetTrigger(EnemyActionType.SpecialAttack);
            yield return new WaitForSeconds(0.5f);

            // move navMeshAgent of illusion a distance of 4 to the target

            StartCoroutine(StartPrecastVFX());
            yield return new WaitForSeconds(0.4f);
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].enabled = true;
            }
            yield return new WaitForSeconds(0.6f);
            Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            
            var eventd = new EventDto
            {
                Event = "GET_ATTACKED",
                ["attacker"] = MaestroLogicHandle,
                ["target"] = target.GetComponent<AureliaMockUp>().LogicHandle,
                ["context"] = new EventDto
                    {
                        ["cxt"] = "post-fail"
                    },
                ["skill"] = this.LogicHandle
            };
            LogicLayer.GetInstance().Observe(eventd);
            
            GameObject hit = Instantiate(hitPrefab, target.transform.position, Quaternion.identity);
            Destroy(hit, 1f);

            Debug.Log("Renderer Count: " + renderers.Count);
            state = SkillState.Idle;
        }

        public void SuccessCast()
        {
            state = SkillState.PostCasting;
            // Aurelia Hit The REAL Illusion
            _playerSuccessful = true;
            Debug.Log("SuccessCast");
            var eventd = new EventDto
            {
                Event = "GET_ATTACKED",
                ["attacker"] = MaestroLogicHandle,
                ["target"] = target.GetComponent<AureliaMockUp>().LogicHandle,
                ["context"] = new EventDto
                {
                    ["cxt"] = "post-success"
                },
                ["skill"] = this.LogicHandle
            };
            navMeshAgent.transform.position = _illusions[1].transform.position;
            navMeshAgent.transform.rotation = _illusions[1].transform.rotation;
            for (int i = 0; i < _illusions.Count; i++)
            {
                StartCoroutine(_illusions[i].GetComponent<IllusionController>().HideIllusion(i != 1));
            }
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].enabled = true;
            }
            
            // Injured animation
            state = SkillState.Idle;
        }
    }
}
