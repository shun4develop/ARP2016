﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]

public class AnimationUI : MonoBehaviour {
	
	private RectTransform rectTransform;
	private CanvasGroup canvasGroup;
	public float SLIDE_SPEED = 0.3f;
	public float FEAD_SPEED = 0.3f;
	public iTween.EaseType SLIDE_EASE_TYPE = iTween.EaseType.easeOutExpo;
	public iTween.EaseType FEAD_EASE_TYPE = iTween.EaseType.linear;
	public bool startWithActive;
	private bool move;
	void Awake () {
		rectTransform = GetComponent<RectTransform> ();
		canvasGroup = GetComponent<CanvasGroup> ();
		if (startWithActive) {
			activate ();
		} else {
			deactivate ();
		}
	}
	void Update(){
		if (transform.GetSiblingIndex () == transform.parent.childCount - 1 && !move)
			activate ();
		else
			deactivate ();
	}

	private enum Direction{
		LEFT,
		TOP,
		RIGHT,
		BOTTOM
	}
	public void slideIn(string fromPlace){
		float toValue, fromValue;

		if (fromPlace == Direction.RIGHT.ToString() || fromPlace == Direction.TOP.ToString()) {
			fromValue = 2;
			toValue = 1;
		} else {
			fromValue = 0;
			toValue = 1;
		}
		Hashtable slideInHashtable = new Hashtable ();
		slideInHashtable.Add ("from",fromValue);
		slideInHashtable.Add ("to",toValue);
		slideInHashtable.Add ("time",SLIDE_SPEED);
		slideInHashtable.Add ("easeType",SLIDE_EASE_TYPE);
		slideInHashtable.Add ("oncompletetarget",gameObject);
		slideInHashtable.Add ("oncomplete","pageInCallback");


		if (fromPlace == Direction.LEFT.ToString() || fromPlace == Direction.RIGHT.ToString()) {
			setHorizon (fromValue);
			slideInHashtable.Add ("onupdate","setHorizon");
		} else {
			setVirtical (fromValue);
			
			slideInHashtable.Add ("onupdate","setVirtical");
		}
		setAlpha (1);
		transform.SetAsLastSibling ();
		move = true;
		iTween.ValueTo (gameObject,slideInHashtable);
	}
	public void slideOut(string toPlace){
		float toValue, fromValue;
		if (toPlace == Direction.RIGHT.ToString() || toPlace == Direction.TOP.ToString()) {
			fromValue = 1;
			toValue = 2;
		} else {
			fromValue = 1;
			toValue = 0;
		}
		Hashtable slideOutHashtable = new Hashtable ();
		slideOutHashtable.Add ("from",fromValue);
		slideOutHashtable.Add ("to",toValue);
		slideOutHashtable.Add ("time",SLIDE_SPEED);
		slideOutHashtable.Add ("easeType",SLIDE_EASE_TYPE);
		slideOutHashtable.Add ("oncompletetarget",gameObject);
		slideOutHashtable.Add ("oncomplete","pageOutCallback");


		if (toPlace == Direction.LEFT.ToString() || toPlace == Direction.RIGHT.ToString()) {
			slideOutHashtable.Add ("onupdate","setHorizon");
		} else {
			slideOutHashtable.Add ("onupdate","setVirtical");
		}
		move = true;
		iTween.ValueTo (gameObject,slideOutHashtable);
	}
	public void feadOut(){
		setAlpha (1);
		setVirtical (1);
		setHorizon (1);
		Hashtable feadOutHashtable = new Hashtable ();
		feadOutHashtable.Add ("from", 1);
		feadOutHashtable.Add ("to", 0);
		feadOutHashtable.Add ("time",FEAD_SPEED);
		feadOutHashtable.Add ("easeType",FEAD_EASE_TYPE);
		feadOutHashtable.Add ("onupdate","setAlpha");
		feadOutHashtable.Add ("oncompletetarget",gameObject);
		feadOutHashtable.Add ("oncomplete","pageOutCallback");
		move = true;
		iTween.ValueTo (gameObject,feadOutHashtable);
	}
	public void feadIn(){
		setAlpha (0);
		setVirtical (1);
		setHorizon (1);

		Hashtable feadInHashtable = new Hashtable ();
		feadInHashtable.Add ("from", 0);
		feadInHashtable.Add ("to", 1);
		feadInHashtable.Add ("time",FEAD_SPEED);
		feadInHashtable.Add ("easeType",FEAD_EASE_TYPE);
		feadInHashtable.Add ("onupdate","setAlpha");
		feadInHashtable.Add ("oncompletetarget",gameObject);
		feadInHashtable.Add ("oncomplete","pageInCallback");

		transform.SetAsLastSibling ();
		move = true;
		iTween.ValueTo (gameObject,feadInHashtable);
	}
	private void setAlpha(float alpha){
		canvasGroup.alpha = alpha;
	}
	private void setVirtical(float value){
		rectTransform.anchorMin = new Vector2(0,value-1);
		rectTransform.anchorMax = new Vector2 (1, value);
	}
	private void setHorizon(float value){
		rectTransform.anchorMin = new Vector2(value -1,0);
		rectTransform.anchorMax = new Vector2 (value,1);
	}

	private void pageInCallback(){
		move = false;
		activate ();
	}
	private void pageOutCallback(){
		move = false;
		transform.SetAsFirstSibling ();
	}
	private void activate(){
		canvasGroup.interactable = true;
		transform.SetAsLastSibling ();
	}
	private void deactivate(){
		canvasGroup.interactable = false;
	}
}
