using UnityEngine;

namespace ithappy
{
    public class Rnd_Animation : MonoBehaviour
    {
        [SerializeField] private string titleAnim;

        private Animator anim;
        private float offsetAnim;


        // Start is called before the first frame update
        private void Start()
        {
            anim = GetComponent<Animator>();
            offsetAnim = Random.Range(0f, 1f);


            anim.Play(titleAnim, 0, offsetAnim);
        }
    }
}