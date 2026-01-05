using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sources")]
    [SerializeField] AudioSource bgmSource;      // loop ON推奨
    [SerializeField] AudioSource sfxSource;      // PlayOneShot用
    [SerializeField] AudioSource chargerSource;  // チャージ警告/サイレン用

    [Header("Clips")]
    [SerializeField] AudioClip bgmClip;
    [SerializeField] AudioClip gunShotClip;
    [SerializeField] AudioClip chargerLoopClip;

    [Header("Volumes")]
    [Range(0f, 1f)] [SerializeField] float bgmVolume = 0.6f;
    [Range(0f, 1f)] [SerializeField] float gunVolume = 0.8f;      // ←Inspectorで調整
    [Range(0f, 1f)] [SerializeField] float chargerVolume = 0.8f;

    Coroutine chargerRoutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // DontDestroyOnLoad(gameObject);  // シーン跨がないなら不要
    }

    private void Start()
    {
        // 初期BGM開始（仕様：ゲーム開始時）
        PlayBgm();
    }

    public void PlayBgm()
    {
        if (bgmSource == null || bgmClip == null) return;

        bgmSource.clip = bgmClip;
        bgmSource.volume = bgmVolume;
        bgmSource.loop = true;

        if (!bgmSource.isPlaying)
            bgmSource.Play();
    }

    public void StopBgm()
    {
        if (bgmSource == null) return;
        bgmSource.Stop();
    }

    public void PlayGunShot()
    {
        if (sfxSource == null || gunShotClip == null) return;
        sfxSource.PlayOneShot(gunShotClip, gunVolume);
    }

    // chargerが追いかけてくる時（3秒）だけ鳴らす
    public void PlayChargerLoopForSeconds(float seconds)
    {
        if (chargerSource == null || chargerLoopClip == null) return;

        if (chargerRoutine != null)
            StopCoroutine(chargerRoutine);

        chargerRoutine = StartCoroutine(PlayCharger(seconds));
    }

    IEnumerator PlayCharger(float seconds)
    {
        chargerSource.clip = chargerLoopClip;
        chargerSource.volume = chargerVolume;
        chargerSource.loop = true;

        chargerSource.Play();
        yield return new WaitForSeconds(seconds);

        chargerSource.Stop();
        chargerRoutine = null;
    }
}
