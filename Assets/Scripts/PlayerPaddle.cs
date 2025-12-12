using UnityEngine;

public class PlayerPaddle : Paddle
{
    private Vector2 _direction;
    private float _inputY;

    private void Update()
    {
        // if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        // {
        //     _direction = Vector2.up;
        // }
        // else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        // {
        //     _direction = Vector2.down;
        // }
        // else
        // {
        //     _direction = Vector2.zero;
        // }
        
        if (_isPlayer)
        {
            _inputY = Input.GetAxisRaw("Vertical");
        }
        
        _direction = new Vector2(0, _inputY);
    }

    private void FixedUpdate()
    {
        if(_direction.sqrMagnitude != 0)
        {
            _rb.AddForce(_direction * _speed);
        }
    }
}
