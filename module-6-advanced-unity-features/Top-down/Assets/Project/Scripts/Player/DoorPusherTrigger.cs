using UnityEngine;

public class DoorPusherTrigger : MonoBehaviour
{
    [SerializeField] private CharacterController _playerController;
    [SerializeField] private float _pushForce = 2f;
    [SerializeField] private LayerMask _interactionLayerMask;

    private void OnTriggerStay(Collider other)
    {
        if (((1 << other.gameObject.layer) & _interactionLayerMask) == 0) return;

        Rigidbody body = other.attachedRigidbody;
        if (body == null || body.isKinematic) return;
        
        Vector3 velocity = _playerController.velocity;
        if (velocity.sqrMagnitude < 0.1f) return;

        Vector3 pushDir = new Vector3(velocity.x, 0, velocity.z).normalized;
        Vector3 hitPoint = other.ClosestPoint(transform.position);

        body.AddForceAtPosition(pushDir * _pushForce, hitPoint, ForceMode.Force);
    }
}