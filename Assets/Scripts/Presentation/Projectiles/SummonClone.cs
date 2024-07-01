using System;
using UnityEngine;

namespace Presentation.Projectiles
{
    public class SummonClone : StaticTargeting
    {
        [SerializeField] public float range = 10f;
        public override void Start()
        {
            Debug.Log("Summon Clone Start");
            base.Start();
        }

        public override void Ultimate()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 playerPosition = player.transform.position;
            player.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            transform.position = playerPosition;
        }
    }
}