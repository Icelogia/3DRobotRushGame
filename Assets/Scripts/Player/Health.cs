using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class Health : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private int health = 100;
    [SyncVar]
    private float currentHealth = 0;
    private bool isUpdatingSlider = true;

    private Slider healthSlider = null;

    public event Action Death;

    [ClientCallback]
    private void Start()
    {
        var hpSlider = GameObject.FindGameObjectWithTag("HP Slider");
        healthSlider = hpSlider.GetComponent<Slider>();

        currentHealth = health;
    }

    [ClientCallback]
    private void FixedUpdate()
    {
        if(!hasAuthority) { return; }

        if(isUpdatingSlider)
        {
            CmdDecreaseHealth();
            UpdateSlider();
        }

        if (currentHealth <= 0 && isUpdatingSlider)
        {
            isUpdatingSlider = false;
            if(Death != null)
            {
                Death.Invoke();
            }
        }
    }

    [Command]
    private void CmdDecreaseHealth()
    {
        currentHealth -= Time.fixedDeltaTime;
    }

    [Client]
    private void UpdateSlider()
    {
        healthSlider.value = currentHealth / health;
    }

    [Command]
    public void CmdUpdateHealth(int hp)
    {
        float currentHP = currentHealth + hp;
        if(currentHP > health)
        {
            currentHP = health;
        }

        currentHealth = currentHP;

        RPCUpdateHealth(currentHealth);
    }

    [ClientRpc]
    private void RPCUpdateHealth(float hp)
    {
        currentHealth = hp;
    }

    [Server]
    public void ServerZeroHealth()
    {
        if(currentHealth > 0)
            currentHealth = 0;
    }
}
