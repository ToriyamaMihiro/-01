using System.Collections;//using = #
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject BoxPrefab;
    public GameObject goalPrefab;
    public GameObject clearText;
    //�z��̐錾
    int[,] map;
    GameObject[,] field;

    void PrintArray()//�f�o�b�N�̕\��
    {
        //string debugText = "";
        //for (int i = 0; i < map.Length; i++)
        //{
        //    debugText += map[i].ToString() + ",";
        //}
        //Debug.Log(debugText);
    }
    Vector2Int GetPlayerIndex()//������C���f�b�N�X���A��Ȃ��悤�ɂ���
    {
        //GameObject obj;
        //obj.tag("Player");
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] == null)
                {
                    continue;
                }
                if (field[y, x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);

    }
   
    //��ȏ�Ȃ�_���ɂ���Ȃ�
    // bool int moveTo,int pusuPower)//�ړ��̉�
    bool MoveNumber(string tag, Vector2Int moveFrom, Vector2Int moveTo)//�ړ��̉�
    {
        //if (pusuPower <= 0)
        //{
        //    return false;
        //}

        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0))
        {
            return false;
        }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1))
        {
            return false;
        }
        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            //�v���C���[�̈ړ��悩���Ɉړ�������
            Vector2Int velocity = moveTo - moveFrom;

            bool success = MoveNumber("Box", moveTo, moveTo + velocity);//pusuPower-1
            //�������̈ړ��Ɏ��s������v���C���[�̈ړ������s����
            if (!success)
            {
                return false;
            }
        }
        field[moveFrom.y, moveFrom.x].transform.position = new Vector3(moveTo.x, field.GetLength(0) - moveTo.y, 0);
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;

        return true;
    }
  
    // Start is called before the first frame update
    //������ 1�t���[�����̏����@
    void Start()
    {
        //GameObject instance = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        map = new int[,] {
        {0,0,0,0,0},
        {0,0,1,0,0},
        {0,3,2,3,0},
        {0,2,3,2,0},
        {0,0,0,0,0}
        };
        field = new GameObject[map.GetLength(0), map.GetLength(1)];

        //�f�o�b�N���O�̏o��
        //Debug.Log("Hellow World");
       
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {

                    field[y, x] = Instantiate(playerPrefab, new Vector3(x, map.GetLength(0) - y, 0), Quaternion.identity);

                }
                else if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate(BoxPrefab, new Vector3(x, map.GetLength(0) - y, 0), Quaternion.identity);
                }
                //else if (map[y, x] == 3)
                //{
                //    field[y, x] = Instantiate(goalPrefab, new Vector3(x, map.GetLength(0) - y, 0), Quaternion.identity);
                //}
            }
           
        }

       
    }

    bool IsCleard()
    {       
        List<Vector2Int> goals = new List<Vector2Int>();
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 3)
                {
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }
        for (int i = 0; i < goals.Count; i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if (f == null || f.tag != "Box")
            {
                return false;
            }
        }
        return true;
    }


    // Update is called once per frame
    //�X�V�����@���t���[���̏���
    void Update()
    {
        if (IsCleard())
        {
            clearText.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //������Ȃ��������̗��߂�-1�ŏ�����
            Vector2Int playerIndex = GetPlayerIndex();
            Vector2Int playerIndex2 = GetPlayerIndex();

            playerIndex2.x += 1;


            //�v�f����map.Length�Ŏ擾
            MoveNumber("Player", playerIndex, playerIndex2);
            // PrintArray();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //������Ȃ��������̗��߂�-1�ŏ�����
            Vector2Int playerIndex = GetPlayerIndex();
            Vector2Int playerIndex2 = GetPlayerIndex();
            playerIndex2.x -= 1;
            //�v�f����map.Length�Ŏ擾
            MoveNumber("Player", playerIndex, playerIndex2);
            // PrintArray();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //������Ȃ��������̗��߂�-1�ŏ�����
            Vector2Int playerIndex = GetPlayerIndex();
            Vector2Int playerIndex2 = GetPlayerIndex();
            playerIndex2.y -= 1;
            //�v�f����map.Length�Ŏ擾
            MoveNumber("Player", playerIndex, playerIndex2);
            // PrintArray();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //������Ȃ��������̗��߂�-1�ŏ�����
            Vector2Int playerIndex = GetPlayerIndex();
            Vector2Int playerIndex2 = GetPlayerIndex();
            playerIndex2.y += 1;
            //�v�f����map.Length�Ŏ擾
            MoveNumber("Player", playerIndex, playerIndex2);
            // PrintArray();
        }
    }
}
