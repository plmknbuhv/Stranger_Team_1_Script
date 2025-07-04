using System;
using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeSpawner : MonoBehaviour
{
    private static List<(Transform, TreeFunction)> treeSpawnPointAndTree = new List<(Transform, TreeFunction)>();

    private void Awake()
    {
        foreach (Transform trm in transform)
            treeSpawnPointAndTree.Add((trm, null));
    }

    private void Start()
    {
        SpawnTrees(2);
    }

    public void SpawnTrees(int treeCount)
    {
        if (!GameOver.Instance._gameOverred)
            StartCoroutine(SpawnTreeCoroutine(treeCount));
    }

    private IEnumerator SpawnTreeCoroutine(int treeCount)
    {
        for (int i = 0; i < treeCount; i++)
        {
            if (CheckCanSpawnTree())
            {
                TreeFunction tree = PoolManager.Instance.Pop(PoolingType.Tree) as TreeFunction;
                SetTreePosition(tree);
                yield return new WaitForSeconds(Random.Range(3f, 6f));
            }
        }
    }

    private bool CheckCanSpawnTree()
    {
        foreach (var treeStruct in treeSpawnPointAndTree)
        {
            if(treeStruct.Item2 == null)
                return true;
        }
        return false;
    }

    private void SetTreePosition(TreeFunction tree)
    {
        int randomIndex;
        while (!GameOver.Instance._gameOverred)
        {
            randomIndex = Random.Range(0, treeSpawnPointAndTree.Count);
            (Transform, TreeFunction) treeStruct = treeSpawnPointAndTree[randomIndex];
            if (treeStruct.Item2 == null)
            {
                tree.transform.position = new Vector3(treeStruct.Item1.position.x + Random.Range(-0.85f, 0.85f), tree.transform.position.y);
                treeSpawnPointAndTree[randomIndex] = (treeStruct.Item1, tree);
                break;
            }
        }
    }

    public static void RemoveTree(TreeFunction tree)
    {
        for (int i = 0; i < treeSpawnPointAndTree.Count; i++)
        {
            if (treeSpawnPointAndTree[i].Item2 == tree)
            {
                treeSpawnPointAndTree[i] = (treeSpawnPointAndTree[i].Item1, null);
                break;
            }
        }
    }
}
