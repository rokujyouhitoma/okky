using UnityEngine;
using System.Collections;

namespace Okky {
	public class Equipment : MonoBehaviour {
		public GameObject mainWeapon;
		GameObject layer;

		void Awake() {
			layer = GameObject.Find("/Layer");
		}

		public void Fire(Vector3 position, Vector3 dir) {
			var weapon = (GameObject)Instantiate(mainWeapon, position, Quaternion.identity);
			weapon.transform.parent = layer.transform;
			var projectile = weapon.GetComponent<Projectile>();
			projectile.dir = dir;
		}
	}
}