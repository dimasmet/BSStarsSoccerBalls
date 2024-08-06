using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private Rigidbody2D _rbShuriken;

    private void Awake()
    {
        _rbShuriken = GetComponent<Rigidbody2D>();
    }

    public void AddForceToMove(Vector2 directionMove, float force)
    {
        _rbShuriken.isKinematic = false;
        //_rbShuriken.AddForce(directionMove * force);
        _rbShuriken.velocity = directionMove * force;
    }
}
