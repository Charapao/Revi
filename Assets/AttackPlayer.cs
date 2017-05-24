using UnityEngine;
using System.Collections;

public class AttackPlayer : MonoBehaviour {
		public Collider col;
		// public EnemyMovement emeny;
	// Use this for initialization
	void Start () {
		
		col = GetComponent<Collider>();
		// enemy = GetComponent<EnemyMovement>();
		col.enabled = false;

	}
	
    void OnTriggerEnter(Collider other) {
      
        if(other.gameObject.name == "Player"){

        	float damage = Random.Range(10, 15);
        	print("Hit!!" + damage);
        	PlayerHealth player = other.GetComponent<PlayerHealth>();
        	
        	// enemy.HitAnim();
        	player.TakeDamage(damage);
        }
    }
	// Update is called once per frame
	void Update () {
		col.enabled = true;
	}
}
