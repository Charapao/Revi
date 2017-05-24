using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
////////////////////
	GameObject player;
	NavMeshAgent nav;
	////////////////////
	// float MaxHp = 100;
	// public float CurrentHp;
	// public Rigidbody rb;
	// bool isDead = false;
	// Animator anim;
	// Use this for initialization
	void Start () {

		// rb = GetComponent<Rigidbody>();
		// anim = GetComponent<Animator>();
		// CurrentHp = MaxHp;
		////////////////
		player = GameObject.FindWithTag("Player");
		nav = GetComponent<NavMeshAgent> ();
		////////////////////////
		// Death();
	}
	// public void gotHit (float damage){
	// 	if (rb != null){
	// 		CurrentHp = CurrentHp - damage;
 //           	transform.Translate(Vector3.back*10);
 //           }
	// 	if(CurrentHp <= 0){
	// 		print("CurrentHp :"+CurrentHp);
	// 		Death();
	// 		Destroy(this.gameObject,3f);
	// 	}

	// }
	// void Death (){
	// 	anim.SetTrigger("Death");
	// 	// nav.enabled = false;
	// 	nav.speed = 0;
		
	// 	isDead = true;
	// }
	// public void HitAnim(){
	// 	print("Doing");
	// 	anim.SetTrigger("Hit");
	// }
	// Update is called once per frame
	void Update () {
		if(nav.enabled == false){
			return;
		}
		nav.SetDestination(player.transform.position);
	}
}
