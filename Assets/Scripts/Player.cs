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
		public float attackCoolDownTime;
		public GameObject buddy;
        
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
			   if (!IsAtackCoolDown()) {
					Attack();
			   }
			}
			if (IsOutsideOfRect()) {
			}
            if (IsOutsideOfCamera()) {
                transform.position = p1;
				MoveCancel();
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
			var diff = movement.Move();
			transform.Translate(diff);
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

		bool IsOutsideOfCamera() {
			return !IsInsideOfCamera();
		}

		bool IsInsideOfCamera() {
			var p = transform.position;
			var width = spriteRenderer.bounds.size.x;
			var height = spriteRenderer.bounds.size.y;
			var cp = camera.transform.position;
			var cr = camera.rect;
			var cameraWidth = cr.width;
			var cameraHeight = cr.height;
			if (cp.x + width - cameraWidth/2 <= p.x && p.x <= cp.x - width + cameraWidth/2 &&
				cp.y + height - cameraHeight/2 <= p.y && p.y <= cp.y - height + cameraHeight/2) {
				return true;
			}
			return false;
		}

		bool IsOutsideOfRect() {
			return !IsInsideOfRect();
		}

		bool IsInsideOfRect() {
			Rect range = new Rect(0f, 0f, 40f, 40f); //TODO
            var p = gameObject.transform.position;
			var cp = camera.transform.position;
            if (cp.x - range.width/2 <= cp.x && p.x <= cp.x + range.width/2 &&
			    cp.y - range.height/2 <= p.y && p.y <= cp.y + range.height/2) {
				return true;
            }
            return false;
		}

		Vector3 GetCenter(Vector3 p1) {
			var p = transform.position;
			var center = (p + p1) / 2;
			return center;
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