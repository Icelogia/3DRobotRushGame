using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System.Collections;

public class Health : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private int health = 100;
    [SyncVar]
    private float currentHealth = 0;

    private Slider healthSlider = null;

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

        CmdDecreaseHealth();
        UpdateSlider();
    }

    [Command]
    private void CmdDecreaseHealth()
    {
        currentHealth -= Time.fixedDeltaTime;
    }

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
}
