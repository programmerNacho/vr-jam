using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField]
    private int damagePoints = 1;

    public int DamagePoints
    {
        get
        {
            return damagePoints;
        }

        private set
        {
            damagePoints = value;
        }
    }

    public void Attack(Health health)
    {
        health?.Attacked(damagePoints);
    }    
}
