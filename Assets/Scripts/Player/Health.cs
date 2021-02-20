using UnityEngine;
using Mirror;
using UnityEngine.UI;

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

        CmdUpdateHealth();
    }

    [Command]
    private void CmdUpdateHealth()
    {
        currentHealth -= Time.fixedDeltaTime;
        
        UpdateSlider();
    }

    [TargetRpc]
    private void UpdateSlider()
    {
        healthSlider.value = currentHealth / health;
    }

    [Command]
    public void CmdIncreaseHealth(int hp)
    {
        float currentHP = currentHealth + hp;
        if(currentHP > health)
        {
            currentHP = health;
        }

        currentHealth = currentHP;

        RPCIncreaseHealth(currentHealth);
    }

    [ClientRpc]
    private void RPCIncreaseHealth(float hp)
    {
        currentHealth = hp;
    }
}
