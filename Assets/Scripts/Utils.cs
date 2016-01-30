using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

class Utils
{
	public static Sprite LoadSprite(string resourcePath)
	{
		Texture2D tex = (Texture2D)Resources.Load(resourcePath);
		Sprite sprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));

		return sprite;
	}
	
}

