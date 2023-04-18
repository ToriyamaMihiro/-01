using System.Collections;//using = #
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //配列の宣言
    int[] map;

    void PrintArray()//デバックの表示
    {
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ",";
        }
        Debug.Log(debugText);
    }
    int GetPlayerIndex()//誤ったインデックスが帰らないようにする
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
    //二個以上ならダメにするなら
    // bool int moveTo,int pusuPower)//移動の可否
    bool MoveNumber(int number, int moveFrom, int moveTo)//移動の可否
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
            //プレイヤーの移動先から先に移動させる
            int velocity = moveTo - moveFrom;

            bool success = MoveNumber(2, moveTo, moveTo + velocity);//pusuPower-1
            //もし箱の移動に失敗したらプレイヤーの移動も失敗する
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
    //初期化 1フレームずつの処理　
    void Start()
    {
        map = new int[] { 0, 0, 0, 1, 0, 2, 0, 2, 0 };
        //デバックログの出力
        //Debug.Log("Hellow World");

        PrintArray();
    }

    // Update is called once per frame
    //更新処理　毎フレームの処理
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //見つからなかった時の溜めに-1で初期化
            int playerIndex = GetPlayerIndex();
            //要素数はアmap.Lengthで取得
            MoveNumber(1, playerIndex, playerIndex + 1);
            PrintArray();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //見つからなかった時の溜めに-1で初期化
            int playerIndex = GetPlayerIndex();
            //要素数はmap.Lengthで取得
            MoveNumber(1, playerIndex, playerIndex - 1);
            PrintArray();
        }
    }
}
