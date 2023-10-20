using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    [SerializeField] private int _highlightIntensity = 4;
    

    private Collider _collider;
    private Rigidbody _rigidbody;
    private Outline _outline;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _outline = GetComponent<Outline>();
    }

    public void SetFocus()
    {
        _outline.OutlineWidth = _highlightIntensity;
    }
    
    public void RemoveFocus()
    {
        _outline.OutlineWidth = 0;
    }

    public void TakeOnHande(Transform playerHand)
    {
        transform.SetParent(playerHand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        _collider.isTrigger = true;
        _rigidbody.isKinematic = true;
    }

    public void Drop()
    {
        _collider.isTrigger = false;
        transform.SetParent(null);
        _rigidbody.isKinematic = false;
    }

    public void ThrowAway(Vector3 direction, float throwPower)
    {
        transform.SetParent(null);
        _collider.isTrigger = false;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(direction * throwPower);
    }
}