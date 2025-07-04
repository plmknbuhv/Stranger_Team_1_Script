using System.Collections;
using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    private DoorChecker _doorChecker;
    private Player _player;

    private RectTransform _interactionRectTransform;
    private TextMeshProUGUI _interactionText;
    private GameObject _interactionObj;
        
    [SerializeField] private Vector3 iconPosition;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        StartCoroutine(FindPlayerCoroutine());
    }

    private void Update()
    {
        UpdateInteract();
    }

    private IEnumerator FindPlayerCoroutine()
    {
        yield return new WaitUntil(() => GameManager.Instance.Player != null);
        _player = GameManager.Instance.Player;
        AfterFindPlayer();
    }

    private void AfterFindPlayer()
    {
        _interactionObj = transform.Find("Interaction").gameObject;
        _interactionText = _interactionObj.transform.Find("InteractionText").GetComponent<TextMeshProUGUI>();
        _interactionRectTransform = _interactionObj.transform.GetComponent<RectTransform>();
        
        _doorChecker = _player.GetCompo<DoorChecker>();
    }

    private void UpdateInteract()
    {
        if (_player == null) return;
        // _interactionRectTransform.position = _mainCamera.WorldToScreenPoint(_player.transform.position + iconPosition);
        _interactionRectTransform.position = _player.transform.position + iconPosition;
        
        Collider2D door = _doorChecker.GetDoorInRange();
        if (door)
            _interactionObj.SetActive(true);
        else
            _interactionObj.SetActive(false);
    }
}
