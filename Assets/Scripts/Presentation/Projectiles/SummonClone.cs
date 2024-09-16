using System;
using UnityEngine;

namespace Presentation.Projectiles
{
    public class SummonClone : StaticTargeting
    {
        [SerializeField] public float range = 10f;
        private GameObject player;
        public void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Debug.Log("Summon Clone Start");
            base.Start();
        }

        public override void Ultimate()
        {
            Vector3 playerPosition = player.transform.position;
            player.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            transform.position = playerPosition;
        }

        private void OnDestroy()
        {
            Debug.Log("Summon Clone Destroyed");
        }
    }
}