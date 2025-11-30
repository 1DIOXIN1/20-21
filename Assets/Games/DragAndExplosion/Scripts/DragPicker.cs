using UnityEngine;

public class DragPicker : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public IDragable TryDragObject(Vector3 mousePosition)
    {
        Ray ray = _camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            IDragable dragableObject = hitInfo.collider.GetComponent<IDragable>();
            return dragableObject;
        }

        return null;
    }
}