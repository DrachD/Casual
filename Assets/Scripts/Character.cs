using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////////////////
// our main character
////////////////////////////////////////////////////////
public class Character : MonoBehaviour
{
    [SerializeField] float _speed = 6f;

    private Rigidbody _rb;
    
    [SerializeField] Camera _mainCamera;

    [SerializeField] LevelData _levelData;

    private Vector3 _velocity;

    // all picked up items
    [SerializeField] List<ActivationItems> _activationItems;

    private CameraController _cameraContoller;

    private GameMenuController _gameMenuController;

    private MenuPaused _menuPaused;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _mainCamera = FindObjectOfType<Camera>();
        _cameraContoller = _mainCamera.GetComponent<CameraController>();
        _gameMenuController = GameObject.Find("GameMenu Controller").GetComponent<GameMenuController>();
        _menuPaused = GameObject.Find("MenuManager").GetComponent<MenuPaused>();
    }

    private void Update()
    {
        _velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * _speed;

        Vector3 dis = new Vector3(_cameraContoller.distanceBetweenObjectsX, _cameraContoller.distanceBetweenObjectsY, _cameraContoller.distanceBetweenObjectsZ);
        _mainCamera.gameObject.transform.position = transform.position + dis;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _velocity * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ActivatedRedDoor"))
        {
            ActivatedDoor(ActivationItems.RedKey, other);
        }

        if (other.gameObject.CompareTag("ActivatedGreenDoor"))
        {
            ActivatedDoor(ActivationItems.GreenKey, other);
        }
    }

    // activate items with picked up items    
    private void ActivatedDoor(ActivationItems activationItem, Collision other)
    {
        for (int i = 0; i < _activationItems.Count; i++)
        {
            if (_activationItems[i] == activationItem)
            {
                _activationItems.RemoveAt(i);
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("RedKey") || obj.CompareTag("GreenKey"))
        {
            _activationItems.Add(other.gameObject.GetComponent<ActivationSubject>().ItemType);
            Destroy(other.gameObject);
        }

        if (obj.CompareTag("Finish"))
        {
            MenuState menuState = _gameMenuController.GetMenuState(State.LevelPassed);
            obj.GetComponent<LevelComplete>().NextLevel();
            menuState.gameObject.SetActive(true);

            _menuPaused.gameOver = true;
            _menuPaused.isMenuPaused = true;
        }
    }
}
