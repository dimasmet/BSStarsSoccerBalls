using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static Action OnShurikenMoveEnd;

    private SpawnerShuriken _spawnerShuriken;
    [SerializeField] private Shuriken _shuriken;

    private void Start()
    {
        _spawnerShuriken = new SpawnerShuriken(_shuriken.transform.position, _shuriken);

        OnShurikenMoveEnd += NewShuriken;
    }

    private void OnDestroy()
    {
        OnShurikenMoveEnd -= NewShuriken;
    }

    private void NewShuriken()
    {
        StartCoroutine(_spawnerShuriken.WaitSpawnNewShuriken());
    }
}
