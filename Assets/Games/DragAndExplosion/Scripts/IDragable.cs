using UnityEngine;

public interface IDragable
{
    void OnDragStart(Vector3 mousePosition);
    void OnDrag(Vector3 mousePosition);
    void OnDragEnd();
}
