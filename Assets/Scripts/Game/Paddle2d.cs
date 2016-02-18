using UnityEngine;

namespace Assets.Scripts.Game {
    using System;

    public class Paddle2d : MonoBehaviour
    {
        public	Animation 	spriteAnimation;

        void OnTriggerEnter2D ( Collider2D __c )
        {
            if ( __c.CompareTag ( "BALL" ) )
            {
                this.spriteAnimation.Play ( "PaddleHit" );
            }
        }
    }
}
