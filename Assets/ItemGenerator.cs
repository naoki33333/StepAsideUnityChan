using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //carPrefab������
    public GameObject carPrefab;
    //coinPrefab������
    public GameObject coinPrefab;
    //conePrefab������
    public GameObject conePrefab;
    //�X�^�[�g�n�_
    private int startPos = 80;
    //�S�[���n�_
    private int goalPos = 360;
    //�A�C�e�����o��x�����͈̔�
    private float posRange = 3.4f;

    //Unity�����̐i�s������50m��܂ŃA�C�e���𐶐��i�ǉ��j
    private float generateDistance = 50f;
    //Unity�����̔w��5m���߂����A�C�e����j���i�ǉ��j
    private float destroyDistance = 5f;

    //�A�C�e���𐶐�����Z���W�i�ǉ��j
    private float lastGeneratedZ;

    //�A�C�e�����X�g�i�ǉ��j
    private List<GameObject> items = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //�����̐����ʒu��ݒ�i�ǉ��j
        lastGeneratedZ = startPos;
    }

    public void ManageItems(float unityChanZ)
    {
        //�A�C�e���̐����i�ǉ��j
        if (unityChanZ + generateDistance > lastGeneratedZ && lastGeneratedZ < goalPos)
        {
            GenerateItems(lastGeneratedZ + 15);
            lastGeneratedZ += 15;
        }

        //�A�C�e���̔j���i�ǉ��j
        DestroyOldItems(unityChanZ - destroyDistance);
    }

    void GenerateItems(float zPos)
    {
        int num = Random.Range(1, 11);
        if (num <= 2)
        {
            //�R�[����x�������Ɉ꒼���ɐ����i�ǉ��j
            for (float j = -1; j <= 1; j += 0.4f)
            {
                GameObject cone = Instantiate(conePrefab);
                cone.transform.position = new Vector3(4 * j, cone.transform.position.y, zPos);
                items.Add(cone); //���X�g�ɒǉ��i�ǉ��j
            }
        }
        else
        {
            //���[�����ƂɃA�C�e���𐶐��i�ǉ��j
            for (int j = -1; j <= 1; j++)
            {
                //�A�C�e���̎�ނ����߂�
                int item = Random.Range(1, 11);
                //�A�C�e����u��Z���W�̃I�t�Z�b�g�������_���ɐݒ�
                int offsetZ = Random.Range(-5, 6);
                //60%�R�C���z�u:30%�Ԕz�u:10%�����Ȃ�
                if (1 <= item && item <= 6)
                {
                    //�R�C���𐶐��i�ǉ��j
                    GameObject coin = Instantiate(coinPrefab);
                    coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, zPos + offsetZ);
                    items.Add(coin); //���X�g�ɒǉ��i�ǉ��j
                }
                else if (7 <= item && item <= 9)
                {
                    //�Ԃ𐶐��i�ǉ��j
                    GameObject car = Instantiate(carPrefab);
                    car.transform.position = new Vector3(posRange * j, car.transform.position.y, zPos + offsetZ);
                    items.Add(car); //���X�g�ɒǉ��i�ǉ��j
                }
            }
        }
    }

    void DestroyOldItems(float zPos)
    {
        //�A�C�e���̔j���i�ǉ��j
        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].transform.position.z < zPos)
            {
                Destroy(items[i]);
                items.RemoveAt(i); //���X�g����폜�i�ǉ��j
            }
        }
    }
}