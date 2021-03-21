using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float velocidade;
    public float salto;

    private Rigidbody2D rig;

    private bool tanoar;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(velocidade,rig.velocity.y);

        if(Input.GetMouseButtonDown(0) && !tanoar){ //checa todas que a gente clica com o botão esquerdo do mouse
            rig.AddForce(Vector2.up * salto, ForceMode2D.Impulse);
            tanoar = true;
            

        }
        
    }
    void OnCollisionEnter2D(Collision2D colisor){
        if(colisor.gameObject.layer == 3){
            tanoar = false;
        }

    }
}
