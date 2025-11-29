using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraModeSwitcher : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> _cameras;

    private const KeyCode SWITCH_NAME_KEYCODE = KeyCode.F;
    private Queue<CinemachineVirtualCamera> _camerasQueue;

    private void Awake() 
    {
        _camerasQueue = new Queue<CinemachineVirtualCamera>(_cameras);
    }

    private void Update()
    {
        if(Input.GetKeyDown(SWITCH_NAME_KEYCODE))
            SwitchNextMode();
    }

    private void SwitchNextMode()
    {
        CinemachineVirtualCamera nextMode = _camerasQueue.Dequeue();

        foreach (CinemachineVirtualCamera camera in _cameras)
        {
            camera.gameObject.SetActive(false);
        }

        nextMode.gameObject.SetActive(true);

        _camerasQueue.Enqueue(nextMode);
    }
}
