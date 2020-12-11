using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Arrow : XRGrabInteractable
{
    [SerializeField]
    private float speed = 20;
    [SerializeField]
    private Transform tip = null;

    private bool inAir = false;
    private Vector3 lastPosition = Vector3.zero;

    private new Rigidbody rigidbody = null;

    private Damager damager = null;

    protected override void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody>();
        damager = GetComponent<Damager>();
    }

    private void FixedUpdate()
    {
        if(inAir)
        {
            CheckForCollision();
            lastPosition = tip.position;
        }
    }

    private void CheckForCollision()
    {
        if(Physics.Linecast(lastPosition, tip.position, out RaycastHit hit))
        {
            Stop(hit.transform);
            CheckAttack(hit.transform.GetComponent<Health>());
        }
    }

    private void Stop(Transform collidedTransform)
    {
        inAir = false;
        SetPhysics(false);
        transform.SetParent(collidedTransform, true);
    }

    private void CheckAttack(Health otherHealth)
    {
        if (otherHealth)
        {
            damager.Attack(otherHealth);
        }
    }

    public void Release(float pullValue)
    {
        inAir = true;
        SetPhysics(true);

        MaskAndFire(pullValue);
        StartCoroutine(RotateWithVelocity());

        lastPosition = tip.position;
    }

    private void SetPhysics(bool usePhysics)
    {
        rigidbody.isKinematic = !usePhysics;
        rigidbody.useGravity = usePhysics;
    }

    private void MaskAndFire(float power)
    {
        colliders[0].enabled = false;
        interactionLayerMask = 1 << LayerMask.NameToLayer("Ignore");

        Vector3 force = transform.forward * (power * speed);
        rigidbody.AddForce(force, ForceMode.Impulse);
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();

        while(inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(rigidbody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }
}
