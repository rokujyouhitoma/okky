using UnityEngine;
using System.Collections;

namespace Okky {
	public class Player : MonoBehaviour {
		Movement movement;
		Equipment equipment;
		Okky.KeyBind keybind;
		SpriteRenderer spriteRenderer;

		bool attackCoolDown = false;
		public Vector3 p1;

		public Camera camera;
		public GameObject buddy;
		public GameDirector gameDirector;

		public float attackCoolDownTime;
		public float bambooSpearTime;

		public int playerId;

		GameObject bambooSpear;
		bool bambooSpearFlag = false;

		void Awake() {
			spriteRenderer = GetComponent<SpriteRenderer>();
			movement = GetComponent<Movement>();
			equipment = GetComponent<Equipment>();
			keybind = GetComponent<KeyBind>();
		}

		void Start() {
			p1 = Vector3.zero;
		}

 		void Update () {
			if (Input.GetKey(keybind.right)) {
				MoveRight();
			}
			if (Input.GetKeyUp(keybind.right)) {
				MoveCancel();
			}
			if (Input.GetKey(keybind.left)) {
				MoveLeft();
			}
			if (Input.GetKeyUp(keybind.left)) {
				MoveCancel();
			}
			if (Input.GetKey(keybind.up)) {
				MoveUp();
			}
			if (Input.GetKeyUp(keybind.up)) {
				MoveCancel();
			}
			if (Input.GetKey(keybind.down)) {
				MoveDown();
			}
			if (Input.GetKeyUp(keybind.down)) {
				MoveCancel();
			}
			if (IsAtackCoolDown()) {
				MoveCancel();
			}
			if (Input.GetKeyDown(keybind.A) || Input.GetKeyDown(keybind.B)) {
				Attack();
			}
			p1 = transform.position;
			MoveUpdate();
		}

		void MoveRight() {
			movement.Right();
		}

		void MoveLeft() {
			movement.Left();
		}

		void MoveUp() {
			movement.Up();
		}

		void MoveDown() {
			movement.Down();
		}

		void MoveCancel() {
			movement.Clear();
		}

		void MoveUpdate() {
			var delta = movement.Move();
			var cameraDelta = Vector3.zero;

			var size = spriteRenderer.bounds.size;
            var range = new Rect(0f, 0f, 220f, 200f);
			var cp = camera.transform.position;
            var p1 = gameObject.transform.position;
			var p2 = buddy.transform.position;
			var p1d = p1 + delta;

			//right
			if (cp.x + range.width/2 < p1d.x) {
				var rx = p2.x + range.width;
				var deltaXd = Mathf.Min(p1d.x, rx) - p1.x;
				var p1dx = p1.x + deltaXd; 
                var cx = p1dx - range.width/2;
				delta.x = deltaXd;
				cameraDelta.x = deltaXd;
 			}
 			//left
			if (p1d.x < cp.x - range.width/2) {
				var lx = p2.x - range.width;
				var deltaXd = Mathf.Max(p1d.x, lx) - p1.x;
				var p1dx = p1.x + deltaXd; 
				var cx = p1dx + range.width/2;
				delta.x = deltaXd;
				cameraDelta.x = deltaXd;
            }
			//up
			if (cp.y + range.height/2 < p1d.y) {
				var uy = p2.y + range.height;
				var deltaYd = Mathf.Min(p1d.y, uy) - p1.y;
				var p1dy = p1.y + deltaYd;
				var cy = p1dy - range.height/2;
				delta.y = deltaYd;
				cameraDelta.y = deltaYd;
			}
			//down
			if (p1d.y < cp.y - range.height/2) {
				var dy = p2.y - range.height;
				var deltaYd = Mathf.Max(p1d.y, dy) - p1.y;
				var p1dy = p1.y + deltaYd;
				var cy = p1dy + range.height/2;
				delta.y = deltaYd;
				cameraDelta.y = deltaYd;
            }

			//TODO
			var background = GameObject.Find("/Layer/Background");
			var sr = background.GetComponent<SpriteRenderer>();
			//right
			if (sr.bounds.size.x/2 - size.x/2 <= p1.x) {
				delta.x = delta.x * -1 * 5;
			}
			//left
			if (p1.x <= -sr.bounds.size.x/2 + size.x/2 ) {
				delta.x = delta.x * -1 * 5;
			}
			//up-down
			if (sr.bounds.size.y/2 <= p1.y || p1.y <= -sr.bounds.size.y/2 ) {
				delta.y = delta.y * -1 * 5;
			} 
 
            camera.transform.Translate(cameraDelta);
			transform.Translate(delta);
        }

		void Attack() {
			if (bambooSpearFlag) {
				return;
			}
			if (IsAtackCoolDown()) {
				return;
			}
			var ninja = FindNearestNinja();
			if (ninja != null) {
				var p = transform.position;
				var p2 = ninja.transform.position;
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

		void TurnOffBambooSpear() {
			bambooSpear.SendMessage("OnDie", gameObject);
			bambooSpearFlag = false;
		}

		GameObject FindNearestNinja() {
			var p = transform.position;
			var objs = GameObject.FindGameObjectsWithTag("Ninja");
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

		void Active() {
			MoveCancel();
			gameObject.SetActive(true);
        }

		public void OnTakeKoban(GameObject obj) {
			obj.SendMessage("OnDie", gameObject);
		}

		public void OnTakeBamboo(GameObject obj) {
			Debug.Log ("OnTakeBamboo");
			var p = transform.position;
			var dir = new Vector3(0, 1);
			var weapon = equipment.Fire(1, p);
			weapon.GetComponent<BambooSpear>().parent = gameObject;
			bambooSpear = weapon;
			bambooSpearFlag = true;
			Invoke("TurnOffBambooSpear", bambooSpearTime);
			obj.SendMessage("OnDie", gameObject);
		}

		public void OnNinja(GameObject obj) {
			SendMessage("OnDie", obj);
		}

		public void OnCube(GameObject obj) {
			//TODO
			var p = transform.position;
			var diff = (p1 - p).normalized * 5; //TODO
			p1 = transform.position = new Vector3(p.x + diff.x, p.y + diff.y, p.z);
		}

		void OnDie(GameObject obj) {
			GameDirector gd = gameDirector.GetComponent<GameDirector>();
			gd.DamageLife(playerId, 1);
			var life = gd.GetPlayerLife(playerId);
			if (0 < life) {
				Invoke("Active", 3f);
			}
//			Destroy(gameObject);
			if (bambooSpearFlag) {
				bambooSpear.SendMessage("OnDie");
			}
			gameObject.SetActive(false);
		}

		void OnTriggerEnter2D(Collider2D collider) {
			var ga = collider.gameObject;
			Id id = ga.GetComponent<Id>();			
			switch (id.type) {
			case Id.Shuriken:
                gameObject.SendMessage("OnDie", ga);
                ga.SendMessage("OnDie", gameObject);
                break;
            }
        }
	}
}
