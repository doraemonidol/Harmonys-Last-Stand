using UnityEngine;

namespace BlazeAISpace
{
    public class StayInPosition : MonoBehaviour
    {
        private BlazeAI blaze;

        private void Start()
        {
            blaze = GetComponent<BlazeAI>();
        }

        private void Update()
        {
            if (blaze.state == BlazeAI.State.normal || blaze.state == BlazeAI.State.alert) blaze.StayIdle();
        }
    }
}