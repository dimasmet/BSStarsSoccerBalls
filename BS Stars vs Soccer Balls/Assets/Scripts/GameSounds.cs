using UnityEngine;

public class GameSounds : MonoBehaviour
{
    public static GameSounds I;

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
        Bomb
    }

    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _boom;
    [SerializeField] private AudioSource _bomb;

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
