using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public static Explosion Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField] private Animator _anim;

    public void SetPosExplosion(Vector2 pos)
    {
        transform.position = pos;
        _anim.Play("Active");
    }
}
