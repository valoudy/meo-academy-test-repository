using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Classes;
public class GameController : MonoBehaviour
{
    /// <summary>
    /// ������ �� GameController
    /// </summary>
    public static GameController init;

    /// <summary>
    /// ��������� �������
    /// </summary>
    public List<GameObject> Levels;
    /// <summary>
    /// ����������� �������
    /// </summary>
    private int iLevel = 0;

    private GameObject InitLEvel;
    public bool Game = false;

    public GameController()
    {
        init = this;
    }

    void Update()
    {
        if (Game)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0)) {
                RaycastHit hit; // �������� ���������� � ���������
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ����������� ���
                                                                             // �������� �� �� ������ �� ��?
                if (Physics.Raycast(ray, out hit))
                {
                    // �������� ����������� ���� ������
                    Biom BiomCenter = Level.init.Bioms.Find(
                            x => x.Vector == Vector.Center);
                    // ������ ���������� ����, ������� ����� � �����������
                    // �� ���� ���� ������
                    Biom BiomSelected = new Biom();
                    // ���� ������ �����
                    if (hit.transform.name == "Top")
                    {
                        // �������� ������ ����
                        BiomSelected = Level.init.Bioms.Find(
                            x => x.Vector == Vector.Bottom);
                    }
                    // ���� ������ ����
                    else if (hit.transform.name == "Bottom")
                    {
                        // �������� ������� ����
                        BiomSelected = Level.init.Bioms.Find(
                            x => x.Vector == Vector.Top);
                    }
                    // �����
                    else if (hit.transform.name == "Left")
                    {
                        // ������
                        BiomSelected = Level.init.Bioms.Find(
                            x => x.Vector == Vector.Right);
                    }
                    // ������
                    else if (hit.transform.name == "Right")
                    {
                        // �����
                        BiomSelected = Level.init.Bioms.Find(
                            x => x.Vector == Vector.Left);
                    }
                    // ���������� ����� ���������� �����
                    for (int iBlock = 0; iBlock < BiomSelected.Blocks.Count; iBlock++)
                    {
                        // ��������� ������� ����
                        Block BlockBiom = BiomSelected.Blocks[iBlock];
                        // ���� � ������, ������ ����
                        if (BlockBiom.Init)
                        {
                            // ����� � ������� �� ������� ����� ��������� �����, 
                            //��������� �������� ������������ �����
                            BlockBiom.SceneBlock.GetComponent<BlockTransform>().target =
                                BiomCenter.Blocks[iBlock].Parent;
                        }
                    }
                }
            }
        }
    }
    [ContextMenu("��������")]
    public void CreateLevel()
    {
        if (InitLEvel != null)
            Destroy(InitLEvel);

        if (iLevel == Levels.Count)
            return;

        if (Game)
            UIController.init.AddTimer(5);

        InitLEvel = Instantiate(Levels[iLevel], Vector3.zero, Quaternion.identity);
        Level.init.CreateLevel();
        iLevel++;
    }
    public void EndGame() {
        // �������������� �������� �����-��
        UIController.init.EndGame();
        Game = false;
    }

    public int GetILevel()
    {
        return iLevel - 1;
    }
}
