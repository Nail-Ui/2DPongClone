using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float maxBounceAngleDeg = 60f;
    [SerializeField] private float minVertical = 0.2f;
    [SerializeField] private float minHorizontal = 0.4f; // yeni: çok dik açıları engelle
    [SerializeField] private GameManager gameManager;

    private Rigidbody2D _rigidBody;
    private bool _isActive = true;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        LaunchBall();
    }

    private void LaunchBall()
    {
        _isActive = true;

        float randomX = Random.Range(0, 2) == 0 ? 1f : -1f;
        float randomY = Random.Range(-0.5f, 0.5f);

        Vector2 direction = new Vector2(randomX, randomY).normalized;
        _rigidBody.linearVelocity = direction * speed;
    }

    private void FixedUpdate()
    {
        if (!_isActive)
            return;

        Vector2 v = _rigidBody.linearVelocity;

        // Hız çok düşmesin, hep sabit olsun
        if (v.sqrMagnitude > 0.0001f)
        {
            v = v.normalized * speed;
            _rigidBody.linearVelocity = v;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.collider.CompareTag("Paddle"))
    {
        // Paddle boyuna göre -1 ile 1 arası offset
        Collider2D paddleCol = collision.collider;

        float paddleY = paddleCol.bounds.center.y;
        float paddleHeight = paddleCol.bounds.size.y;

        float contactY = collision.GetContact(0).point.y;
        float offset = (contactY - paddleY) / (paddleHeight / 2f); // -1..1
        offset = Mathf.Clamp(offset, -1f, 1f);

        // Top şu an sağa mı sola mı gidiyor?
        float currentXDir = Mathf.Sign(_rigidBody.linearVelocity.x);
        if (currentXDir == 0) currentXDir = 1f;

        // Maksimum sekme açısını radyana çevir
        float maxBounceAngleRad = maxBounceAngleDeg * Mathf.Deg2Rad;

        // Offset'e göre bir açı hesapla (-maxAngle .. +maxAngle)
        float bounceAngle = offset * maxBounceAngleRad;

        // Yeni yön vektörü:
        // X: cos, Y: sin; sağ/sol paddle için X işaretini ters çeviriyoruz
        Vector2 newDir = new Vector2(
            Mathf.Cos(bounceAngle) * -currentXDir,
            Mathf.Sin(bounceAngle)
        );

        newDir = newDir.normalized;
        _rigidBody.linearVelocity = newDir * speed;
    }

    if (collision.collider.CompareTag("RightGoal"))
    {
        gameManager.AddScore(true);
        gameManager.ResetBall();
    }
    else if (collision.collider.CompareTag("LeftGoal"))
    {
        gameManager.AddScore(false);
        gameManager.ResetBall();
    }
}
    // Goal'ler için trigger kullanıyoruz
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RightGoal"))
        {
            gameManager.AddScore(true);
            gameManager.ResetBall();
        }
        else if (other.CompareTag("LeftGoal"))
        {
            gameManager.AddScore(false);
            gameManager.ResetBall();
        }
    }

    public void StopBall()
    {
        _isActive = false;
        _rigidBody.linearVelocity = Vector2.zero;
    }

    public IEnumerator ResetRoutine()
    {
        StopBall();
        transform.position = Vector3.zero;
        yield return new WaitForSeconds(1f);
        LaunchBall();
    }
}