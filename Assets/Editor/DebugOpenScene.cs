using System.Collections;
using UnityEditor;

public class DebugOpenScene : EditorWindow{

	[MenuItem("OpenScene/OpenScene_Title &#%1")]
	public static void OpenScene_Title(){
		OpenScene("Title");
	}

	[MenuItem("OpenScene/OpenScene_Select &#%2")]
	public static void OpenScene_Select(){
		OpenScene("Select");
	}

	[MenuItem("OpenScene/OpenScene_SelectSpecial &#%2")]
	public static void OpenScene_SelectSpecial(){
		OpenScene("SelectSpecial");
	}

	[MenuItem("OpenScene/OpenScene_Operating &#%2")]
	public static void OpenScene_Operating(){
		OpenScene("Operating");
	}

	[MenuItem("OpenScene/OpenScene_Game &#%2")]
	public static void OpenScene_Game(){
		OpenScene("Game");
	}

	[MenuItem("OpenScene/OpenScene_Result &#%2")]
	public static void OpenScene_Result(){
		OpenScene("Result");
	}

    [MenuItem("OpenScene/OpenScene_Relay &#%2")]
    public static void OpenScene_Relay()
    {
        OpenScene("Relay");
    }

    private static void OpenScene(string scene){
		EditorApplication.OpenScene(string.Format("Assets/Scenes/{0}.unity",scene));
	}
}
