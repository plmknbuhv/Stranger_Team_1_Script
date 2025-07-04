using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapManager : MonoSingleton<MiniMapManager>
{
    private List<(Transform, Color, GameObject)> _mappingObjList;
    [SerializeField] private GameObject miniMapPrefab;
    [SerializeField] private RectTransform miniMap;
    [SerializeField] private Color playerMappingColor;

    private void Awake()
    {
        _mappingObjList = new List<(Transform, Color, GameObject)>();
        AddObject(GameManager.Instance.Player.gameObject, playerMappingColor);
    }

    private void Update()
    {
        ShowMiniMap();
    }

    private void ShowMiniMap()
    {
        if (GameOver.Instance._gameOverred) return;
        
        foreach (var trm in _mappingObjList)
        {
            trm.Item3.transform.localPosition = (trm.Item1.position * 3.75f) + Vector3.up * 75;
        }
    }

    public void AddObject(GameObject obj, Color color)
    {
        var mapObj = Instantiate(miniMapPrefab, miniMap);
        mapObj.GetComponent<Image>().color = color;
        _mappingObjList.Add((obj.transform, color, mapObj));
        mapObj.transform.localPosition = (obj.transform.position * 3.75f) + Vector3.up * 75;
    }

    public void RemoveObject(GameObject obj)
    {
        foreach (var tuple in _mappingObjList.ToList())
        {
            if (tuple.Item1 == obj.transform)
            {
                _mappingObjList.Remove(tuple);
                Destroy(tuple.Item3);
                return;
            }
        }
    }
}
