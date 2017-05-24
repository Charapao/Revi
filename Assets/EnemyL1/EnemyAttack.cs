using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public int attackDamage = 10;
	public float timeBetweenAttacks = 0.5f;
	bool playerInRange = false;
	float timer;

	Animator anim;
	GameObject player;
	EnemyHealth enemyHealth;
	PlayerHealth playerHealth;
	Animator playerAnim;
	PlayerAttackScript playerAttack;
	GameObject attackColliderPlayer;
	PlayerBehaviorScript playerBehavior;
	bool isEnabled=true;
	NavMeshAgent nav;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		attackColliderPlayer = GameObject.FindGameObjectWithTag("Weapon");
		enemyHealth = GetComponent<EnemyHealth>();
		playerHealth = player.GetComponent<PlayerHealth>();
		playerAnim = player.GetComponent<Animator>();
		playerAttack = attackColliderPlayer.GetComponent<PlayerAttackScript>();
		playerBehavior = player.GetComponent<PlayerBehaviorScript>();
		nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isEnabled==false){
			return;
		}
		timer += Time.deltaTime;
		// print(playerInRange+"----"+enemyHealth.currentHealth);
		if(timer >= timeBetweenAttacks && playerInRange == true && enemyHealth.currentHealth > 0){
			// print("GGG");
			Attack();
		}

		if(playerHealth.currentHealth <= 0){
			//we need to play the death animation on the player
			playerHealth.Death();
			isEnabled = false;
			anim.SetTrigger("Idle");
			nav.enabled = false;
			playerAttack.DisableAttack();
			playerBehavior.DisableMovement();
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject == player){
			playerInRange = true;
		}
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject == player){
			playerInRange = false;
		}
	}

	void Attack(){
		timer = 0f;
		anim.SetTrigger("Attack");

		if(playerHealth.currentHealth > 0){
			
			playerHealth.TakeDamage(attackDamage);

		}
	}
}
