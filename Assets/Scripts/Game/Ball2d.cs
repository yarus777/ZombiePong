#region References

#endregion

namespace Assets.Scripts.Game {
    using System;
    using Currency;
    using Effects;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class Ball2d : MonoBehaviour {
        [Tooltip("SPEED OF THE BALL WHEN GAME STARTS")]
        public float speedOnStart = 100f; // SPEED ON GAME START

        public Animation spriteAnimation;

        private bool isAlive; // IS PLAYER ALIVE

        [SerializeField]
        private float ballSpeed; // CURRENT SPEED

        private float lastTimeHitPaddle; // STORE LAST TIME THE BALL HITS PADDLE

        private Vector2 moveDirection; // DIRECTION

        private Transform tr; // CACHED TRANSFORM

        private Rigidbody2D rb; // CACHED RIGIDBODY 2D
        public event Action Hit;

        private int hit_count;

        void Awake() {
            tr = GetComponent<Transform>();
            rb = GetComponent<Rigidbody2D>();
            GetComponent<CircleCollider2D>();
            GameLogic2d.Instance.StateChanged += OnStateChanged;
        }

        private void OnStateChanged(GameLogic2d.State state) {
            if (state == GameLogic2d.State.Game) {
                OnGameStart();
            }
        }

        void FixedUpdate() {
            if (isAlive) {
                // KEEP THE SPEED OF THE BALL
                rb.velocity = moveDirection*ballSpeed*GameLogic2d.Instance.GameSpeed*Time.deltaTime;
            } else {
                rb.velocity = Vector2.zero;
            }
        }

        private void OnHit() {
            var handler = Hit;
            if (handler != null) {
                handler();
            }
        }

        void OnTriggerEnter2D(Collider2D __c) {
            if (isAlive) {
                // BALL HITS PADDLE
                if (__c.CompareTag("PADDLE")) {
                    Debug.Log("hit");
                    if ((Time.time - lastTimeHitPaddle) > 0.1f) {
                        // CHANGE DIRECTION TOWARD CENTER OF THE CIRCLE WITH AN ADDITIONAL RANDOM ANGLES
                        moveDirection = Vector3.Reflect(moveDirection, new Vector3(0.2f, -1.3f, 0) - tr.position);
                        moveDirection = Quaternion.Euler(0f, 0f, Random.Range(0f, 45f))*moveDirection;
                        moveDirection.Normalize();

                        // UPDATE SCORE

                        GameLogic2d.Instance.AddScore(1);
                        hit_count++;

                        Debug.Log("HitCount" + hit_count);
                        if (hit_count == 10) {
                            Currency.Instance.AddCoins();
                            hit_count = 0;
                        }

                        spriteAnimation.Play("BallHit");
                        OnHit();

                        //MusicManager.Instance.PlayGameBallHitPaddle ();
                        SoundManager.Instance.Play(FxType.BallHit);
                    }

                    lastTimeHitPaddle = Time.time;
                }
            }
        }

        void OnTriggerExit2D(Collider2D __c) {
            // GAME IS OVER IF THE BALL EXITS CIRCLE
            if (__c.CompareTag("INSIDECIRCLE")) {
                GameOver();
                hit_count = 0;
            }
        }

        // CALL WHEN GAME STARTS
        private void OnGameStart() {
            // PLACE THE BALL IN CENTER OF THE CIRCLE
            tr.position = new Vector3(0.2f, -0.39f, 0);

            // THE BALL WILL MOVE IN UP DIRECTION AT START
            moveDirection = Vector2.up;

            lastTimeHitPaddle = Time.time;

            // INITIAL MOVING SPEED
            ballSpeed = speedOnStart;

            // 
            isAlive = true;
        }

        void GameOver() {
            if (isAlive) {
                isAlive = false;

                // GAMELOGIC2D HANDLES GAME OVER
                if (Lost != null) {
                    Lost();
                }
                SoundManager.Instance.Play(FxType.GameOver);
                //MusicManager.Instance.PlayGameBallOver ();
            }
        }

        public event Action Lost;
    }
}