using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Shuriken _currentShuriken;

    [SerializeField] private float _forceMove;

    private bool isInput = false;

    private void Start()
    {
        GameSessionHandler.OnStartSessionGame += StartTraking;
        GameSessionHandler.OnEndGame += StopTraking;
    }

    private void OnDestroy()
    {
        GameSessionHandler.OnStartSessionGame -= StartTraking;
        GameSessionHandler.OnEndGame -= StopTraking;
    }

    private void StartTraking()
    {
        isInput = true;
    }

    private void StopTraking()
    {
        isInput = false;
    }

    private void Update()
    {
        if (isInput)
        {
            /*if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit;

                Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                hit = Physics2D.Raycast(mousepos, Vector3.forward);
                if (hit)
                {
                    if (hit.collider.gameObject.TryGetComponent(out Shuriken sh))
                    {
                        _currentShuriken = sh;
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
            }*/

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        RaycastHit2D hit;

                        Vector2 mousepos = Camera.main.ScreenToWorldPoint(touch.position);

                        hit = Physics2D.Raycast(mousepos, Vector3.forward);
                        if (hit)
                        {
                            if (hit.collider.gameObject.TryGetComponent(out Shuriken sh))
                            {
                                _currentShuriken = sh;
                            }
                        }
                        break;
                    case TouchPhase.Ended:
                        if (_currentShuriken != null)
                        {
                            Vector2 direction = (Camera.main.ScreenToWorldPoint(touch.position) - _currentShuriken.transform.position).normalized;

                            _currentShuriken.AddForceToMove(direction, _forceMove);

                            _currentShuriken = null;
                        }
                        break;
                }
            }
        }

    }
}
