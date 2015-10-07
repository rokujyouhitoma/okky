using UnityEngine;
using System.Collections;

namespace Okky {
	public class Equipment : MonoBehaviour {
		public GameObject mainWeapon;

		public void Fire(Vector3 position, Vector3 dir) {
			var weapon = (GameObject)Instantiate(mainWeapon, position, Quaternion.identity);
			var projectile = weapon.GetComponent<Projectile>();
			projectile.dir = dir;
		}
	}
}