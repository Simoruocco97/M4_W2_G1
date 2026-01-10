using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask terrain;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundDistance;
    private bool IsGrounded = true;

    public bool CheckIsGrounded() => IsGrounded;

    private void Awake()
    {
        terrain = LayerMask.GetMask("Terrain");
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        else if (player == null) Debug.LogWarning("Player non trovato!");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundChecker.position, groundDistance);
    }

    private void Update()
    {
        IsGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, terrain, QueryTriggerInteraction.Ignore);
    }
}