using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;
using MyClass;

public class ItemData : MonoBehaviour {

	//シングルトン

	private static ItemData _instance;
	public List<Item> items;
	public Dictionary<int,Texture2D> contents;
	public Dictionary<int,Texture2D> thumbnail;

	public static ItemData instance {
		get{
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<ItemData> ();
			}
			return _instance;
		}
	}
	void Start(){
		StartCoroutine (test());
	}
	public void SortById(){
		items.Sort ((Item a,Item b)=>{return a.getId() - b.getId();});
	}
	private IEnumerator test(){
		yield return new WaitForSeconds (30);
	}
	public void SetItems(List<Item> items){
		this.items = items;
		SortById ();
		contents = new Dictionary<int, Texture2D> ();
		thumbnail = new Dictionary<int, Texture2D> ();

		foreach(Item item in items){
			//Contents
			int id = item.getId();
			Action<Texture2D> success_func_contents = (Texture2D tex)=>{
				contents.Add(id,tex);
			};
			Action failure_func_contents = () => {
				//contents.Add(item.getId(),failure_tex);
			};
			WebManager.instance.getResources (success_func_contents,failure_func_contents,item.getFilepath());

			//Thumbnail
			Action<Texture2D> success_func_thumbnail = (Texture2D tex)=>{
				thumbnail.Add(id,tex);
			};
			Action failure_func_thumbnail = () => {
				//thumbnail.Add(item.getId(),failure_tex);
			};
			WebManager.instance.getResources (success_func_thumbnail,failure_func_thumbnail,item.getThumbnail());
		}
	}


	public Item getItemById(int id){
		for (int i = 0; i < items.Count; i++) {
			if (items [i].getId() == id) {
				return items [i];
			}
		}
		return null;
	}
	public Texture2D getContentsTexture2DById(int id){
		Texture2D tex;
		contents.TryGetValue (id,out tex);
		if (tex)
			return tex;
		else {
			Action<Texture2D> success= (Texture2D resource) => {
				contents.Add(id,tex);
			};

			WebManager.instance.getResources (success,getItemById(id).getFilepath());
		}
		return null;

	}
	public Texture2D getThumbnailTexture2DById(int id){
		Texture2D tex;
		thumbnail.TryGetValue (id,out tex);
		if (tex)
			return tex;
		else {
			Action<Texture2D> success= (Texture2D resource) => {
				thumbnail.Add(id,resource);
			};

			WebManager.instance.getResources (success,getItemById(id).getThumbnail());
		}
		return null;

	}
}
