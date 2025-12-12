using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [Header("Paddle Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] bool isPlayer1;
    [SerializeField] float _yRange = 3.6f;

    private float InputY;
    private Rigidbody2D _rb;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Input her zaman Update de okunur, Physics ise FixedUpdate de

        if (isPlayer1)
        {
            InputY = Input.GetAxisRaw("Vertical");
        }
        else
        {
            InputY = Input.GetAxisRaw("Vertical2");
        }
    }

    private void FixedUpdate()
    {

        //Şu anki pozisyon
        Vector2 pos = _rb.position;

        // Yeni Y pozisyonunu hesaplıyoruz
        float newY = pos.y + InputY * moveSpeed * Time.fixedDeltaTime;

        newY = Mathf.Clamp(newY, -_yRange, _yRange);

        // Sadece MovePosition kullanıyoruz (_rb.position'a direkt yazmıyoruz)
        Vector2 targetPos = new Vector2(pos.x, newY);
        _rb.MovePosition(targetPos);

        // Aşağıda olan sistem çalışmıyor çünkü Önce MovePosition diyorsun → fizik sistemi “ben bunu bir sonraki fixed step’te uygularım” diyor.
        //Sonra rb.position = ... ile anında pozisyonu override ediyorsun.
        
        // Vector2 targetPos = _rb.position + Vector2.up * (InputY * moveSpeed * Time.fixedDeltaTime);
        // _rb.MovePosition(targetPos);

        // Vector2 _clampPosition = _rb.position;
        // _clampPosition.y = Mathf.Clamp(_clampPosition.y, -_yRange, _yRange);
        // _rb.position = _clampPosition;
    }
}
