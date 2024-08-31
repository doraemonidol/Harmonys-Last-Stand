using System.Collections;
using Common;
using MockUp;
using UnityEngine;

namespace Presentation.Maestro
{
    public class PJackInTheBoxMayhem : MaestroSkill
    {
        [Header("Spawn Settings")]
        [SerializeField] private GameObject jackInTheBoxPrefab;

        [SerializeField] private float spawnChance;
        
        [Header("Raycast Settings")]
        [SerializeField] private float distanceBetweenCheck;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Transform TopRight, BottomLeft;
        private Vector2 positivePosition, negativePosition;
        [SerializeField] private float heightOfCheck = 10f, rangeOfCheck = 30f;
        
        public override void Start()
        {
            base.Start();
            positivePosition = new Vector2(Mathf.Max(TopRight.position.x, BottomLeft.position.x), Mathf.Max(TopRight.position.z, BottomLeft.position.z));
            negativePosition = new Vector2(Mathf.Min(TopRight.position.x, BottomLeft.position.x), Mathf.Min(TopRight.position.z, BottomLeft.position.z));
        }
        
        public override IEnumerator StartCasting()
        {
            animator.SetTrigger(EnemyActionType.CastSpell2);
            yield return new WaitForSeconds(1.2f);
            StartCoroutine(StartPrecastVFX());
            SpawnResources();
            yield return new WaitForSeconds(1f);
        }

        private void SpawnResources()
        {
            if (distanceBetweenCheck <= 0)
            {
                Debug.LogError("Distance between check must be greater than 0");
                return;
            }
            
            for (float x = negativePosition.x; x < positivePosition.x; x += distanceBetweenCheck)
            {
                for (float z = negativePosition.y; z < positivePosition.y; z += distanceBetweenCheck)
                {
                    Vector3 position = new Vector3(x, heightOfCheck, z);
                    RaycastHit hit;
                    if (Physics.Raycast(position, Vector3.down, out hit, rangeOfCheck, layerMask))
                    {
                        if (Random.Range(0, 101) < spawnChance)
                        {
                            float randomHeight = Random.Range(5.0f, 15.0f);
                            GameObject jackBox = Instantiate(jackInTheBoxPrefab, hit.point, Quaternion.identity);
                            jackBox.transform.GetChild(0).gameObject.AddComponent<SkillColliderInfo>().Initialize(
                                attacker: this.Owner,
                                skill: this.LogicHandle
                            );

                            jackBox.GetComponent<Rigidbody>().AddForce(Vector3.up * randomHeight, ForceMode.Impulse);
                            Destroy(jackBox, timeout);
                        }
                    }
                }
            }
        }

        public override IEnumerator StartHitting()
        {
            throw new System.NotImplementedException();
        }
    }
}