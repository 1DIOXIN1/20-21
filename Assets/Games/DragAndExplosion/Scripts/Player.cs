using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _radiusExplosion = 3f;
    [SerializeField] private float _forceExplosion = 10;
    [SerializeField] private ParticleSystem _explosionParticle;

    private DragPicker _dragPicker;
    private ExplosionShooter _shooter;
    private Camera _camera;
    
    private const KeyCode SHOOT_KEYCODE = KeyCode.Mouse1;
    private const KeyCode DRAG_KEYCODE = KeyCode.Mouse0;

    private void Awake() 
    {
        _camera = Camera.main;
        _shooter = new ExplosionShooter(_radiusExplosion, _forceExplosion, _camera, _explosionParticle);
        _dragPicker = new DragPicker(_camera);
    }

    private void Update()
    {
        HandleShooting();
        HandleDragging();
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(SHOOT_KEYCODE))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            _shooter.Shoot(ray);
        }
    }

    private void HandleDragging()
    {
        Vector2 mousePosition = Input.mousePosition;
        
        if (Input.GetKeyDown(DRAG_KEYCODE))
            _dragPicker.StartDragging(mousePosition);
        
        if (Input.GetKey(DRAG_KEYCODE))
            _dragPicker.UpdateDragging(mousePosition);
        
        if (Input.GetKeyUp(DRAG_KEYCODE))
            _dragPicker.StopDragging();
    }
}