using UnityEngine;
using System.Collections;

public class PlayerAttackScript : MonoBehaviour {
		public Collider col;
		GameObject player;
		PlayerBehaviorScript playerBehaviorScript;
		PlayerHealth playerHealth;
		float timer;
		public float timeBetweenAttacks = 0.5f;
		bool isEnabled = true;
	// Use this for initialization
	void Start () {
		col = GetComponent<Collider>();
		player = GameObject.FindGameObjectWithTag("Player") ;
		playerBehaviorScript = player.GetComponent<PlayerBehaviorScript>();
		playerHealth = player.GetComponent<PlayerHealth>();
		col.enabled = false;
		
		

		
	}

	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		if (playerBehaviorScript.getZbutton_Attack()) {
            playerHealth.AnimateAttack();
            col.enabled = true;
            // print("Hit!!");
        }
        else{
			col.enabled = false;

        }
	}

	public void enableCol(){
		col.enabled = true;
	}

	public void disableCol(){
		col.enabled = false;
	}

	void OnTriggerEnter(Collider other) {

        // Destroy(other.gameObject);
        
        
        if(timer >= timeBetweenAttacks && other.gameObject.tag == "EnemyL1" && isEnabled == true){
        	if(playerHealth.UsingAttpotion()){
	        	print("EEEEEEEEEEEEEEEEEEEE");
	        	timer = 0f;
	        	float damage = Random.Range(30, 50)*playerHealth.levelPlayer*2;
	        	print("Hit!!" + damage);
	        	EnemyHealth enemy = other.GetComponent<EnemyHealth>();
	        	// player.AnimateAttack();
	        	if(enemy != null){
	        		enemy.TakeDamage(damage);
	        	}
        	}else{
        		print("EEEEEEEEEEEEEEEEEEEE");
	        	timer = 0f;
	        	float damage = Random.Range(30, 50)*playerHealth.levelPlayer;
	        	print("Hit!!" + damage);
	        	EnemyHealth enemy = other.GetComponent<EnemyHealth>();
	        	// player.AnimateAttack();
	        	if(enemy != null){
	        		enemy.TakeDamage(damage);
	        	}
        	}
        }
    }
    public void DisableAttack(){
    	isEnabled = false;
    }
}
