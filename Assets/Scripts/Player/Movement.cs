using UnityEngine;
using Mirror;
using System;

public class Movement : NetworkBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform trans;

    [Header("Movement Parametres")]
    [SyncVar]
    [SerializeField] private float forwardMovementSpeed = 1;
    [SyncVar]
    [SerializeField] private float backwardMovementSpeed = 1;
    [SyncVar]
    private float movementSpeed = 1;
    [SyncVar]
    [SerializeField] private float rotationSpeed = 1;

    private bool isMoving = false;

    private float verticalMovement;
    private float rotationMovement;

    private const float speedMultiplier = 100;

    [SerializeField] private Transform movementDirection = null;

    #region Client
    [ClientCallback]
    private void FixedUpdate()
    {
        if (!hasAuthority) { return; }

        verticalMovement = Input.GetAxis("Vertical");
        rotationMovement = Input.GetAxis("Horizontal");

        movementSpeed = verticalMovement > 0 ? forwardMovementSpeed : backwardMovementSpeed;

        Move(verticalMovement);

        isMoving = Mathf.Abs(rb.velocity.x) > 0.4 || Mathf.Abs(rb.velocity.z) > 0.4;

        rotationMovement *= isMoving ? 1 : -1;

        RpcRotate(rotationMovement);
    }

    [ClientRpc]
    public void RpcRotate(float rotationMovement)
    {
        if (!isLocalPlayer) { return; }

        if (isMoving)
        {
            var angle = new Vector3(0.0f, rotationMovement * rotationSpeed);
            trans.Rotate(angle * speedMultiplier * Time.fixedDeltaTime);
        }
    }

    [Client]
    private void Move(float verticalMovement)
    {
        var movement = MovementForce(verticalMovement);

        rb.AddForce(movement);
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
            speedMultiplier * additionalForce * movementSpeed * Time.fixedDeltaTime;

        movementForce = new Vector3(movementForce.x, 0 , movementForce.z);

        return movementForce;
    }
    
    
    #endregion


}
