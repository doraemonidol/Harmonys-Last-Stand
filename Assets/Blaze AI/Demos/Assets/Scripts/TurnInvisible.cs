using UnityEngine;

namespace BlazeAIDemo
{
    public class TurnInvisible : MonoBehaviour
    {
        public GameObject body;
        public Material invisibleMat;
        public AudioSource invisibleAudio;
        public AudioSource returnAudio;
        public bool invisibleOnStart;
        private Material defaultMat;
        private string defaultTag;

        private bool state;

        private void Start()
        {
            if (invisibleOnStart) Invisible();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (state)
                    Appear();
                else
                    Invisible();
            }
        }

        private void Invisible()
        {
            //turn invisible
            state = true;
            defaultTag = transform.tag;
            defaultMat = body.GetComponent<Renderer>().material;
            body.GetComponent<Renderer>().material = invisibleMat;
            transform.tag = "Untagged";
            if (!invisibleAudio.isPlaying) invisibleAudio.Play();
        }

        private void Appear()
        {
            //return normal
            state = false;
            body.GetComponent<Renderer>().material = defaultMat;
            transform.tag = defaultTag;
            if (!returnAudio.isPlaying) returnAudio.Play();
        }
    }
}