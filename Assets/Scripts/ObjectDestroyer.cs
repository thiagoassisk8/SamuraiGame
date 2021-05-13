using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour {
    public GameObject[] objectSpawn;
    public GameObject platformDescructionPoint;
    
    

    // Use this for initialization
    void Start () {
        platformDescructionPoint = GameObject.Find("PlatformDescructionPoint");        
        InvokeRepeating("Spawn", 5, 5);
        
            
    }
    
    void Spawn(){
        
        for (int i=0;i<=8;i++){
            Instantiate(objectSpawn[0], transform.position, Quaternion.identity);
        }
        
    }    
	
    // Update is called once per frame
    void Update () {
        
        if (transform.position.x < platformDescructionPoint.transform.position.x) {
            gameObject.SetActive(false);
            
        }
        
    }
}