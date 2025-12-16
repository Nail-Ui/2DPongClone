using UnityEngine;

public class BouncySurface : MonoBehaviour
{
    [SerializeField] float extraSpeed = 0.5f;
    [SerializeField] float soundCoolDown = 0.05f; //50ms

    private float _lastSoundTime = -10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BallScript ball = collision.gameObject.GetComponent<BallScript>();

        if (ball == null) return;
        {
            ball.BoostSpeed(extraSpeed);
            // Vector2 normal = collision.GetContact(0).normal;
            // ball.AddBallForce(-normal * this._bouncyStrenght);
        }

        if (Time.time - _lastSoundTime < soundCoolDown) return;

        _lastSoundTime = Time.time;

        //Ses effectleri
        if (gameObject.CompareTag("Paddle"))
        {
            AudioManager.Instance.PlayPaddleHit();
        }
        else if (gameObject.CompareTag("Wall"))
        {
            AudioManager.Instance.PlayWallHit();
        }
    }
}
