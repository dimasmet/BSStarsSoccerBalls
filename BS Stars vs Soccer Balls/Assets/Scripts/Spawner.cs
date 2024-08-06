using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private ObjectPool objectPool;

    [SerializeField] private Sprite[] _falseSprites;
    [SerializeField] private Sprite _trueSprites;

    private void Start()
    {
        StartCoroutine(WaitToSpawnElement());
    }

    private IEnumerator WaitToSpawnElement()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            Vector2 posSpawn = _points[Random.Range(0, _points.Length)].position;

            Element element = objectPool.GetElement();
            element.InitElement(GetDataToElement(), posSpawn);

        }
    }

    private DataElement GetDataToElement()
    {
        float randomValue = Random.Range(0, 100);
        DataElement data;

        if (randomValue < 50)
        {
            data = new DataElement(_falseSprites[Random.Range(0, _falseSprites.Length)], Element.TypeElement.False);
        }
        else
        {
            data = new DataElement(_trueSprites, Element.TypeElement.True);
        }

        return data;
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
