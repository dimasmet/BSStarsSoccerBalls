using UnityEngine;

public class Element : MonoBehaviour
{
    private ObjectPool _origin;

    public enum TypeElement
    {
        True,
        False
    }

    private TypeElement _currentTypeElement;

    [SerializeField] private SpriteRenderer _sprite;
    private Rigidbody2D rb;
    [SerializeField] private float _forceUp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetObjectPool(ObjectPool objectPool)
    {
        _origin = objectPool;
    }

    public void InitElement(DataElement dataElement, Vector2 position)
    {
        _currentTypeElement = dataElement.type;
        _sprite.sprite = dataElement.image;

        transform.position = position;

        rb.AddForce(Vector2.up * _forceUp);
    }

    public void ReturnToPool()
    {
        _origin.ReturnToPool(this);
    }

    public TypeElement GetTypeElement()
    {
        return _currentTypeElement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ClearObject clear))
        {
            ReturnToPool();

            if (_currentTypeElement == TypeElement.True)
            {
                GameSessionHandler.OnDiscreaseHp?.Invoke();
            }
        }
    }
}
