using UnityEngine;

namespace BlazeAIDemo
{
    public class ClickToDistract : MonoBehaviour
    {
        public AudioSource distractionAudio;
        private BlazeAIDistraction distractionScript;

        private void Start()
        {
            distractionScript = GetComponent<BlazeAIDistraction>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (distractionAudio) distractionAudio.Play();
                distractionScript.TriggerDistraction();
            }
        }
    }
}