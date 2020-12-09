using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Quiver : XRSocketInteractor
{
    [SerializeField]
    private GameObject arrowPrefab = null;

    private Vector3 attachOffset = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();
        CreateAndSelectArrow();
        SetAttachOffset();
    }

    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        base.OnSelectExited(interactable);
        CreateAndSelectArrow();
    }

    private void CreateAndSelectArrow()
    {
        Arrow arrow = CreateArrow();
        SelectArrow(arrow);
    }

    private Arrow CreateArrow()
    {
        GameObject arrowObject = Instantiate(arrowPrefab, transform.position - attachOffset, transform.rotation);
        return arrowObject.GetComponent<Arrow>();
    }

    private void SelectArrow(Arrow arrow)
    {
        interactionManager.ForceSelect(this, arrow);
    }

    private void SetAttachOffset()
    {
        if(selectTarget is XRGrabInteractable interactable)
        {
            attachOffset = interactable.attachTransform.localPosition;
        }
    }
}
