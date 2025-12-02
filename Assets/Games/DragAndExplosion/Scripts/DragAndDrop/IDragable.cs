using UnityEngine;

public interface IDragable
{
    void OnDragStart(Vector3 cursorPosition);
    void OnDrag(Vector3 cursorPosition);
    void OnDragEnd();
}
