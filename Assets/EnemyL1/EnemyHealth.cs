using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public float startingHealth = 100f;
	public float currentHealth = 100f;
	// public GameObject objectToSpawn;
	public GameObject Hppotion;
	public GameObject Mppotion;
	public GameObject Attpotion;
	public GameObject Defpotion;
	bool isDead = false;
	Animator anim;
	NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		anim = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();

		// Death();
	}
	
	// Update is called once per frame
	void Update () {
		// TakeDamage(100);
	}

	public void TakeDamage(float amount){
		if(isDead){
			return;
		}

		currentHealth -= amount;
		if(currentHealth <= 0){
			Death();
		}
	}

	void Death(){
		anim.SetTrigger("Death");
		agent.speed = 0;
		isDead = true;
		Destroy(gameObject, 2f);
		float random = Random.Range(0f, 100f);
			print(random);
			if(random <=10){
			Instantiate(Hppotion,transform.position,transform.rotation);
			}
			random = Random.Range(0f, 100f);
			if(random <=10){
			Instantiate(Mppotion,transform.position,transform.rotation);
			}
			random = Random.Range(0f, 100f);
			if(random <=5){
			Instantiate(Attpotion,transform.position,transform.rotation);
			}
			random = Random.Range(0f, 100f);
			if(random <=5){
			Instantiate(Defpotion,transform.position,transform.rotation);
			}
	}
}
