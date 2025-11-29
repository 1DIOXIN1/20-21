using UnityEngine;

public class ExplosionShooter
{
    private float _radius;
    private float _forceExplosion;

    private Camera _camera;

    public ExplosionShooter(float radius, float forceExplosion, Camera camera)
    {
        _radius = radius;
        _forceExplosion = forceExplosion;
        _camera = camera;
    }

    public void Shoot()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit _hitInfo))
        {
            Collider[] colliders = Physics.OverlapSphere(_hitInfo.point, _radius);
            
            foreach(Collider collider in colliders)
            {
                IExplosionable interactable = collider.GetComponent<IExplosionable>();

                if(interactable != null)
                    interactable.Explosion(_hitInfo.point, _forceExplosion);
            }
        }
    }
}
