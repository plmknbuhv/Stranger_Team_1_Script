using UnityEngine;

public class PlayerRenderer : MonoBehaviour, IPlayerComponent
{
    private PlayerMovement _playerMoveCompo;
    private SpriteRenderer _spriteRenderer;
    private Player _player;

    public void Initialize(Player player)
    {
        _player = player;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMoveCompo = player.GetCompo<PlayerMovement>();
        _playerMoveCompo.OnMovementEvent += HandleFlipEvent;
    }

    private void HandleFlipEvent(Vector2 velocity)
    {
        if (velocity.x > 0)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        if (velocity.x < 0)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
