using UnityEngine;
using System.Collections;

namespace Okky {
	public class Ninja : MonoBehaviour {
		public Vector3 p1;

		Movement movement;
		Equipment equipment;
		GameObject camera;

		bool coolDown = false;
		int tactics;
		Dir dir;

		enum Dir {
			Right = 0,
			Left,
			Up,
			Down
		}

		void Awake() {
			movement = GetComponent<Movement>();
			equipment = GetComponent<Equipment>();
			camera = GameObject.Find("/Camera");
		}

		void Start() {
			p1 = Vector3.zero;
			dir = Dir.Right;
			RandomDir();
			InvokeRepeating("RandomDir", 1f, 2f);
			InvokeRepeating("RandomAttack", 1f, 2f);
		}

		void Update() {
			Walk();
			if(IsOutOfCamera()) {
				OnDie();
			}
			p1 = transform.position;
			var delta = movement.Move();
			transform.Translate(delta);
		}

		void RandomDir() {
			var rand = Random.Range(0, 4);
			switch(rand) {
			case 0:
				dir = Dir.Right;
				break;
			case 1:
				dir = Dir.Left;
				break;
			case 2:
				dir = Dir.Up;
				break;
			case 3:
				dir = Dir.Down;
				break;
			}
		}

		void Walk() {
			switch(dir) {
			case Dir.Right:
				movement.Right();
				break;
			case Dir.Left:
				movement.Left();
				break;
			case Dir.Up:
				movement.Up();
				break;
			case Dir.Down:
				movement.Down();
				break;
			}
		}

		bool IsOutOfCamera() {
			var p = transform.position;
			var cp = camera.transform.position;
			var range = new Rect(0f, 0f, 220f, 200f);
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
				equipment.Fire(p, dir);
			}
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
			}
		}

		public void OnCube(GameObject obj) {
			//TODO
			var p = transform.position;
			var diff = (p1 - p).normalized * 5; //TODO
			p1 = transform.position = new Vector3(p.x + diff.x, p.y + diff.y, p.z);
			RandomDir();
		}

		void OnDie() {
			Destroy(gameObject);
		}
	}
}