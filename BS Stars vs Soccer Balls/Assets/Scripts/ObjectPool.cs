using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Element _prefabs;
    private Stack<Element> _objectPool = new Stack<Element>();

    public Element GetElement()
    {
        Element element;

        if (_objectPool.Count > 0)
        {
            element = _objectPool.Pop();
            element.transform.SetParent(null);
            element.gameObject.SetActive(true);
        }
        else
        {
            element = Instantiate(_prefabs);
            element.SetObjectPool(this);

            Debug.Log("Create");
        }

        return element;
    }

    public void ReturnToPool(Element element)
    {
        element.gameObject.SetActive(false);
        element.transform.SetParent(transform);

        element.transform.localPosition = Vector3.zero;
        element.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        element.transform.localEulerAngles = Vector3.one;

        _objectPool.Push(element);
    }
}


