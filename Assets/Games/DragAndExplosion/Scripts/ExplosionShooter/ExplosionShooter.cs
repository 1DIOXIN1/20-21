using UnityEngine;

public class ExplosionShooter
{
    private float _radius;
    private float _forceExplosion;
    private ParticleSystem _particle;

    private Camera _camera;

    public ExplosionShooter(float radius, float forceExplosion, Camera camera, ParticleSystem particle)
    {
        _radius = radius;
        _forceExplosion = forceExplosion;
        _camera = camera;
        _particle = particle;
    }

    public void Shoot(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Collider[] colliders = Physics.OverlapSphere(hitInfo.point, _radius);
            
            foreach (Collider collider in colliders)
            {
                IExplosionable interactable = collider.GetComponent<IExplosionable>();

                if (interactable != null)
                {
                    interactable.Explosion(hitInfo.point, _forceExplosion);
                    PlayParticle(hitInfo.point);
                }      
            }
        }
    }

    private void PlayParticle(Vector3 position)
    {
        _particle.transform.position = position;
        _particle.Play();
    }
}
