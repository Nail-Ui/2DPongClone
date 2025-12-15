using UnityEngine;

public class ComputerPaddle : Paddle
{
    public Rigidbody2D _rbBall;

    private void FixedUpdate()
    {
        if (this._rbBall.linearVelocity.x > 0.0f)
        {

            if (this._rbBall.position.y > this.transform.position.y)
            {
                _rb.AddForce(Vector2.up * this._speed);
            }
            else if (this._rbBall.position.y < this.transform.position.y)
            {
                _rb.AddForce(Vector2.down * this._speed);
            }
        }
        else
        {
            if(this.transform.position.y > 0.0f)
            {
                _rb.AddForce(Vector2.down * this._speed);
            }
            else if(this.transform.position.y < 0.0f)
            {
                _rb.AddForce(Vector2.up * this._speed);
            }
        }
    }
}
