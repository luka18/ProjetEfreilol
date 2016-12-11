using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfiniteTerrain : MonoBehaviour
{
    [SerializeField]
    GameObject Coin;
    [SerializeField]
    GameObject chemin;

    [SerializeField]
    Transform player;

    [SerializeField]
    GameObject[] obtab;

    long k = 0;
    long kcoin = 0;
    long check = 50;
    float TimerObstable = 0;
    int lastrandomOB = 0;
    int lastrandom = 0;
    public int coinsstack = 8;

    int lastcoin = 0;
    float timercoin = 0;
    List<GameObject> ListGO;

    List<GameObject> ListObs;

    List<GameObject> ListGold;

    // Use this for initialization
    void Start()
    {
        
        ListGO = new List<GameObject>();
        ListObs = new List<GameObject>();
        ListGold = new List<GameObject>();
        for(int i = 0; i < 4; i++)
        {
            GameObject tmp = (GameObject)Instantiate(chemin, new Vector3(0, 0, k), Quaternion.identity);
            ListGO.Add(tmp);
            k += 50;
        }
        k -= 50;
        check = 50;
        kcoin =0;
        
        TimerObstable = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
   
        if (player.position.z > check)
        {
            k += 50;
            check += 50;
            GameObject tmp = (GameObject)Instantiate(chemin, new Vector3(0, 0, k), Quaternion.identity);
            ListGO.Add(tmp);
            GameObject tmp2 = ListGO[0];
            Destroy(tmp2);
            ListGO.RemoveAt(0);
        }
        if (Time.time > TimerObstable)
        {
            float ran = Random.Range(0.4f, 1.5f);
            TimerObstable += ran;
            SpawnObstabcle();
        }
        if (player.position.z> kcoin)
        {
            int z = 0;
            int r = Random.Range(0, 2);
            coinsstack--;
            if(coinsstack == 0)
            {
                coinsstack = 8;
                kcoin += 23;
            }
            switch (lastrandom)
            {
                case -1:
                    if (r == 0)
                        z = 0;
                    else
                        z = 1;
                    break;
                case 0:
                    if (r == 0)
                        z = -1;
                    else
                        z = 1;
                    break;
                case 1:
                    if (r == 0)
                        z = 0;
                    else
                        z = -1;
                    break;
            }
            kcoin += 7;
            GameObject tmp = (GameObject) Instantiate(Coin, new Vector3(z*5, 0, player.position.z+75 ), Quaternion.identity);
            ListGold.Add(tmp);
            if (ListGold.Count > 20)
            {
                Destroy(ListGold[0]);
                ListGold.RemoveAt(0);
            }
        }
    }
    public void DeleteGold(GameObject obj)
    {
        Destroy(obj);
        ListGold.Remove(obj);
    }
    public void ResetAll()
    {
        foreach(GameObject k in ListGO)
        {
            Destroy(k);
        }
        ListGO.Clear();
        foreach (GameObject k in ListGold)
        {
            Destroy(k);
        }
        ListGold.Clear();
        foreach (GameObject k in ListObs)
        {
            Destroy(k);
        }
        ListObs.Clear();
        k = 0;
        
        for (int i = 0; i < 4; i++)
        {
            GameObject tmp = (GameObject)Instantiate(chemin, new Vector3(0, 0, k), Quaternion.identity);
            ListGO.Add(tmp);
            k += 50;
        }
        k -= 50;
        check = 50;
        kcoin = 0;
        coinsstack = 8;
        TimerObstable = Time.time;

    }
    void SpawnObstabcle()
    {
        int i = Random.Range(0, obtab.Length );
        if(i == lastrandomOB)
            i = Random.Range(0, obtab.Length);
        lastrandomOB = i;
        int k = Random.Range(-1, 2);
        if (k == lastrandom)
            k  = Random.Range(-1, 2);
        lastrandom = k;
        
        GameObject tmp =  (GameObject)Instantiate(obtab[i], new Vector3(lastrandom*5, 1, player.position.z +75), Quaternion.identity);
        ListObs.Add(tmp);
        if (ListObs.Count > 15)
        {
            Destroy(ListObs[0]);
            ListObs.RemoveAt(0);
        }
    }

}
