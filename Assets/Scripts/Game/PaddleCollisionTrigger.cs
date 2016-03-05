namespace Assets.Scripts.Game {
    using UnityEngine;

    class PaddleCollisionTrigger : MonoBehaviour {
        [SerializeField]
        private Collider2D _paddle;

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("BALL")) {
                Physics2D.IgnoreCollision(collision, _paddle, true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.CompareTag("BALL")) {
                Physics2D.IgnoreCollision(collision, _paddle, false);
            }
        }
    }
}