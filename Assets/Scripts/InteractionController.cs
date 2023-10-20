using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private Transform _playerHand;
    [SerializeField] private float _throwPower = 100;
    
    private Camera _camera;
    private InteractableItem _previousPickedUpItem;
    private InteractableItem _previousInteractableItem;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        HighlightSelectedObject();
        ThrowForwardItem();

        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem();
            OpenCloseDoor();
        }
    }
    
    private T GetSelectedObject<T>() where T: MonoBehaviour
    {
        var playerView = Input.mousePosition;
        var ray = _camera.ScreenPointToRay(playerView);
        
        if (Physics.Raycast(ray, out var hitInfo))
        {
            var selectedObject = hitInfo.collider.gameObject.GetComponent<T>();
            return selectedObject;
        }

        return null;
    }
    
    private void HighlightSelectedObject()
    {
        var interactableItem = GetSelectedObject<InteractableItem>();

        if (_previousInteractableItem != interactableItem)
        {
            if (_previousInteractableItem != null)
            {
                _previousInteractableItem.RemoveFocus();
            }

            if (interactableItem != null)
            {
                interactableItem.SetFocus();
            }

            _previousInteractableItem = interactableItem;
        }
    }
    
    private void OpenCloseDoor()
    {
        var door = GetSelectedObject<Door>();

        if (door != null)
        {
            door.SwitchDoorState();
        }
    }

    private void PickUpItem()
    {
        var interactableItem = GetSelectedObject<InteractableItem>();

        if (interactableItem != _previousPickedUpItem)
        {
            if (_previousPickedUpItem != null)
            {
                _previousPickedUpItem.Drop();
            }

            if (interactableItem != null)
            {
                interactableItem.TakeOnHande(_playerHand);
            }

            _previousPickedUpItem = interactableItem;
        }
    }

    private void ThrowForwardItem()
    {
        if (Input.GetMouseButtonDown(0) && _previousPickedUpItem != null)
        {
            _previousPickedUpItem.ThrowAway(_camera.transform.forward, _throwPower);
            _previousPickedUpItem = null;
        }
    }
}
