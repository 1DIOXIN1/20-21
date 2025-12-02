using UnityEngine;

public class DragPicker
{
    private Camera _camera;
    private IDragable _currentDragable;
    private float _zOffset;
    
    public DragPicker(Camera camera)
    {
        _camera = camera;
    }

    public void StartDragging(Vector2 cursorPosition)
    {
        _currentDragable = TryGetDragable(cursorPosition, out Vector3 hitPoint);
        
        if (_currentDragable != null)
        {
            _zOffset = _camera.WorldToScreenPoint(hitPoint).z;
            Vector3 worldPos = GetCursorWorldPosition(cursorPosition, _zOffset);
            _currentDragable.OnDragStart(worldPos);
        }
    }

    public void UpdateDragging(Vector2 cursorPosition)
    {
        if (_currentDragable != null)
        {
            Vector3 worldPos = GetCursorWorldPosition(cursorPosition, _zOffset);
            _currentDragable.OnDrag(worldPos);
        }
        else
        {
            StartDragging(cursorPosition);
        }
    }

    public void StopDragging()
    {
        if (_currentDragable != null)
        {
            _currentDragable.OnDragEnd();
            _currentDragable = null;
        }
    }

    private IDragable TryGetDragable(Vector2 cursorPosition, out Vector3 hitPoint)
    {
        hitPoint = Vector3.zero;
        Ray ray = _camera.ScreenPointToRay(cursorPosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            IDragable dragableObject = hitInfo.collider.GetComponent<IDragable>();

            hitPoint = hitInfo.point;

            return dragableObject;
        }

        return null;
    }

    private Vector3 GetCursorWorldPosition(Vector3 cursorPosition, float zDepth)
    {
        Vector3 screenPoint = new Vector3(cursorPosition.x, cursorPosition.y, zDepth);

        return _camera.ScreenToWorldPoint(screenPoint);
    }
}