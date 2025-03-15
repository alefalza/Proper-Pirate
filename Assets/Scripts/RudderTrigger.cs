using System;
using UnityEngine;

public class RudderTrigger : MonoBehaviour
{
    private BoatController _boatController;
    private PlayerController _playerController;
    private bool _isPlayerNearby;

    public Action<PlayerController> OnTrigger;

    private void Awake()
    {
        _boatController = GetComponentInParent<BoatController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerController = _playerController != null ? _playerController : other.GetComponent<PlayerController>();
            _isPlayerNearby = true;
            Debug.Log("Press [E] to pilot the boat");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerNearby = false;
        }
    }

    private void Update()
    {
        if ((_isPlayerNearby || _boatController.IsPiloting) && Input.GetKeyDown(KeyCode.E))
        {
            OnTrigger?.Invoke(_playerController);
        }
    }
}