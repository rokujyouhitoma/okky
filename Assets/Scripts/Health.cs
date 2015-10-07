 using UnityEngine;
using System.Collections;

namespace Okky {
	public class Health : MonoBehaviour {

		public float score = 0;
		public float hp = 0;
		public float maxhp = 0;

 		public void Damage(float amount) {
			if (0f < hp) {
				hp = Mathf.Max(0f, hp - amount);
				if (hp <= 0f) {
					SendMessage("OnDie", gameObject);
				}
			}
		}
	}
}