using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MockUp;
using Common;

namespace Presentation.Maestro
{
    public class PIllusionCarnival : MaestroSkill
    {
        [SerializeField] private float radius = 2;
        [SerializeField] private GameObject prefab;
        [SerializeField] private List<Renderer> renderers;
        private List<GameObject> illusions = new List<GameObject>();

        [SerializeField] private RotateToMouseScript rotateToMouseScript;

        public override IEnumerator StartCasting()
        {
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
            Debug.Log("Start casting illusion carnival" + targetAngle);
            // Create 5 sphere around (0, 0, 0) with radius 2
            for (int i = 1; i < 2; i++)
            {
                float angle = i * Mathf.PI * 2 / 5 + targetAngle.eulerAngles.y;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;
                GameObject illusion = Instantiate(prefab, new Vector3(x, 0, z) + target.transform.position , Quaternion.identity);
                illusions.Add(illusion);
            }

            yield return new WaitForSeconds(1f);

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
    }
}
