using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Okky {
	public class PlayerLife : MonoBehaviour {
		public GameDirector gameDirector;
		public int playerId;

		Text textUI;

		void Start () {
			textUI = GetComponent<Text>();
		}
		
		void Update () {
			var gd = gameDirector.GetComponent<GameDirector>();
			textUI.text = gd.GetPlayerLife(playerId).ToString() ;
		}
	}
}