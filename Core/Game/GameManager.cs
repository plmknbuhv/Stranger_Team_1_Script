using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private Player _player;
    
    [SerializeField] private CinemachineVirtualCamera underGroundCamera;
    [SerializeField] private ScreenTransition screenTransition;
    
    [SerializeField] private Transform groundSpawnTrm;
    [SerializeField] private Transform underGroundSpawnTrm;
        
    public bool IsActingCamera { get; private set; }
    public NotifyValue<bool> IsGround { get; private set; } = new NotifyValue<bool>(true);

    public NotifyValue<int> woodCurrency = new NotifyValue<int>();
    public int TotalWoodCount { get; private set; }
    
    [field:SerializeField] public Transform GroundStoragePos { get; private set; }
    [field:SerializeField] public Transform UnderGroundStoragePos { get; private set; }

    #region WoodCurrency
    public void AddWoodCurrency(int wood)
    {
        woodCurrency.Value += wood;
        TotalWoodCount += wood;
    }
    
    public void UseWoodCurrency(int wood)
    {
        woodCurrency.Value -= wood;
    }
    #endregion

    #region ChagneCam
    public void ChangeCamera()
    {
        IsActingCamera = true;
        Player.GetCompo<PlayerMovement>().isCanMove = false;
        screenTransition.TransitionScreen(() =>
        {
            Player.transform.position 
                = (IsGround.Value ? underGroundSpawnTrm.position : groundSpawnTrm.position) - new Vector3(0, 0.8f, 0);
            underGroundCamera.Priority = IsGround.Value ? 11 : 9;
            IsGround.Value = !IsGround.Value;
        }, () =>
        {
            IsActingCamera = false;
            Player.GetCompo<PlayerMovement>().isCanMove = true;
        });
    }
    #endregion

    public Player Player
    {
        get
        {
            if (_player == null)
            {
                _player = FindObjectOfType<Player>();
            }

            if (_player == null)
            {
                Debug.LogWarning("하지마이씨");
            }

            return _player;
        }
    }
}
