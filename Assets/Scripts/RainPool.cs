using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RainPool : MonoBehaviour
{
  
    [SerializeField] private GameObject[] dropsPref;
    [SerializeField] private int drop = 4;
    [SerializeField] private int timeToReturn = 5;

    private Queue<GameObject>[] pool;

    public GameObject[] DropsPref 
    {
        get => dropsPref;
    }

    private void Awake()
    {
        Spawn();
    }

    void Spawn()
    {

        pool = new Queue<GameObject>[dropsPref.Length];

        if(dropsPref.Length > 0 && dropsPref != null)
        {
            for (int i = 0; i < dropsPref.Length; i++)
            {
                pool[i] =new Queue<GameObject>();

                for (int h = 0; h < drop; h++)
                {
                    GameObject ins = Instantiate(dropsPref[i], (Vector3.up * -100), Quaternion.identity);
                    ins.SetActive(false);
                    pool[i].Enqueue(ins);
                }
            }   
        }
    }
    public GameObject RandomDrop()
    {
        GameObject result;

        int randomPool = UnityEngine.Random.Range(0, dropsPref.Length);

        if(pool[randomPool].Count > 0)
        {
            result = pool[randomPool].Dequeue();
        }
        else
        {
            result = Instantiate(dropsPref[randomPool]);
        }

        if(result != null) 
        {
            result.SetActive(true);
            result.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        StartCoroutine(Return(result, randomPool));
        return result; 
    }
    IEnumerator Return(GameObject _drop, int dropIndex)
    {
        yield return new WaitForSeconds(timeToReturn);
        if(_drop != null) _drop.SetActive(false);
        pool[dropIndex].Enqueue(_drop);
    }
}
