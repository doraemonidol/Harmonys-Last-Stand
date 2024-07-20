using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Presentation.Maestro
{
    public class IllusionController : MonoBehaviour
    {
        [SerializeField] private List<Renderer> renderers;
        
        private void Start()
        {
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].enabled = false;
            }
        }
        
        public IEnumerator ShowIllusion(Dictionary<string, object> data)
        {
            VFX preCastVfx = (VFX) data["preCastVfx"];
            StartCoroutine(StartPrecastVFX(preCastVfx));
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].enabled = true;
            }

            this.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        }

        private IEnumerator StartPrecastVFX(VFX preCastVfx)
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
    }
}