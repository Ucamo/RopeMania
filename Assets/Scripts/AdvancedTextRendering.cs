using UnityEngine;
using System.Collections;

public class AdvancedTextRendering{

	//draw text of a specified color, with a specified outline color
	public static void DrawOutline(Rect position ,string text, GUIStyle style, Color outColor, Color inColor, float strokeSize){
		GUIStyle backupStyle = style;
		var oldColor = style.normal.textColor;

		style.normal.textColor = outColor;
		position.x -= strokeSize;
		GUI.Label(position, text, style);
		position.x += strokeSize*2;
		GUI.Label(position, text, style);
		position.x -= strokeSize;
		position.y -= strokeSize;
		GUI.Label(position, text, style);
		position.y += strokeSize*2;
		GUI.Label(position, text, style);
		position.y -= strokeSize;
		style.normal.textColor = inColor;
		GUI.Label(position, text, style);

		style.normal.textColor = oldColor;
		style = backupStyle;
	}
}
