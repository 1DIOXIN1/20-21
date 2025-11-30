using UnityEngine;

public class ExplosionShooter : MonoBehaviour
{
    [SerializeField] private float _radius = 3f;
    [SerializeField] private float _forceExplosion = 10;
    [SerializeField] private ParticleSystem _particlePrefab;

    private Camera _camera;

    public ExplosionShooter(float radius, float forceExplosion, Camera camera)
    {
        _radius = radius;
        _forceExplosion = forceExplosion;
        _camera = camera;
    }

    private void Awake() 
    {
        _camera = Camera.main;
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
                {
                    interactable.Explosion(_hitInfo.point, _forceExplosion);

                    PlayParticle(_hitInfo.point);
                }      
            }
        }
    }

    private void PlayParticle(Vector3 position)
    {
        _particlePrefab.transform.position = position;
        _particlePrefab.Play();
    }
}
