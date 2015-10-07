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

		void Start() {
		}

		void Update() {
		}

		public Vector3 Move() {
			var dt = Time.deltaTime;
			var p = transform.localPosition; 
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
			transform.localPosition = new Vector3(x, y, p.z);
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

		public void Top() {
			var dt = Time.deltaTime;
			var v =  vi + a * dt;
			vy = v;
		}

		public void Bottom() {
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