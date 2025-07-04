using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimation : MonoBehaviour, IPlayerComponent
{
    private PlayerInteraction _playerInteraction;
    private PlayerMovement _playerMoveCompo;
    private Animator _animator;
    private Player _player;
    
    private readonly int _velocityHash = Animator.StringToHash("Velocity");
    private readonly int _isAttackHash = Animator.StringToHash("IsAttack");
    
    public event Action OnAttackEndEvent;
    public event Action OnAttackActionEvent;
    public UnityEvent OnWalkEvent;
    
    public void Initialize(Player player)
    {
        _player = player;
        _animator = GetComponent<Animator>();
        _playerMoveCompo = player.GetCompo<PlayerMovement>();
        _playerInteraction = player.GetCompo<PlayerInteraction>();
        
        _playerMoveCompo.OnMovementEvent += HandleMovementEvent;
        _playerInteraction.OnAttackEvent += HandleAttackEvent;
    }
    
    private void OnDestroy()
    {
        _playerMoveCompo.OnMovementEvent -= HandleMovementEvent;
        _playerInteraction.OnAttackEvent -= HandleAttackEvent;
    }

    private void HandleAttackEvent(bool isAttack)
    {
        _animator.SetBool(_isAttackHash, isAttack);
    }

    private void HandleMovementEvent(Vector2 velocity)
    {
        _animator.SetFloat(_velocityHash, Mathf.Abs(velocity.x));
    }

    public void AttackEndTrigger()
    {
        OnAttackEndEvent?.Invoke();
    }

    public void AttackActionTrigger()
    {
        OnAttackActionEvent?.Invoke();
    }   

    public void WalkSoundTrigger()
    {
        OnWalkEvent?.Invoke();
    }
}
