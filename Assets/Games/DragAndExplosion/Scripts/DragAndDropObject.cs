using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DragAndDropObject : MonoBehaviour, IDragable, IExplosionable
{
    private bool _isDragging = false;
    private Camera _camera;
    private Rigidbody _rigidbody;
    private float _dragPlaneDistance;
    private Vector3 _dragOffset;

    private void Awake() 
    {
        _camera = Camera.main;
        
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Explosion(Vector3 initiator, float force)
    {
        if (_isDragging) return;

        Vector3 direction = (transform.position - initiator).normalized * force;

        _rigidbody.AddForce(direction, ForceMode.Impulse);
    }

    public void OnDragStart(Vector3 mousePosition)
    {
        _isDragging = true;
        _rigidbody.isKinematic = true;
        
        Vector3 objectScreenPoint = _camera.WorldToScreenPoint(transform.position);

        _dragPlaneDistance = objectScreenPoint.z;

        Vector3 mouseWorldPos = GetMouseWorldPosition(mousePosition);

        _dragOffset = transform.position - mouseWorldPos;
    }

    public void OnDrag(Vector3 mousePosition)
    {
        if (_isDragging == false)
            OnDragStart(mousePosition);

        Vector3 worldPosition = GetMouseWorldPosition(mousePosition) + _dragOffset;

        transform.position = worldPosition;
    }

    public void OnDragEnd()
    {
        _isDragging = false;
        _rigidbody.isKinematic = false;
    }

    private Vector3 GetMouseWorldPosition(Vector3 mousePosition)
    {
        mousePosition.z = _dragPlaneDistance;

        return _camera.ScreenToWorldPoint(mousePosition);
    }
}