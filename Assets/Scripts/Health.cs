using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealthPoints = 10;
    [SerializeField]
    private int initialHealthPoints = 1;

    public UnityEvent OnDeath = new UnityEvent();

    private int currentHealthPoints = 0;

    public bool IsAlive { get; private set; } = true;

    private void Awake()
    {
        currentHealthPoints = initialHealthPoints;
    }

    private void OnDestroy()
    {
        OnDeath.RemoveAllListeners();
    }

    public void Attacked(int damagePoints)
    {
        if(IsAlive)
        {
            currentHealthPoints -= damagePoints;

            if (currentHealthPoints <= 0)
            {
                currentHealthPoints = 0;
                IsAlive = false;
                OnDeath.Invoke();
                OnDeath.RemoveAllListeners();
            }
        }
    }

    public void Healed(int healingPoints)
    {
        if(IsAlive)
        {
            currentHealthPoints += healingPoints;
            currentHealthPoints = Mathf.Clamp(currentHealthPoints, 0, maxHealthPoints);
        }
    }
}
