using UnityEngine;
using System.Collections;

namespace Okky {
	public class BambooSpear: MonoBehaviour {
		public GameObject parent;

		void Update () {
			transform.position = parent.transform.position + new Vector3(0f, 10f, 0);
		}

		void Die() {
			Destroy(gameObject);
		}
		
		void OnDie(GameObject ga) {
			Die();
		}
	}
}