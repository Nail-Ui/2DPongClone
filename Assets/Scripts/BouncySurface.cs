using UnityEngine;

public class BouncySurface : MonoBehaviour
{
    [SerializeField] float extraSpeed = 0.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BallScript ball = collision.gameObject.GetComponent<BallScript>();

        if(ball != null)
        {
            ball.BoostSpeed(extraSpeed);

            // Vector2 normal = collision.GetContact(0).normal;
            // ball.AddBallForce(-normal * this._bouncyStrenght);
        }
    }
}
