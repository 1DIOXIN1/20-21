using UnityEngine;

public class DragPicker
{
    private Camera _camera;

    public DragPicker(Camera camera)
    {
        _camera = camera;
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