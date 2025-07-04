using System;
using ObjectPooling;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private float moveSpeed = 5f;
    
    private Player _player;
    private Rigidbody2D _rbCompo;
    private InputReader _inputReader;
    private PlayerInteraction _playerInteraction;

    [HideInInspector] public bool isCanMove = true;
    
    public event Action<Vector2> OnMovementEvent;
    
    public void Initialize(Player player)
    {
        _player = player;
        _inputReader = player.GetCompo<InputReader>();
        _playerInteraction = player.GetCompo<PlayerInteraction>();
        _rbCompo = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (!isCanMove || _playerInteraction.IsAttack)
        {
            _rbCompo.velocity = Vector2.zero;
            OnMovementEvent?.Invoke(Vector2.zero);
            return;
        }
        
        var moveDir = new Vector2(_inputReader.MoveDir.x * moveSpeed, _rbCompo.velocity.y);
        _rbCompo.velocity = moveDir;
        if (Mathf.Abs(moveDir.x) > 0 && GameManager.Instance.IsGround.Value)
        {
            var effect = PoolManager.Instance.Pop(PoolingType.WalkEffect) as EffectPlayer;
            effect.SetPositionAndPlay(transform.position + Vector3.up * 0.2f);
        }
        OnMovementEvent?.Invoke(moveDir);
    }
}
