using UnityEngine;
using Mirror;

public class SlimeObstacle : MovementObstacle
{
    [SyncVar]
    [Range(0.1f, 0.9f)]
    [SerializeField] private float slowDownForce = 0.8f;

    [SerializeField] private Transform mainBone = null;
    private Vector3 mainBoneStartPosition;

    private Transform entityTransform = null;


    [ServerCallback]
    private void Start()
    {
        mainBoneStartPosition = mainBone.position;
    }

    [Server]
    override protected void ChangeMovementOf(Movement player)
    {
        player.RpcAddForce(-slowDownForce);
    }

    [ServerCallback]
    private void Update()
    {
        if(!isServer) { return; }

        if(!entityTransform && mainBone.position != mainBoneStartPosition)
        {
            float xPos = Mathf.Lerp(mainBone.position.x, mainBoneStartPosition.x, Time.deltaTime * 4);
            float zPos = Mathf.Lerp(mainBone.position.z, mainBoneStartPosition.z, Time.deltaTime * 4);
            mainBone.position = new Vector3(xPos, mainBone.position.y, zPos);
        }
           
    }

    [Server]
    private void OnTriggerEnter(Collider other)
    {
        entityTransform = other.GetComponent<Transform>();
    }

    [Server]
    private void OnTriggerStay(Collider other)
    {
        this.ChangePlayerMovement(other);

        if (entityTransform)
        {
            float xPos = Mathf.Lerp(mainBone.position.x, entityTransform.position.x, Time.deltaTime * 8);
            float zPos = Mathf.Lerp(mainBone.position.z, entityTransform.position.z, Time.deltaTime * 8);
            mainBone.position = new Vector3(xPos, mainBone.position.y, zPos);
        }
    }

    [Server]
    private void OnTriggerExit(Collider other)
    {
        entityTransform = null;
    }

}
