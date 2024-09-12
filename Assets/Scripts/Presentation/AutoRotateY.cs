using System.Collections;
using UnityEngine;

namespace Presentation
{
    public class AutoRotateY : MonoBehaviour
    {
        public float RotationSpeed = 10f;
        
        private void Start()
        {
            StartCoroutine(StartRotating());
        }
        
        private IEnumerator StartRotating()
        {
            while (true)
            {
                transform.Rotate(Vector3.up, RotationSpeed * 0.01f);
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }
    }
}