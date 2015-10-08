using UnityEngine;
using System.Collections;

namespace Okky {
	public class Ninja : MonoBehaviour {
		Movement movement;
		Equipment equipment;

		bool coolDown = false;
		int tactics;

		void Awake() {
			movement = GetComponent<Movement>();
			equipment = GetComponent<Equipment>();
			InvokeRepeating("RandomWalk", 10f, 10f);
		}

		void Update() {
			movement.Up();
		}

		void RandomWalk() {
//			var rand = Random.Range(0, 6);
			//TODO
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
				break;
			case Id.Kama:
				gameObject.SendMessage("OnDie", ga);
				ga.SendMessage("OnDie", gameObject);
				break;
			}
		}
		
		void OnDie() {
			Destroy(gameObject);
		}
	}
}