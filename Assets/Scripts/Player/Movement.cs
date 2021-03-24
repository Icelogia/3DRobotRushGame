using UnityEngine;
using Mirror;
using System.Collections;

public class Movement : NetworkBehaviour
{
    [Header("Main Parametres")]
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private Transform trans = null;
    [SerializeField] private PlayerInputControl inputControl = null;
    [SerializeField] private Transform movementDirection = null;

    [Header("Movement Parametres")]
    [SyncVar]
    [SerializeField] private float forwardMovementSpeed = 1;
    [SyncVar]
    [SerializeField] private float backwardMovementSpeed = 1;
    [SyncVar]
    private float movementSpeed = 1;
    [SyncVar]
    [SerializeField] private float rotationSpeed = 1;

    [Header("Charge Parametres")]
    [SyncVar]
    [SerializeField] private float chargeSpeed = 1;
    [SyncVar]
    [SerializeField] private bool canCharge = false;
    [SyncVar]
    [SerializeField] private float chargeCooldown = 1;
    [SyncVar]
    private float currentChargeTime = 0;
    [SyncVar]
    private float chargeBoost = 1;



    public bool isMoving = false;

    private float verticalMovement;
    private float rotationMovement;
    private bool chargeInput;

    private const float speedMultiplier = 100;

    [ClientCallback]
    private void Update()
    {
        if (!hasAuthority) { return; }
        if (!isLocalPlayer) { return; }

        verticalMovement = inputControl.verticalMovement;
        rotationMovement = inputControl.rotationMovement;
        chargeInput = inputControl.charge;
    }

    [ClientCallback]
    private void FixedUpdate()
    {
        if (!hasAuthority) { return; }
        if (!isLocalPlayer) { return; }

        movementSpeed = verticalMovement > 0 ? forwardMovementSpeed : backwardMovementSpeed;
        Move(verticalMovement);

        isMoving = Mathf.Abs(rb.velocity.x) > 0.4 || Mathf.Abs(rb.velocity.z) > 0.4;
        rotationMovement *= isMoving ? 1 : -1;
        ClientRotate(rotationMovement);

        Charge();
    }

    [Client]
    private void Charge()
    {
        SetCharge();
        if (canCharge && chargeInput)
        {
            canCharge = false;
            currentChargeTime = 0;
            StartCoroutine(ChargeBoost());
        }
    }

    [Client]
    private void SetCharge()
    {
        if(currentChargeTime >= chargeCooldown)
        {
            canCharge = true;
        }

        currentChargeTime += Time.fixedDeltaTime;
    }

    private IEnumerator ChargeBoost()
    {
        chargeBoost = chargeSpeed;
        yield return new WaitForSeconds(2);
        chargeBoost = 1;

    }

    [Client]
    private void ClientRotate(float rotationMovement)
    {
        Rotate(rotationMovement);
    }

    [ClientRpc]
    public void RpcRotate(float rotationMovement)
    {
        Rotate(rotationMovement);
    }

    private void Rotate(float rotationMovement)
    {
        if (!isLocalPlayer) { return; }

        if (isMoving)
        {
            var angle = new Vector3(0.0f, rotationMovement * verticalMovement * rotationSpeed);
            trans.Rotate(angle * speedMultiplier * Time.fixedDeltaTime);
        }
    }

    [Client]
    private void Move(float verticalMovement)
    {
        if (rb.velocity.magnitude <= movementSpeed * chargeBoost)
        {
            var movement = MovementForce(verticalMovement);

            rb.AddForce(movement);
        }
    }

    [ClientRpc]
    public void RpcAddForce(float force)
    {
        if(!isLocalPlayer) { return; }

        if (isMoving)
        {
            var movementAdditionalForce = MovementForce(force) * verticalMovement;

            rb.AddForce(movementAdditionalForce);
        }
    }

    [Client]
    private Vector3 MovementForce(float additionalForce)
    {
        var movementForce = movementDirection.forward.normalized *
            speedMultiplier * additionalForce * movementSpeed * chargeBoost * Time.fixedDeltaTime;

        movementForce = new Vector3(movementForce.x, 0 , movementForce.z);

        return movementForce;
    }

    [TargetRpc]
    public void TRpcDrown(NetworkConnection target)
    {
        if(rb.velocity.y < -0.1f)
        {
            rb.velocity = Vector3.zero;
        }
        Vector3 buoyancy = new Vector3(-Physics.gravity.x, -Physics.gravity.y - 1f, -Physics.gravity.z);
        rb.AddForce(buoyancy);
    }
}
