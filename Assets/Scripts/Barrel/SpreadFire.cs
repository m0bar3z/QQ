using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadFire : MonoBehaviour
{
    public GameObject explosionFire;
    public int maximumFlames;
    public float fireZone;

    public void AfterExplosion()
    {
        for(int i = 1; i < maximumFlames; i++)
        {
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-fireZone, fireZone),
                                                                                                                     transform.position.y + Random.Range(-fireZone, fireZone), transform.position.z);
            Instantiate(explosionFire, pos, Quaternion.identity).GetComponent<Burnable>().Burn();
        }
    }

    void Start()
    {
        AfterExplosion();    
    }
    void Update()
    {
        
    }
}
