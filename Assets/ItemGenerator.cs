using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //conePrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPos = 80;
    //ゴール地点
    private int goalPos = 360;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    //Unityちゃんの進行方向に50m先までアイテムを生成（追加）
    private float generateDistance = 50f;

    //アイテムを生成するZ座標（追加）
    private float lastGeneratedZ;

    //アイテムリスト（追加）
    private List<GameObject> items = new List<GameObject>();

    void Start()
    {
        //初期の生成位置を設定（追加）
        lastGeneratedZ = startPos;
    }

    public void ManageItems(float unityChanZ)
    {
        //アイテムの生成（追加）
        if (unityChanZ + generateDistance > lastGeneratedZ && lastGeneratedZ < goalPos)
        {
            GenerateItems(lastGeneratedZ + 15);
            lastGeneratedZ += 15;
        }
    }

    void GenerateItems(float zPos)
    {
        int num = Random.Range(1, 11);
        if (num <= 2)
        {
            //コーンをx軸方向に一直線に生成（追加）
            for (float j = -1; j <= 1; j += 0.4f)
            {
                GameObject cone = Instantiate(conePrefab);
                cone.transform.position = new Vector3(4 * j, cone.transform.position.y, zPos);
                items.Add(cone); //リストに追加（追加）
            }
        }
        else
        {
            //レーンごとにアイテムを生成（追加）
            for (int j = -1; j <= 1; j++)
            {
                //アイテムの種類を決める
                int item = Random.Range(1, 11);
                //アイテムを置くZ座標のオフセットをランダムに設定
                int offsetZ = Random.Range(-5, 6);
                //60%コイン配置:30%車配置:10%何もなし
                if (1 <= item && item <= 6)
                {
                    //コインを生成（追加）
                    GameObject coin = Instantiate(coinPrefab);
                    coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, zPos + offsetZ);
                    items.Add(coin); //リストに追加（追加）
                }
                else if (7 <= item && item <= 9)
                {
                    //車を生成（追加）
                    GameObject car = Instantiate(carPrefab);
                    car.transform.position = new Vector3(posRange * j, car.transform.position.y, zPos + offsetZ);
                    items.Add(car); //リストに追加（追加）
                }
            }
        }
    }

    public List<GameObject> GetGeneratedItems() //生成されたアイテムリストを取得するメソッド（追加）
    {
        return items;
    }
}
