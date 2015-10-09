using UnityEngine;
using System.Collections;


namespace Okky {
	public class Movement : MonoBehaviour {

		public float vx = 0f;
		public float vy = 0f;

		public float vi = 0f;
		public float a = 0f;

		public float LimitXMin = -10000f;
		public float LimitXMax = 10000f;

		void Awake() {
		}

		void Start() {
		}

		public Vector3 Move() {
 			var dt = Time.deltaTime;
			var p = transform.position; 
			var diffX = (vx * dt);
			var diffY = (vy * dt);
			var x = p.x + diffX;
    			var y = p.y + diffY;
			if (x <= LimitXMin) {
				x = LimitXMin;
				diffX = x - p.x;
			} 
			if (LimitXMax <= x) {
				x = LimitXMax;
				diffX = x - p.x;
			}
			return new Vector3(diffX, diffY, 0);
		}

		public void Right() {
			var dt = Time.deltaTime;
			var v =  vi + a * dt;
			vx = v;
		}

		public void Left() {
			var dt = Time.deltaTime;
			var v =  vi + a * dt;
			vx = -v;
		}

		public void Up() {
			var dt = Time.deltaTime;
			var v =  vi + a * dt;
			vy = v;
		}

		public void Down() {
			var dt = Time.deltaTime;
			var v =  vi + a * dt;
			vy = -v;
		}

		public void Clear() {
			vx = 0f;
			vy = 0f;
		}

	}
}