using UnityEngine;
using System.Collections;
namespace MyLibrary{
	public class Base64Converter{
		public static Sprite convertSpriteFromBase64(string base64String)
		{
			Texture2D tex = convertTexture2DFromBase64 (base64String);
			//texをSpriteに変換します
			Sprite sp = Sprite.Create (tex, new Rect (0, 0, tex.width, tex.height), new Vector2 (0.5f, 0.5f));
			return sp;
		}
		public static Texture2D convertTexture2DFromBase64(string base64String){
			//文字列をbase64としてByte型に変換
			byte[] bytes = System.Convert.FromBase64String(base64String);
			//Texture2Dを作成 ※引数の数字は関係ないっぽい
			Texture2D tex = new Texture2D(1,1);
			//texにByte型になっているイメージを読み込む
			//ここで初めて画像になります
			tex.LoadImage(bytes);
			return tex;
		}

		public static string convertTexture2DToBase64(Texture2D tex){
			string base64data = System.Convert.ToBase64String (tex.GetRawTextureData());
			return base64data;
		}
		public static string convertSpriteToBase64(Sprite s){
			string base64data = System.Convert.ToBase64String (s.texture.GetRawTextureData());
			return base64data;
		}
	}
}