using Assets.Scripts.Classes;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("���������")]
    public List<Biom> Bioms;

    [Header("�������")]
    public GameObject PrefBlock;
    public GameObject PrefMoney;

    public static Level init;
    public Level()
    {
        init = this;
    }

    [ContextMenu("��������� ������")]
    public void CreateLevel()
    {
        foreach (Biom Biom in Bioms)
        {
            foreach (Block Block in Biom.Blocks)
            {
                if (Block.Init)
                {
                    if (Biom.Vector == Vector.Center)
                    {
                        GameObject _object = Instantiate(PrefMoney, Block.Parent.position, Quaternion.identity);
                        _object.transform.parent = transform;
                        break;
                    }
                    else
                    {
                        Block.SceneBlock = Instantiate(PrefBlock, Block.Parent.position, Quaternion.identity);
                        Block.SceneBlock.transform.parent = transform;
                        Block.SceneBlock.GetComponent<BlockTransform>().VectorBlock = Biom.Vector;
                        SoundController.init.PlaySound("create");
                    }
                }
            }
        }
    }

    public bool CheckEndLevel()
    {
        // �� ������ �������
        bool EndLevel = true;
        // ��������� �����
        foreach (Biom Biom in Bioms)
        {
            // ��������� �����
            foreach (Block Block in Biom.Blocks)
            {
                // ���� ������
                if (Block.Init)
                {
                    // �� ���������� ���
                    if (Biom.Vector != Vector.Center)
                    {
                        // ���� ���� �� ����� ��� �����
                        if (Block.SceneBlock.GetComponent<BlockTransform>().EndPosition == false)
                        {
                            // ������� �� �������
                            EndLevel = false;
                        }
                    }
                }
            }
        }
        if (EndLevel)
            SoundController.init.PlaySound("compile");
        return EndLevel;
    }
}
