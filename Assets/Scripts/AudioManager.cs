using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip _paddleHitClip;
    [SerializeField] private AudioClip _wallHitClip;

    [Header("Music List")]
    public AudioClip[] _musicList;
    private int _lastIndex = -1;

    [SerializeField] private float _ballBounceVolume = 0.8f;
    [SerializeField] private float _musicVolume = 0.5f;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
        //audioSource.playOnAwake = false;
    }

    private void Start()
    {
        StartCoroutine(MusicLoop());
    }


    public void PlayPaddleHit()
    {
        _audioSource.PlayOneShot(_paddleHitClip, _ballBounceVolume);
    }
    public void PlayWallHit()
    {
        _audioSource.PlayOneShot(_wallHitClip, _ballBounceVolume);
    }

    private IEnumerator MusicLoop()
    {
        while (true)
        {
            PlayRandomMusic();

            if (_audioSource.clip == null)
                yield break;

            yield return new WaitForSeconds(_audioSource.clip.length);
        }
    }

    private void PlayRandomMusic()
    {
        if (_musicList == null || _musicList.Length == 0)
            return;

        int index;
        do
        {
            index = Random.Range(0, _musicList.Length);
        }
        while (index == _lastIndex && _musicList.Length > 1);

        _lastIndex = index;

        _audioSource.clip = _musicList[index];
        _audioSource.volume = _musicVolume;
        _audioSource.loop = false;
        _audioSource.Play();
    }
}
