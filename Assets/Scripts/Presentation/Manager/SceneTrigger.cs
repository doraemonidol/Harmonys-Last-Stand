using System;
using System.Collections;
using Common;
using UnityEngine;

namespace Presentation.GUI
{
    public class SceneTrigger : MonoBehaviour
    {
        [SerializeField] private SceneTypeEnum sceneType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                UIManager.Instance.OnLoadScene(sceneType);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(DisableTrigger());
            }
        }
        
        IEnumerator DisableTrigger()
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            yield return new WaitForSeconds(1);
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}