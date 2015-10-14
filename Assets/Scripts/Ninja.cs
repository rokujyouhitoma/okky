using UnityEngine;
using System.Collections;

namespace Okky {
	public class Ninja : MonoBehaviour {
		public Vector3 p1;

		Movement movement;
		Equipment equipment;
		GameObject camera;

		Vector3 dir;

		public float attackCoolDownTime;
		bool attackCoolDown = false;

		void Awake() {
			movement = GetComponent<Movement>();
			equipment = GetComponent<Equipment>();
			camera = GameObject.Find("/Camera");
		}

		void Start() {
			p1 = Vector3.zero;
			dir = Vector3.zero;
			RandomDir();
			InvokeRepeating("RandomDir", 1f, 2f);
			InvokeRepeating("RandomAttack", 1f, Random.Range(2, 5));
		}

		void Update() {
			Walk();
			if(IsOutOfCamera()) {
				OnDie();
			}
			if (IsAtackCoolDown()) {
				MoveCancel();
			}
			p1 = transform.position;
			Move ();
		}

		void MoveCancel() {
			movement.Clear();
		}

		void Move() {
			var delta = movement.Move();
			transform.Translate(delta);
		}

		void RandomDir() {
			var randX = Random.Range(-100 , 100) / 100f;
			var randY = Random.Range(-100, 0) / 100f;
			dir = (new Vector3(randX, randY, 0)).normalized;
		}

		void ReverseDir() {
			dir = (new Vector3(-dir.x, -dir.y, dir.z)).normalized;
		}

		void Walk() {
			var dt = Time.deltaTime;
			movement.vy = dir.y * (movement.vi + movement.a * dt);
			movement.vx = dir.x * (movement.vi + movement.a * dt);
		} 

		bool IsOutOfCamera() {
			var p = transform.position;
			var cp = camera.transform.position;
			var range = new Rect(0f, 0f, 220f, 220f);
			if (cp.x + range.width/2 <= p.x ||
			    p.x <= cp.x - range.width/2 ||
			    cp.y + range.height/2 <= p.y ||
			    p.y <= cp.y - range.height/2) {
				return true;
			}
			return false;
		}

		void RandomAttack() {
			var rand = Random.Range(0, 3);
			if(rand != 0) {
				Attack();
			}
		}

		void Attack() {
			var player = FindNearestPlayer();
			if (player != null) {
				var p = transform.position;
				var p2 = player.transform.position;
				var dir = (p2 - p).normalized;
				var weapon = equipment.Fire(0, p);
				var projectile = weapon.GetComponent<Projectile>();
				projectile.dir = dir;
				TurnOnAttackCoolDown();
				Invoke("TurnOffAttackCoolDown", attackCoolDownTime);
			}
		}

		void TurnOnAttackCoolDown() {
			attackCoolDown = true;
		}
		
		void TurnOffAttackCoolDown() {
			attackCoolDown = false;
		}
		
		bool IsAtackCoolDown() {
			return attackCoolDown;
		}

		GameObject FindNearestPlayer() {
			var p = transform.position;
			var objs = GameObject.FindGameObjectsWithTag("Player");
			GameObject nearest = null;
			float distance = 0f;
			foreach (var obj in objs) {
				var p2 = obj.transform.position;
				float d = Vector3.Distance(p2, p);
				if (d < distance || distance == 0) {
					distance = d;
					nearest = obj;
				}
			}
			return nearest;
		}

		void OnTriggerEnter2D(Collider2D collider) {
			var ga = collider.gameObject;
			Id id = ga.GetComponent<Id>();

			switch (id.type) {
			case Id.Player1:
			case Id.Player2:
				ga.SendMessage("OnNinja", gameObject);
				gameObject.SendMessage("OnDie", ga);
				break;
			case Id.Kama:
				gameObject.SendMessage("OnDie", ga);
				ga.SendMessage("OnDie", gameObject);
				break;
			case Id.BambooSpear:
				gameObject.SendMessage("OnDie", ga);
				break;
			}
		}

		public void OnCube(GameObject obj) {
			//TODO
			var p = transform.position;
			var diff = (p1 - p).normalized * 5; //TODO
			p1 = transform.position = new Vector3(p.x + diff.x, p.y + diff.y, p.z);
			ReverseDir();
		}

		void OnDie() {
			Destroy(gameObject);
		}
	}
}