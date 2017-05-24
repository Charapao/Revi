using UnityEngine;
using System.Collections;

public class detectAttack : MonoBehaviour {
		Collider col;
	// Use this for initialization
	void Start () {
		
		col = GetComponent<Collider>();
		col.enabled = false;
	}
	
    void OnTriggerEnter(Collider other) {
        // Destroy(other.gameObject);
        
        if(other.gameObject.name == "EnemyL1"){
        	float damage = Random.Range(30, 50);
        	print("Hit!!" + damage);
        	// EnemyMovement enemy = other.GetComponent<EnemyMovement>();
        	// enemy.gotHit(damage);
        }
    }
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("z")) {
            col.enabled = true;
            // print("Hit!!");
        }else{
			col.enabled = false;

        }
	}
}
