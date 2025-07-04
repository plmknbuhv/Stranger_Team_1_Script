using System;
using System.Collections;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour, IPlayerComponent
{
    private PlayerAnimation _playerAnimation;
    private DoorChecker _doorChecker;
    private InputReader _inputReader;
    private Player _player;
    
    [SerializeField] private LayerMask whatIsTree;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown = 1;
    [SerializeField] private int attackDamage;

    public bool IsAttack { get; private set; }
    public bool IsAttackDelay { get; private set; }
    
    public event Action<bool> OnAttackEvent;
    
    public void Initialize(Player player)
    {
        _player = player;
        _doorChecker = player.GetCompo<DoorChecker>();
        _inputReader = player.GetCompo<InputReader>();
        _playerAnimation = player.GetCompo<PlayerAnimation>();

        _playerAnimation.OnAttackActionEvent += HandleAttackActionEvent;
        _playerAnimation.OnAttackEndEvent += HandleAttackEndEvent;
        _inputReader.OnInteractEvent += HandleInteractionEvent;
    }

    private void OnDisable()
    {
        _playerAnimation.OnAttackActionEvent -= HandleAttackActionEvent;
        _playerAnimation.OnAttackEndEvent -= HandleAttackEndEvent;
        _inputReader.OnInteractEvent -= HandleInteractionEvent;
    }

    private void HandleInteractionEvent()
    {
        if (IsAttack) return;      
        Collider2D col = _doorChecker.GetDoorInRange();
        if (col)
        {
            if(col.TryGetComponent(out Door door))
                door.EnterDoor();
        }
        else
        {
            if (IsAttackDelay) return;
            if (!GameManager.Instance.IsGround.Value) return;
            
            IsAttack = true;
            OnAttackEvent?.Invoke(IsAttack);
        }
    }
    
    private void HandleAttackActionEvent()
    {
        IsAttackDelay = true;
        RaycastHit2D hitTarget = Physics2D.Raycast(transform.position -_playerAnimation.transform.right / 2 + Vector3.up,
            -_playerAnimation.transform.right,
            attackRange, whatIsTree);
        if (hitTarget)
        {
            TreeFunction tree = hitTarget.transform.GetComponent<TreeFunction>();
            GameManager.Instance.AddWoodCurrency(tree.CutDownTree(attackDamage));
        }

        StartCoroutine(DelayCoroutine());
    }

    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        IsAttackDelay = false;
    }

    private void HandleAttackEndEvent()
    {
        IsAttack = false;
        OnAttackEvent?.Invoke(IsAttack);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // 왼쪽 선
        Gizmos.DrawLine(transform.position + Vector3.up + Vector3.left / 2,
            (transform.position + Vector3.up + Vector3.left / 2) + Vector3.left * attackRange);
        // 오른쪽 선
        Gizmos.DrawLine(transform.position + Vector3.up + Vector3.right / 2,
            (transform.position + Vector3.up + Vector3.right / 2) + Vector3.right * attackRange);
        
        Gizmos.color = Color.white;
    }
#endif
}
