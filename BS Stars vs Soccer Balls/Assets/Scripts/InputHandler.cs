using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Shuriken _currentShuriken;

    [SerializeField] private float _forceMove;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Clicked");

            RaycastHit2D hit;

            Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            hit = Physics2D.Raycast(mousepos, Vector3.forward);
            if (hit)
            {
                if (hit.collider.gameObject.TryGetComponent(out Shuriken sh))
                {
                    _currentShuriken = sh;
                    Debug.Log("объект: " + sh.gameObject.name);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_currentShuriken != null)
            {
                Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - _currentShuriken.transform.position).normalized;

                _currentShuriken.AddForceToMove(direction, _forceMove);
            }
        }

    }
}
