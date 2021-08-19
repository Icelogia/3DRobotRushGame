using Mirror;
using System;
using UnityEngine;

public class AnimationController : NetworkBehaviour
{
    [Header("Vehicle Components")]
    [SerializeField] private Transform poligonSphere = null;
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private PlayerInputControl inputControl = null;
    [SerializeField] private Animator animator = null;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 15f;
    private Vector3 rotationVec;


    private ElectricObstacle electricAttack = null;

    private float currentSpeed = 0;

    [ClientCallback]
    private void Update()
    {
        currentSpeed = rb.velocity.magnitude;
        animator.SetFloat("speed", currentSpeed);

        RotatePoligonSphere();
    }

    [Server]
    public void FireElectricAttack()
    {
        animator.SetTrigger("shoot");
    }

    [Client]
    private void RotatePoligonSphere()
    {   
        rotationVec = new Vector3(inputControl.verticalMovement, 0.0f, 0.0f);
        poligonSphere.Rotate(rotationVec * rotationSpeed * currentSpeed);
    }

}
