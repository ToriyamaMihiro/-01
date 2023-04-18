using System.Collections;//using = #
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //�z��̐錾
    int[] map;

    void PrintArray()//�f�o�b�N�̕\��
    {
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ",";
        }
        Debug.Log(debugText);
    }
    int GetPlayerIndex()//������C���f�b�N�X���A��Ȃ��悤�ɂ���
    {
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }
    //��ȏ�Ȃ�_���ɂ���Ȃ�
    // bool int moveTo,int pusuPower)//�ړ��̉�
    bool MoveNumber(int number, int moveFrom, int moveTo)//�ړ��̉�
    {
        //if (pusuPower <= 0)
        //{
        //    return false;
        //}

        if (moveTo < 0 || moveTo >= map.Length)
        {
            return false;
        }
        if (map[moveTo] == 2)
        {
            //�v���C���[�̈ړ��悩���Ɉړ�������
            int velocity = moveTo - moveFrom;

            bool success = MoveNumber(2, moveTo, moveTo + velocity);//pusuPower-1
            //�������̈ړ��Ɏ��s������v���C���[�̈ړ������s����
            if (!success)
            {
                return false;
            }
        }
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }
    // Start is called before the first frame update
    //������ 1�t���[�����̏����@
    void Start()
    {
        map = new int[] { 0, 0, 0, 1, 0, 2, 0, 2, 0 };
        //�f�o�b�N���O�̏o��
        //Debug.Log("Hellow World");

        PrintArray();
    }

    // Update is called once per frame
    //�X�V�����@���t���[���̏���
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //������Ȃ��������̗��߂�-1�ŏ�����
            int playerIndex = GetPlayerIndex();
            //�v�f���̓Amap.Length�Ŏ擾
            MoveNumber(1, playerIndex, playerIndex + 1);
            PrintArray();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //������Ȃ��������̗��߂�-1�ŏ�����
            int playerIndex = GetPlayerIndex();
            //�v�f����map.Length�Ŏ擾
            MoveNumber(1, playerIndex, playerIndex - 1);
            PrintArray();
        }
    }
}
