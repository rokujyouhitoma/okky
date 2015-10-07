using UnityEngine;
using System.Collections;

namespace Okky {
	public class Player : MonoBehaviour {
		Movement movement;
		Equipment equipment;
		Okky.KeyBind keybind;

		void Awake() {
			movement = GetComponent<Movement>();
			equipment = GetComponent<Equipment>();
			keybind = GetComponent<KeyBind>();
		}

		void Start() {
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
			if (Input.GetKeyDown(keybind.A) || Input.GetKeyDown(keybind.B)) {
				Atack();
			}
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

		public void OnTakeKoban(GameObject obj) {
			obj.SendMessage("OnDie", gameObject);
		}

		void Atack() {
			var ninja = FindNearestNinja();
			if (ninja != null) {
				var p = transform.position;
				var p2 = ninja.transform.position;
				var dir = (p2 - p).normalized;
				equipment.Fire(p, dir);
			}
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
	}
}