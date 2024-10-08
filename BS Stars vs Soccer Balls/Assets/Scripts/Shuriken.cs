using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [SerializeField] private Animator _animShuriken;
    [SerializeField] private SpriteRenderer _spriteRend;
    [SerializeField] private Sprite[] _sprites;
    private int _numImage;

    private Rigidbody2D _rbShuriken;

    private void Awake()
    {
        _rbShuriken = GetComponent<Rigidbody2D>();
    }

    public void InitNew()
    {
        _spriteRend.sprite = _sprites[_numImage];

        _animShuriken.Play("idle");
    }

    public void AddForceToMove(Vector2 directionMove, float force)
    {
        _rbShuriken.isKinematic = false;
        _rbShuriken.velocity = directionMove * force;
        _animShuriken.Play("Rotate");
    }

    public void SetSkin(int number)
    {
        _numImage = number;
        _spriteRend.sprite = _sprites[_numImage];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Element element))
        {
            Element.TypeElement type = element.GetTypeElement();

            switch (type)
            {
                case Element.TypeElement.True:
                    Explosion.Instance.SetPosExplosion(element.transform.position);
                    GameSessionHandler.OnAddScore?.Invoke();

                    SoundsGame.I.RunSound(SoundsGame.Sound.Boom);
                    break;
                case Element.TypeElement.False:
                    GameSessionHandler.OnDiscreaseHp?.Invoke();

                    SoundsGame.I.RunSound(SoundsGame.Sound.Bomb);
                    break;
            }
            element.ReturnToPool();

            _rbShuriken.isKinematic = true;
            _rbShuriken.velocity = Vector2.zero;
            transform.gameObject.SetActive(false);
            GameSessionHandler.OnShurikenMoveEnd?.Invoke();
        }

        if (collision.gameObject.TryGetComponent(out ClearObject clear))
        {
            _rbShuriken.isKinematic = true;
            _rbShuriken.velocity = Vector2.zero;
            transform.gameObject.SetActive(false);
            GameSessionHandler.OnShurikenMoveEnd?.Invoke();
        }
    } 
}
