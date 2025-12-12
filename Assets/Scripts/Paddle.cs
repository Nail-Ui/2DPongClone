using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float _speed = 10.0f;
    public bool _isPlayer;
    protected Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


}
