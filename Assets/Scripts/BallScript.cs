using System.Collections;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private float _maxBounceAngleDeg = 60f; // 45-70 arası classic pong gibi ?
    [SerializeField] private float _speed = 100f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        ResetPosition();
        StartCoroutine(StartingForceWait());
    }

    public void ResetPosition()
    {
        _rb.position = Vector3.zero;
        _rb.linearVelocity = Vector3.zero;
    }

    public IEnumerator StartingForceWait()
    {
        yield return new WaitForSeconds(2);

        AddStartingForce();
    }

    public void AddStartingForce()
    {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);

        Vector2 _direction = new Vector2(x, y);

        _rb.AddForce(_direction * _speed);
    }

    //Encapsulation u bozmamak için diğer scriptlerden ulaşılabilecek bir method yapıyoruz
    public void AddBallForce(Vector2 force)
    {
        _rb.AddForce(force);
    }


    //(Eski Basic sistem) Top hızlandıkça Y yönünde daha fazla sekmeye başlıyor bu da rahatsız edici bir oyun deneyimi sunuyor.

    // public void BoostSpeed(float extraSpeed)
    // {
    //     Vector2 v = _rb.linearVelocity;

    //     if (v.sqrMagnitude < 0.0001f) return;

    //     float _newSpeed = v.magnitude + extraSpeed;
    //     _rb.linearVelocity = v.normalized * _newSpeed;
    // }

    public void BoostSpeed(float extraSpeed)
    {
        Vector2 v = _rb.linearVelocity;
        if(v.sqrMagnitude < 0.0001f) return;

        float _newSpeed = v.magnitude + extraSpeed;

        //1) Yönü alıyoruz 
        Vector2 dir = v.normalized;

        // Aşırı dik bir açı ise yumuşatıyoruz (angle clamp)
        dir = ClampDirectionAngle(dir, _maxBounceAngleDeg);

        //yeni bir hız uyguluyoruz
        _rb.linearVelocity = dir * _newSpeed;
    }

    private Vector2 ClampDirectionAngle(Vector2 dir, float maxAngleDeg)
    {
        float xSign = Mathf.Sign(dir.x);
        if(xSign == 0) xSign =1f;

        //dir'i Sağa gidiyormuş gibi düşün (x pozitif)
        float angle = Mathf.Atan2(dir.y, Mathf.Abs(dir.x)); //0.90 derece

        float maxRad = maxAngleDeg * Mathf.Deg2Rad;
        angle = Mathf.Clamp(angle, -maxRad, maxRad);

        // yeni yön: x= cos, y = sin
        float x = Mathf.Cos(angle) * xSign;
        float y = Mathf.Sin(angle);

        return new Vector2(x, y).normalized;
    }
}
