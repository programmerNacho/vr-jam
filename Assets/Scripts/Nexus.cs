using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : MonoBehaviour
{
    private Health health = null;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.OnDeath.AddListener(Dead);
    }

    private void OnDisable()
    {
        health.OnDeath.RemoveListener(Dead);
    }

    private void Dead()
    {
        FindObjectOfType<GameManager>().Lost();
    }
}
