using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private float ID=0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down *speed *Time.deltaTime);
        Destroy();
    }

    void Destroy(){
        if(transform.position.y<=-6){
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        Player miPlayer= other.GetComponent<Player>();
            switch(ID){
                case 0:
                    miPlayer.CanTripleOn();
                    break;
                case 1:
                    miPlayer.SpeedUpOn();
                    break;
                case 2:
                    miPlayer.ShieldOn();
                    break;
            }
            Debug.Log("Collision con: "+other.name);
            Destroy(this.gameObject);
        
    }
}
