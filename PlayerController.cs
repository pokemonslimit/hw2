using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerController : MonoBehaviour {
	private Animator animatorController;
	private AudioSource WalkingSound;
	public Transform rotateYTransform;
	public Transform rotateXTransform;
	public float rotateSpeed;
	public float currentRotateX = 0;
	public float MoveSpeed;
	float currentSpeed = 0;

	public Rigidbody rigidBody;

	public JumpSensor JumpSensor;
	public float JumpSpeed;
	public GunManager gunManager;
	public FlameGunManager FGManager;
	public GameUIManager uiManager;
	public GameObject gun;
	public GameObject FG;
	public int hp = 100;

	// Use this for initialization
	void Start () {
		animatorController = this.GetComponent<Animator> ();
		WalkingSound = this.GetComponent<AudioSource> ();
		//gun = this.GetComponent<GameObject> ();
	}

	public void Hit(int value)
	{
		if (hp <= 0) {
			return;
		}

		hp -= value;
		uiManager.SetHP (hp);

		if (hp > 0) {
			uiManager.PlayHitAnimation ();
		} else {
			uiManager.PlayerDiedAnimation ();

			rigidBody.gameObject.GetComponent<Collider> ().enabled = false;
			rigidBody.useGravity = false;
			rigidBody.velocity = Vector3.zero;
			this.enabled = false;
			rotateXTransform.transform.DOLocalRotate (new Vector3 (-60, 0, 0),0.5f);
			rotateYTransform.transform.DOLocalMoveY (-1.5f,0.5f).SetRelative (true);
		}
	}

	private int Weapon=1;

	// Update is called once per frame
	void Update () 
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		if(Input.GetKeyDown(KeyCode.Z))
		{
			Weapon=1;
			gun.SetActive(true);
			FG.SetActive(false);
		}

		if(Input.GetKeyDown(KeyCode.X))
		{
			Weapon=2;
			gun.SetActive(false);
			FG.SetActive(true);

		}


		if (Input.GetMouseButton (0)) {
			if(Weapon==1)
			gunManager.TryToTriggerGun ();
			if(Weapon==2)
			FGManager.TryToTriggerFG ();
		}

		//決定鍵盤input的結果
		Vector3 movDirection = Vector3.zero;
		if (Input.GetKey (KeyCode.W)){movDirection.z += 1;WalkingSound.Play();}
		else if(WalkingSound.isPlaying){WalkingSound.Pause();}
		if (Input.GetKey (KeyCode.S)){movDirection.z -= 1;WalkingSound.UnPause();}
		else{WalkingSound.Pause();}
		if (Input.GetKey (KeyCode.D)){movDirection.x += 1;WalkingSound.UnPause();}
		else{WalkingSound.Pause();}
		if (Input.GetKey (KeyCode.A)){movDirection.x -= 1;WalkingSound.UnPause();}
		else{WalkingSound.Pause();}
		movDirection = movDirection.normalized;

		//決定要給Animator的動畫參數
		if (movDirection.magnitude == 0||!JumpSensor.IsCanJump()) {currentSpeed = 0;} 
		else {
			if (movDirection.z < 0) {currentSpeed = -MoveSpeed;} 
			else {currentSpeed = MoveSpeed;}
		}
		animatorController.SetFloat("Speed",currentSpeed);

		//轉換成世界座標的方向
		Vector3 worldSpaceDirection = movDirection.z * rotateYTransform.transform.forward +
			movDirection.x * rotateYTransform.transform.right;
		Vector3 velocity = rigidBody.velocity;
		velocity.x = worldSpaceDirection.x * MoveSpeed;
		velocity.z = worldSpaceDirection.z * MoveSpeed;

		if(Input.GetKey(KeyCode.Space)&&JumpSensor.IsCanJump())
		{
			velocity.y = JumpSpeed;
		}

		rigidBody.velocity = velocity;

		//計算滑鼠
		rotateYTransform.transform.localEulerAngles += new Vector3 (0,Input.GetAxis ("Horizontal"),0) * rotateSpeed;
		currentRotateX += Input.GetAxis ("Vertical") * rotateSpeed;

		if (currentRotateX > 90) {
			currentRotateX = 90;
		} else if (currentRotateX < -90) {
			currentRotateX = -90;
		}
		rotateXTransform.transform.localEulerAngles = new Vector3 (-currentRotateX,0,0);

	}
}
