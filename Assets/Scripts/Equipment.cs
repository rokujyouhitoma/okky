using UnityEngine;
using System.Collections;

namespace Okky {
	public class Equipment : MonoBehaviour {
		public GameObject mainWeapon;
		GameObject gameboard;

		void Awake() {
			gameboard = GameObject.Find("/Canvas/Layer/GameBoard");
		}

		public void Fire(Vector3 position, Vector3 dir) {
			var weapon = (GameObject)Instantiate(mainWeapon, position, Quaternion.identity);
			weapon.transform.parent = gameboard.transform;
			var projectile = weapon.GetComponent<Projectile>();
			projectile.dir = dir;
		}
	}
}