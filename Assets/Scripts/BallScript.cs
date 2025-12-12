using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed = 100f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        AddStartingForce();
    }

    private void AddStartingForce()
    {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);

        Vector2 _direction = new Vector2(x, y);

        _rb.AddForce(_direction * _speed);
    }
}
