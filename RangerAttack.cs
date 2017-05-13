using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RangerAttack : MonoBehaviour {

	public CollisionListScript PlayerSensor;
	public CollisionListScript AttackSensor;
	public GameObject bulletCandidate;
	public GameObject troll;
	MonsterScript monster;

	public void ShootPlayer()
	{
	
	
	GameObject newBullet =  GameObject.Instantiate (bulletCandidate);
	BulletScript bullet = newBullet.GetComponent<BulletScript>();

		bullet.transform.position = troll.transform.position;
		bullet.transform.rotation = troll.transform.rotation;
		bullet.InitAndShoot (monster.FollowTarget.gameObject.transform.position);
	
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ShootPlayer ();
	}
}
