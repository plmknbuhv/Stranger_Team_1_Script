using UnityEngine;

public class DoorChecker : MonoBehaviour, IPlayerComponent
{
    private Player _player;
    
    [SerializeField] private LayerMask whatIsDoor;
    [SerializeField] private float checkRadius;

    public void Initialize(Player player)
    {
        _player = player;
    }

    public Collider2D GetDoorInRange()
    {
        var door = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsDoor);
        if (door)
            return door;

        return null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        Gizmos.color = Color.white;
    }
#endif
}
