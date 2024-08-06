using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private ObjectPool objectPool;

    [SerializeField] private Sprite[] _falseSprites;
    [SerializeField] private Sprite _trueSprites;

    private int numPosSpawn;

    private bool isBallSpawnInTime = false;

    private void Start()
    {
        GameSessionHandler.OnEndGame += StopSpawnElements;
        GameSessionHandler.OnStartSessionGame += StartSpawnElements;
    }

    private void OnDestroy()
    {
        GameSessionHandler.OnEndGame -= StopSpawnElements;
        GameSessionHandler.OnStartSessionGame -= StartSpawnElements;
    }

    private void StartSpawnElements()
    {
        StopAllCoroutines();
        objectPool.HideAllObject();
        isBallSpawnInTime = false;
        StartCoroutine(WaitToSpawnElement());
    }

    private void StopSpawnElements()
    {
        StopAllCoroutines();
        objectPool.HideAllObject();
    }

    private IEnumerator WaitToSpawnElement()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Vector2 posSpawn = _points[numPosSpawn].position;
            numPosSpawn++;
            if (numPosSpawn >= _points.Length) numPosSpawn = 0;

            Element element = objectPool.GetElement();
            element.InitElement(GetDataToElement(), posSpawn);

        }
    }

    private DataElement GetDataToElement()
    {
        float randomValue = Random.Range(0, 100);
        DataElement data;

        if (isBallSpawnInTime)//randomValue < 70)
        {
            data = new DataElement(_falseSprites[Random.Range(0, _falseSprites.Length)], Element.TypeElement.False);
        }
        else
        {
            data = new DataElement(_trueSprites, Element.TypeElement.True);
            isBallSpawnInTime = true;
            StartCoroutine(WaitSpawnBall());
        }

        return data;
    }

    private IEnumerator WaitSpawnBall()
    {
        yield return new WaitForSeconds(Random.Range(2,4));
        isBallSpawnInTime = false;

    }
}

public struct DataElement
{
    public Sprite image;
    public Element.TypeElement type;

    public DataElement(Sprite image, Element.TypeElement type)
    {
        this.image = image;
        this.type = type;
    }
}
