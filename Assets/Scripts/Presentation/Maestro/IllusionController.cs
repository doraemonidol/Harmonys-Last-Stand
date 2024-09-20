using System.Collections;
using System.Collections.Generic;
using Common;
using DTO;
using Logic.Facade;
using MockUp;
using UnityEngine;
using UnityEngine.AI;

namespace Presentation.Maestro
{
    public class IllusionController : MonoBehaviour
    {
        [SerializeField] private List<Renderer> renderers;
        [SerializeField] public bool isReal = false;
        public Identity maestroLogicHandle;
        public Identity skillLogicHandle;
        private PIllusionCarnival _illusionCarnival;
        private VFX preCastVfx;
        
        private void Start()
        {
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].enabled = false;
            }
        }
        
        public IEnumerator ShowIllusion(Dictionary<string, object> data)
        {
            preCastVfx = (VFX) data["preCastVfx"];
            _illusionCarnival = (PIllusionCarnival) data["illusionCarnival"];
            StartCoroutine(StartPrecastVFX());
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].enabled = true;
            }

            this.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        }
        
        public IEnumerator HideIllusion(bool playVfx = true)
        {
            if (playVfx)
            {
                StartCoroutine(StartPrecastVFX());
                yield return new WaitForSeconds(0.3f);
            }
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].enabled = false;
            }
            yield return new WaitForSeconds(3f);
            
            Destroy(this.gameObject);
        }

        private IEnumerator StartPrecastVFX()
        {
            if (preCastVfx.HasVFX())
            {
                GameObject instantiatedVfx =  Instantiate(preCastVfx.vfx, transform.position, Quaternion.identity);
                
                if (preCastVfx.autoDestroy)
                {
                    Destroy(instantiatedVfx, preCastVfx.duration);
                }
            }

            yield return null;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy")) return;
            // Debug.Log(this.LogicHandle + "EnemyCollision Tag: " + other.gameObject.tag + " " + other.gameObject.name);

            if (other.gameObject.GetComponent<SkillColliderInfo>() == null) return;

            if (isReal)
            {
                _illusionCarnival.SuccessCast();
                return;
            }
            else
            {
                _illusionCarnival.FailCast();
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.gameObject.CompareTag("Enemy")) return;
            // Debug.Log(this.LogicHandle + "EnemyCollision Tag: " + other.gameObject.tag + " " + other.gameObject.name);

            if (other.gameObject.GetComponent<SkillColliderInfo>() == null) return;

            if (isReal)
            {
                _illusionCarnival.FailCast();
                return;
            }
            else
            {
                _illusionCarnival.SuccessCast();
            }
        }
    }
}