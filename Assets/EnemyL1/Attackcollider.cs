using UnityEngine;
using System.Collections;

public class Attackcollider : MonoBehaviour {

	Collider col;
	private float timer;
	public float timebetweenAttack = 0.5f;
	public GameObject enemy;
	bool stillAttack = false;
	// Use this for initialization
	void Start () {
		enemy = GetComponent<GameObject>();
		col = GetComponent<Collider>();
		col.enabled = true;
		timer = 0;
	}
	
    void OnTriggerEnter(Collider other) {
    	
        // Destroy(other.gameObject);
        // print(timer);

        if(other.gameObject.name == "Player" && timer >=  timebetweenAttack ){
        	timer -=timebetweenAttack;
        	float damage = Random.Range(10, 12);
        	print("Hit!!" + damage);
        	PlayerHealth player = other.GetComponent<PlayerHealth>();
        	
        	player.TakeDamage(damage);
        }
        
    }
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
	}
}
