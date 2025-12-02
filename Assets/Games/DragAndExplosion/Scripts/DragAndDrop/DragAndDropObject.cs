using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DragAndDropObject : MonoBehaviour, IDragable, IExplosionable
{
    private bool _isDragging = false;
    private Rigidbody _rigidbody;
    private Vector3 _dragOffset;

    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Explosion(Vector3 initiator, float force)
    {
        if (_isDragging) return;

        Vector3 direction = (transform.position - initiator).normalized * force;
        _rigidbody.AddForce(direction, ForceMode.Impulse);
    }

    public void OnDragStart(Vector3 worldPosition)
    {
        _isDragging = true;
        _rigidbody.isKinematic = true;
        
        _dragOffset = transform.position - worldPosition;
    }

    public void OnDrag(Vector3 worldPosition)
    {
        if (!_isDragging) return;
        
        transform.position = worldPosition + _dragOffset;
    }

    public void OnDragEnd()
    {
        _isDragging = false;
        _rigidbody.isKinematic = false;
    }
}