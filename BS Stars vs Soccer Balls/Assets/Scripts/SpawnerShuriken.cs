using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerShuriken
{
    private Vector2 _positionSpawn;
    private Shuriken _shuriken;

    public SpawnerShuriken(Vector2 pos, Shuriken shuriken)
    {
        _positionSpawn = pos;
        _shuriken = shuriken;
    }

    public IEnumerator WaitSpawnNewShuriken()
    {
        yield return new WaitForSeconds(0.4f);
        _shuriken.transform.position = _positionSpawn;
        _shuriken.InitNew();
        _shuriken.gameObject.SetActive(true);
    }
}
