using UnityEngine;
using System.Collections;

namespace Okky {
	public class Player : MonoBehaviour {
		Movement movement;
		Equipment equipment;
		Okky.KeyBind keybind;

		bool attackCoolDown = false;
		public Vector3 p1;

		public Camera camera;
		public float attackCoolDownTime;
		public GameObject buddy;
        
		void Awake() {
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
			   if (!IsAtackCoolDown()) {
					Attack();
			   }
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
			camera.transform.Translate(cameraDelta);
			transform.Translate(delta);
        }

		void Attack() {
			var ninja = FindNearestNinja();
			if (ninja != null) {
				var p = transform.position;
				var p2 = ninja.transform.position;
				var dir = (p2 - p).normalized;
				equipment.Fire(p, dir);
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

		public void OnTakeKoban(GameObject obj) {
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

		void OnDie() {
//			Destroy(gameObject);
//			gameObject.SetActive(false);
		}
	}
}