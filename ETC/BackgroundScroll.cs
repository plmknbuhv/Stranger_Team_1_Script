using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float _parallaxOffset;

    private SpriteRenderer _spriteRenderer;
    private Material _backgroundMat;

    private float _currentScroll;
    private float _ratio;

    private Transform _mainCamTrm;
    private float _beforePosition;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _backgroundMat = _spriteRenderer.material;
        _currentScroll = 0;

        _ratio = 1f / _spriteRenderer.bounds.size.x;
    }

    private void Start()
    {
        _mainCamTrm = Camera.main.transform;
        _beforePosition = _mainCamTrm.position.x;
    }

    private void Update()
    {
        float delta = _mainCamTrm.position.x - _beforePosition;
        _beforePosition = _mainCamTrm.position.x;

        _currentScroll += delta * _parallaxOffset * _ratio;
        
        _backgroundMat.mainTextureOffset = new Vector2(_currentScroll, 0);
    }
}