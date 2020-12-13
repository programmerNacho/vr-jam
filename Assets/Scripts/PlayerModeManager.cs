using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject combatMode;
    [SerializeField]
    private GameObject uiMode;

    private void Start()
    {
        EnterCombatMode();
    }

    public void EnterCombatMode()
    {
        combatMode.SetActive(true);
        uiMode.SetActive(false);
    }

    public void EnterUIMode()
    {
        combatMode.SetActive(false);
        uiMode.SetActive(true);
    }
}
