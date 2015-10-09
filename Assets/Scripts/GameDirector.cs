using UnityEngine;
using System.Collections;

namespace Okky {
	public class GameDirector : MonoBehaviour {
		public float topScore = 0f;
		public float score = 0f;
		public float koban = 0f;

		int[][] mapchip = new int[][] {
			new int[] { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
		};

		Generator generator;
		public GameObject NinjaPrefab;
		GameObject camera;

		GameObject layer;

		void Awake() {
			camera = GameObject.Find("/Camera");
			layer = GameObject.Find("/Layer");
			generator = GetComponent<Generator>();
			SetupMapChip();
			InvokeRepeating("SendInNinja", 2f, 2f);
		}

		void Start() {
		}

		void Update() {
			if (!ExistKoban()) {
				AreaClear ();
			}
			if (!ExistNinja()) {
				SendInNinja();
			}
		}

		void SetupMapChip() {
			float ViewWidth = 256f;
			float ViewHeight = 480f;
			float chipWidth = 8f;
			float chipHeight = 8f;
			int y = 0;
			int x = 0;
			for(y = 0 ; y < mapchip.Length; ++y) {
				for(x = 0; x < mapchip[y].Length; ++x) {
					var chipId = mapchip[y][x];
					if (chipId != 0) {
						var prefab = generator.GenerateById(chipId);
						if (prefab != null) {
							var position = new Vector3(- ViewWidth/2 + chipWidth/2 + chipWidth * (x),
							                           ViewHeight/2 - chipHeight/2 - chipHeight * (y));
							var chip = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
							chip.transform.localScale = new Vector3(100f, 100f, 100f);
							chip.transform.parent = layer.transform;
							chip.transform.position = position;
						}
					}
				}
			}
		}

		void ResetGame() {
			Application.LoadLevel("GameMain");
		}

		public float GetScore() {
			return score;
		}

		public void AddScoreByHealth(Health health) {
			AddScore (health.score);
		}

		void AddScore(float v) {
			score += v;
			if (score > topScore) {
				topScore = score;
			}
		}

		public float GetKoban() {
			return koban;
		}

		void TakeKoban() {
			koban++;
		}

		bool ExistKoban() {
			var objs = GameObject.FindGameObjectsWithTag("Koban");
			if (objs .Length > 0) {
				return true;
			}
			return false;
		}

		bool ExistNinja() {
 			var objs = GameObject.FindGameObjectsWithTag("Ninja");
			if (objs .Length > 0) {
				return true;
			}
			return false;
		}

		void AreaClear() {
			ResetGame();
		}

		void SendInNinja() {
			var cp = camera.transform.position;
			var ninja = (GameObject)Instantiate(NinjaPrefab, Vector3.zero, Quaternion.identity);
			ninja.transform.localScale = new Vector3(100f, 200f, 100f);
			ninja.transform.parent = layer.transform;
		}
    }
}