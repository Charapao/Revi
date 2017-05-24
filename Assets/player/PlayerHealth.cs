using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	public float startingHealth = 100f;
	public float currentHealth = 100f;
	public float startingMagic = 100f;
	public float currentMagic = 100f;
	public float startingExp = 15f;
	public float currentExp = 0f;
	public float levelPlayer = 1f;
	public float maxlevel = 5;
	bool isDead = false;
	Animator anim;
	GameObject player;
	PlayerBehaviorScript playerBehaviorScript;
	float MaxHp = 100;
	float CurrentHp;
	public Rigidbody rb;
	public GameObject healthBar;
	public GameObject magicBar;
	public GameObject expBar;
	public Text skill_points;
	public Text hp_potionText;
	public Text mp_potionText;
	public Text att_potionText;
	public Text def_potionText;
	public Text att_potiontimeText;
	public Text def_potiontimeText;
	public Text health_Text;
	public Text magic_Text;
	public Text exp_Text;
	public Text level_Text;
	public Button footUpgrade;
	public Button swordUpgrade;
	public Button armorUpgrade;
	public Canvas skillCanvas;
	private float atttimer;
	private float deftimer;
	public bool defUsing;
	public bool attUsing;
	public float footskillsaved = 0f;
	public float swordskillsaved = 0f;
	public float armorskillsaved = 0f;
	public float skillpoints = 0f;
	public float skillpointssaved = 0f;
	float countHPpotion = 100;
	float countMPpotion = 100;
	float countATTpotion = 10;
	float countDEFpotion = 10;

	// Use this for initialization
	void Start () {
		
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		currentHealth = startingHealth;
		player = GameObject.FindWithTag("Player");
		skillCanvas = skillCanvas.GetComponent<Canvas>();
		playerBehaviorScript = player.GetComponent<PlayerBehaviorScript>();
		hp_potionText.text = ""+countHPpotion;
		mp_potionText.text = ""+countMPpotion;
		att_potionText.text = ""+countATTpotion;
		def_potionText.text = ""+countDEFpotion;
		skill_points.text = "Skill Points: "+skillpoints;
		// Death();
		SetHealthBar(currentHealth/(startingHealth*levelPlayer));
		SetMagicBar(currentMagic/(startingMagic*levelPlayer));
		SetExpBar(currentExp/startingExp);
		atttimer = 10f;	
		attUsing = false;
		deftimer = 10f;
		defUsing = false;
		skillCanvas.enabled = false;
		att_potiontimeText.enabled = false;
	}
	public void TakeDamage (float amount){
		if(isDead){
			return;
		}
		// print("currentHealth :"+currentHealth);
		// if (rb != null){
		// 	currentHealth = currentHealth - amount;
  //          	transform.Translate(Vector3.back*1);
  //          }
		// if(currentHealth <= 0){
		// 	print("currentHealth :"+currentHealth);
		// 	Death();
		// 	Destroy(this.gameObject,3f);
		// }
		print(currentHealth);
		currentHealth -= amount;
		float calc_Health = currentHealth/(startingHealth*levelPlayer); //if cur 80/100 = 0.8f
		SetHealthBar(calc_Health);
		health_Text.text = currentHealth+"/"+(startingHealth*levelPlayer);
		// if(currentHealth <= 0){
		// 	Death();

		// }


	}
	public void UsepotionMagic(float amount){
		currentMagic -= amount;
		float calc_Magic = currentMagic/(startingMagic*levelPlayer);
		SetMagicBar(calc_Magic);
		magic_Text.text = currentMagic+"/"+(startingMagic*levelPlayer);

	}
	public void Death (){
		if(isDead){
			return;
		}
		anim.SetTrigger("Death");
		isDead = true;

	}
	// Update is called once per frame
	public void AnimateAttack(){
		anim.SetTrigger("Attack");
	}
	void Update () {
		if(isDead){
			return;
		}
		if(attUsing){
			atttimer -= Time.deltaTime;
			att_potiontimeText.enabled = true;
			att_potiontimeText.text = ""+atttimer;
			
		}
		if(atttimer <= 0){
			atttimer = 10f;
			attUsing = false;
			att_potiontimeText.enabled = false;
		}
		if(defUsing){
			deftimer -= Time.deltaTime;
			def_potiontimeText.enabled = true;
			def_potiontimeText.text = ""+deftimer;
		}
		if(deftimer <= 0){
			deftimer = 10f;
			defUsing = false;
			def_potiontimeText.enabled = false;
		}
		// print(atttimer);
		if(Input.GetKeyDown("r")){
			countHPpotion--;
        	hp_potionText.text = ""+countHPpotion;
        	currentHealth += 10;
        	if(currentHealth >= (startingHealth*levelPlayer)){
        		currentHealth = (startingHealth*levelPlayer);
        	}
			float calc_Health = currentHealth/(startingHealth*levelPlayer); //if cur 80/100 = 0.8f
			SetHealthBar(calc_Health);
		}
		 if (Input.GetKeyDown("b")){
		 	countATTpotion--;
		 	att_potionText.text = ""+countATTpotion;
		 	atttimer = 10f;
		 	attUsing = true;
		 }
		 if (Input.GetKeyDown("space")){
		 	countDEFpotion--;
		 	def_potionText.text = ""+countDEFpotion;
		 	deftimer = 10f;
		 	defUsing = true;
		 }
		 if (Input.GetKeyDown("n")){
		 	countMPpotion--;
		 	mp_potionText.text = ""+countMPpotion;
		 	currentMagic += 10;
		 	if(currentMagic >= (startingMagic*levelPlayer)){
        		currentMagic = (startingMagic*levelPlayer);
        	}
        	float calc_Magic = currentMagic/(startingMagic*levelPlayer); //if cur 80/100 = 0.8f
			SetMagicBar(calc_Magic);
		 }
		 if (Input.GetKeyDown("d")){
		 	skillCanvas.enabled = true;

		 }
		 if (Input.GetKeyDown("y")){
		 	if(levelPlayer < maxlevel){
		 	currentExp++;
		 	SetExpBar(currentExp/(startingExp*levelPlayer));
		 	health_Text.text = currentHealth+"/"+(startingHealth*levelPlayer);
			exp_Text.text = currentExp+"/"+(startingExp*levelPlayer);
			if(currentExp == (startingExp*levelPlayer)){
				levelPlayer++;
				currentHealth = (startingHealth*levelPlayer);
				health_Text.text = currentHealth+"/"+(startingHealth*levelPlayer);
				float calc_Health = currentHealth/(startingHealth*levelPlayer); //if cur 80/100 = 0.8f
				SetHealthBar(calc_Health);
				currentMagic = (startingMagic*levelPlayer);
				magic_Text.text = currentMagic+"/"+(startingMagic*levelPlayer);
				float calc_Magic = currentMagic/(startingMagic*levelPlayer); //if cur 80/100 = 0.8f
				SetMagicBar(calc_Magic);
				skillpoints += 8;
				skill_points.text = "Skill Points: "+skillpoints;
				currentExp = 0;
				SetExpBar(currentExp/(startingExp*levelPlayer));
			exp_Text.text = currentExp+"/"+(startingExp*levelPlayer);
			level_Text.text = "Level : "+levelPlayer;
			}
			}

		 }

		// if (Input.GetKeyDown ("z")) {
            // AnimateAttack();
            // print("Hit!!");
        // }


	}
	public void SetExpBar(float myExp){
		expBar.transform.localScale = new Vector3( myExp, expBar.transform.localScale.y,expBar.transform.localScale.z);
	}
	public void SetHealthBar(float myHealth){
		//myhealth value 0-1 , 
		healthBar.transform.localScale = new Vector3( myHealth, healthBar.transform.localScale.y,healthBar.transform.localScale.z);
	}
	public void SetMagicBar(float myMagic){
		//myhealth value 0-1 , 
		magicBar.transform.localScale = new Vector3( myMagic, magicBar.transform.localScale.y,magicBar.transform.localScale.z);
	}
	public bool UsingAttpotion(){
		return attUsing;
	}

	void OnTriggerEnter(Collider other) {

        // Destroy(other.gameObject);
        
        
        if(other.gameObject.tag == "Hppotion"){
        	Destroy(other.gameObject);
        	countHPpotion++;
        	hp_potionText.text = ""+countHPpotion;
        	
        }
        if(other.gameObject.tag == "Mppotion"){
        	Destroy(other.gameObject);
        	countMPpotion++;
        	mp_potionText.text = ""+countMPpotion;
        	
        }
        if(other.gameObject.tag == "Attpotion"){
        	Destroy(other.gameObject);
        	countATTpotion++;
        	att_potionText.text = ""+countATTpotion;
        	
        }
        if(other.gameObject.tag == "Defpotion"){
        	Destroy(other.gameObject);
        	countDEFpotion++;
        	def_potionText.text = ""+countDEFpotion;
        	
        }
    }


}
