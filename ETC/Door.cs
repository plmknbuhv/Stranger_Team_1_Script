using UnityEngine;

public class Door : MonoBehaviour
{
    private bool _isEntering;
    
    public void EnterDoor()
    {
        if (!GameManager.Instance.IsActingCamera)
            GameManager.Instance.ChangeCamera();
    }
}
