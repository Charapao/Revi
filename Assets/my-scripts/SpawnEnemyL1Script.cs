using UnityEngine;
using System.Collections;

public class SpawnEnemyL1Script : MonoBehaviour {

	public GameObject EnemyL1;
	public float timeToWaitBetweenSpawns = 0.02f;
	private float timer;

	// Use this for initialization
	void Start () {
		timer = 0;	
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		// print(timer+"--------"+timeToWaitBetweenSpawns);
		if(timer > timeToWaitBetweenSpawns){
			print(timer+"zzz");
			Instantiate(EnemyL1,transform.position,transform.rotation);
			// todo: spawn the objectToSpawn
			// we need to reset the timer
			
			
			timer=0;
		}
	}
}
