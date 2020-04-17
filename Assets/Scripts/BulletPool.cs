using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }
    [SerializeField]private GameObject bullet;
    [SerializeField] private int ammo = 50;
    [SerializeField] private float timeToReturn = 5;

    private Queue<GameObject> charger = new Queue<GameObject>();
    

    private void Awake()
    {
        Instance = this;
        FillPool();
    }

    private void FillPool()
    {
        for (int i = 0; i < ammo; i++)
        {
            GameObject ins = Instantiate(bullet);
            ins.SetActive(false);
            charger.Enqueue(ins);
        }
    }
    public GameObject Next()
    {
        GameObject chamber = null;

        if (charger.Count > 0)
        {
            chamber = charger.Dequeue();
        }
        else
        {
            chamber = Instantiate(bullet);
        }
        
        if(chamber != null) 
        {
            chamber.SetActive(true);
            chamber.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        StartCoroutine(Return(chamber));
        return chamber;
    }    
    IEnumerator Return(GameObject _bullet)
    {
        yield return new WaitForSeconds(timeToReturn);
        if(_bullet != null) _bullet.SetActive(false);
        charger.Enqueue(_bullet);
    }
}

