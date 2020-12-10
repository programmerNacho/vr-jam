using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Bow : MonoBehaviour
{
    private Animator animator = null;
    private Puller puller = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        puller = GetComponentInChildren<Puller>();
    }

    private void Update()
    {
        AnimateBow(puller.PullAmount);
    }

    private void AnimateBow(float value)
    {
        animator.SetFloat("Tension", value);
    }
}
