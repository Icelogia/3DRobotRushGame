using UnityEngine;
using Mirror;

public class Movement : NetworkBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform trans;

    [Header("Movement Parametres")]
    [SyncVar]
    [SerializeField] private float movementSpeed = 1;
    [SyncVar]
    [SerializeField] private float rotationSpeed = 1;

    private const float speedMultiplier = 100;

    [SerializeField] private Transform movementDirection = null;

    #region Client
    [ClientCallback]
    private void FixedUpdate()
    {
        if (!hasAuthority) { return; }

        float verticalMovement = Input.GetAxis("Vertical");
        float rotationMovement = Input.GetAxis("Horizontal");
        
        Move(verticalMovement, rotationMovement);
    }

    [Client]
    private void Move(float verticalMovement, float rotationMovement)
    {
        
        var movement = movementDirection.forward.normalized * verticalMovement *
            speedMultiplier * movementSpeed * Time.fixedDeltaTime;

        movement = new Vector3(movement.x, rb.velocity.y, movement.z);
        rb.velocity = movement;

        var angle = new Vector3(0.0f, rotationMovement * rotationSpeed);
        trans.Rotate(angle * speedMultiplier * Time.fixedDeltaTime);
        
    }
    #endregion


}
