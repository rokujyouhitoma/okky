using UnityEngine;
using System.Collections;

namespace Okky {
	public class Player : MonoBehaviour {
		Movement movement;
		Equipment equipment;
		Okky.KeyBind keybind;

		bool attackCoolDown = false;
		Vector3 p1;

		public float attackCoolDownTime;

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
			movement.Move();
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
			var diff = (p1 - p).normalized * 4; //TODO
			p1 = transform.position = new Vector3(p.x + diff.x, p.y + diff.y, p.z);
		}

		void OnDie() {
			Destroy(gameObject);
		}
	}
}