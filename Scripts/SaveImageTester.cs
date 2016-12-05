using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MyLibrary;
using MyManagers;

public class SaveImageTester : MonoBehaviour {
	public Image test;
	// Use this for initialization
	void Start () {
		//SaveDataManager.saveUserIcon (test.sprite.texture);
		test.sprite = SaveDataManager.loadUserIcon();
	}
}
