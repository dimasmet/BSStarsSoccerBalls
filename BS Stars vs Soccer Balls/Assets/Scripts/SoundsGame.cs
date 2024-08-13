using UnityEngine;

public class SoundsGame : MonoBehaviour
{
    public static SoundsGame I;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
    }

    public enum Sound
    {
        Boom,
        Bomb,
        Click,
        Result,
    }

    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _boom;
    [SerializeField] private AudioSource _bomb;
    [SerializeField] private AudioSource _click;
    [SerializeField] private AudioSource _result;

    private bool isSound = true;
    private bool isMusic = true;

    public void RunSound(Sound sound)
    {
        if (isSound)
            switch (sound)
            {
                case Sound.Boom:
                    _boom.Play();
                    break;
                case Sound.Bomb:
                    _bomb.Play();
                    break;
                case Sound.Click:
                    _click.Play();
                    break;
                case Sound.Result:
                    _result.Play();
                    break;
            }
    }

    public bool ActivityMusic()
    {
        isMusic = !isMusic;

        if (isMusic) _music.Play(); else _music.Stop();

        return isMusic;
    }

    public bool ActivitySounds()
    {
        isSound = !isSound;

        return isSound;
    }
}
