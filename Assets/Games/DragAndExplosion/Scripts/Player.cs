using UnityEngine;

[RequireComponent(typeof(ExplosionShooter), typeof(DragPicker))]
public class Player : MonoBehaviour
{
    private DragPicker _dragHandler;

    private const KeyCode SHOOT_KEYCODE_NAME = KeyCode.Mouse1;
    private const KeyCode DRAG_KEYCODE_NAME = KeyCode.Mouse0;

    private ExplosionShooter _shooter;
    private IDragable _currentDragable;

    private void Awake() 
    {
        _shooter = GetComponent<ExplosionShooter>();
        _dragHandler = GetComponent<DragPicker>();
    }

    private void Update()
    {
        HandleShooting();
        HandleDragging();
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(SHOOT_KEYCODE_NAME))
            _shooter.Shoot();
    }

    private void HandleDragging()
    {
        if (Input.GetKeyDown(DRAG_KEYCODE_NAME))
        {
            _currentDragable = _dragHandler.TryDragObject(Input.mousePosition);

            if (_currentDragable != null)
            {
                _currentDragable.OnDragStart(Input.mousePosition);
            }
        }

        if (Input.GetKey(DRAG_KEYCODE_NAME) && _currentDragable != null)
        {
            _currentDragable.OnDrag(Input.mousePosition);
        }

        if (Input.GetKeyUp(DRAG_KEYCODE_NAME) && _currentDragable != null)
        {
            _currentDragable.OnDragEnd();
            _currentDragable = null;
        }
    }
}