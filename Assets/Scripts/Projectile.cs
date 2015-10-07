using UnityEngine;
using System.Collections;

namespace Okky {
	public class Projectile : MonoBehaviour {
		public Vector2 dir;
		public float speed;
		public float range;
		public float flight;

		Movement movement;

		void Awake() {
			movement = GetComponent<Movement>();
		}

		void Update() {
			SetupMovement();
			var distance = movement.Move ();
			flight += distance.magnitude;
			if (IsOutOfRange()) {
				Die();
			}
		}

		void SetupMovement() {
			var vec = dir * speed;
			movement.vx = vec.x;
			movement.vy = vec.y;
		}

		bool IsOutOfRange() {
			if(range <= flight) {
				return true;
			}
			return false;
		}

		void Die() {
			Destroy(gameObject);
		}

		void OnDie(GameObject ga) {
			Die();
		}
	}
}