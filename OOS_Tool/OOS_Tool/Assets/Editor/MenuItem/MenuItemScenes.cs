using UnityEngine;
using UnityEditor;
using System.Collections;

public class MenuItemScenes : MonoBehaviour
{

	private static string relativePathBathroom = "Assets/Scene/BATHROOM";
	private static string relativePathChildroom = "Assets/Scene/CHILDROOM";
	private static string relativePathKitchen= "Assets/Scene/KITCHEN";
	private static string relativePathLIVING01 = "Assets/Scene/LIVING/LIVING_01";
	private static string relativePathLIVING02 = "Assets/Scene/LIVING/LIVING_02";
	private static string relativePathROOM01 = "Assets/Scene/ROOM/ROOM_01";
	private static string relativePathROOM02 = "Assets/Scene/ROOM/ROOM_02";
	private static string relativePathMenuStart = "Assets/Scene/MENU/START";
	private static string relativePathMenuOptions = "Assets/Scene/MENU/OPTIONS";

	/*
	 * CHILDROOM
	 */
	[MenuItem("Load level/CHILDROOM/GA")]
	private static void GoToGAChildroom()
	{
		LoadScene(relativePathChildroom+"/GA/CHILDROOM_GA.unity");
	}
	
	[MenuItem("Load level/CHILDROOM/GD")]
	private static void GoToGDChildroom()
	{
		LoadScene(relativePathChildroom+"/GD/CHILDROOM_GD.unity");
	}
	
	[MenuItem("Load level/CHILDROOM/GDP")]
	private static void GoToGDPChildroom()
	{
		LoadScene(relativePathChildroom+"/GDP/CHILDROOM_GDP.unity");
	}
	
	[MenuItem("Load level/CHILDROOM/MSD")]
	private static void GoToMSDChildroom()
	{
		LoadScene(relativePathChildroom+"/MSD/CHILDROOM_MSD.unity");
	}
	
	[MenuItem("Load level/CHILDROOM/MAINSCENE")]
	private static void GoToMainChildroom()
	{
		Debug.Log (relativePathChildroom+"/CHILDROOM.unity");
		LoadScene(relativePathChildroom+"/CHILDROOM.unity");
	}
	
	/*
	 * KITCHEN
	 */
	[MenuItem("Load level/KITCHEN/GA")]
	private static void GoToGAKitchen()
	{
		LoadScene(relativePathKitchen+"/GA/KITCHEN_GA.unity");
	}
	
	[MenuItem("Load level/KITCHEN/GD")]
	private static void GoToGDKitchen()
	{
		LoadScene(relativePathKitchen+"/GD/KITCHEN_GD.unity");
	}
	
	[MenuItem("Load level/KITCHEN/GDP")]
	private static void GoToGDPKitchenN()
	{
		LoadScene(relativePathKitchen+"/GDP/KITCHEN_GDP.unity");
	}
	
	[MenuItem("Load level/KITCHEN/MSD")]
	private static void GoToMSDCKitchen()
	{
		LoadScene(relativePathKitchen+"/MSD/KITCHEN_MSD.unity");
	}
	
	[MenuItem("Load level/KITCHEN/MAINSCENE")]
	private static void GoToMainKitchen()
	{
		LoadScene(relativePathKitchen+"/KITCHEN.unity");
	}

	/*
	 * LIVING 01
	 */
	[MenuItem("Load level/LIVING/01/GA")]
	private static void GoToGALiving01()
	{
		LoadScene(relativePathLIVING01+"/GA/LIVING_01_GA.unity");
	}
	
	[MenuItem("Load level/LIVING/01/GD")]
	private static void GoToGDLiving01()
	{
		LoadScene(relativePathLIVING01+"/GD/LIVING_01_GD.unity");
	}
	
	[MenuItem("Load level/LIVING/01/GDP")]
	private static void GoToGDPLiving01()
	{
		LoadScene(relativePathLIVING01+"/GDP/LIVING_01_GDP.unity");
	}
	
	[MenuItem("Load level/LIVING/01/MSD")]
	private static void GoToMSDLiving01()
	{
		LoadScene(relativePathLIVING01+"/MSD/LIVING_01_MSD.unity");
	}
	
	[MenuItem("Load level/LIVING/01/MAINSCENE")]
	private static void GoToMainLiving01()
	{
		LoadScene(relativePathLIVING01+"/LIVING_01.unity");
	}


	/*
	 * LIVING 02
	 */
	[MenuItem("Load level/LIVING/02/GA")]
	private static void GoToGALiving02()
	{
		LoadScene(relativePathLIVING02+"/GA/LIVING_02_GA.unity");
	}
	
	[MenuItem("Load level/LIVING/02/GD")]
	private static void GoToGDLiving02()
	{
		LoadScene(relativePathLIVING02+"/GD/LIVING_02_GD.unity");
	}
	
	[MenuItem("Load level/LIVING/02/GDP")]
	private static void GoToGDPLiving02()
	{
		LoadScene(relativePathLIVING02+"/GDP/LIVING_02_GDP.unity");
	}
	
	[MenuItem("Load level/LIVING/02/MSD")]
	private static void GoToMSDLiving02()
	{
		LoadScene(relativePathLIVING02+"/MSD/LIVING_02_MSD.unity");
	}
	
	[MenuItem("Load level/LIVING/02/MAINSCENE")]
	private static void GoToMainLiving02()
	{
		LoadScene(relativePathLIVING02+"/LIVING_02.unity");
	}
	
	/*
	 * ROOM 01
	 */
	[MenuItem("Load level/ROOM/01/GA")]
	private static void GoToGARoom01()
	{
		LoadScene(relativePathROOM01+"/GA/ROOM_01_GA.unity");
	}
	
	[MenuItem("Load level/ROOM/01/GD")]
	private static void GoToGDRoom01()
	{
		LoadScene(relativePathROOM01+"/GD/ROOM_01_GD.unity");
	}
	
	[MenuItem("Load level/ROOM/01/GDP")]
	private static void GoToGDPRoom01()
	{
		LoadScene(relativePathROOM01+"/GDP/ROOM_01_GDP.unity");
	}
	
	[MenuItem("Load level/ROOM/01/MSD")]
	private static void GoToMSDRoom01()
	{
		LoadScene(relativePathROOM01+"/MSD/ROOM_01_MSD.unity");
	}
	
	[MenuItem("Load level/ROOM/01/MAINSCENE")]
	private static void GoToMainRoom01()
	{
		LoadScene(relativePathROOM01+"/ROOM_01.unity");
	}

	/*
	 * ROOM 02
	 */
	[MenuItem("Load level/ROOM/02/GA")]
	private static void GoToGARoom02()
	{
		LoadScene(relativePathROOM02+"/GA/ROOM_02_GA.unity");
	}
	
	[MenuItem("Load level/ROOM/02/GD")]
	private static void GoToGDRoom02()
	{
		LoadScene(relativePathROOM02+"/GD/ROOM_02GD.unity");
	}
	
	[MenuItem("Load level/ROOM/02/GDP")]
	private static void GoToGDPRoom02()
	{
		LoadScene(relativePathROOM02+"/GDP/ROOM_02_GDP.unity");
	}
	
	[MenuItem("Load level/ROOM/02/MSD")]
	private static void GoToMSDRoom02()
	{
		LoadScene(relativePathROOM02+"/MSD/ROOM_02_MSD.unity");
	}
	
	[MenuItem("Load level/ROOM/02/MAINSCENE")]
	private static void GoToMainRoom02()
	{
		LoadScene(relativePathROOM02+"/ROOM_02.unity");
	}

	/*
	 * START MENU 
	 */
    [MenuItem("Load level/MAINMENU/GA")]
	private static void GoToGAStartMenu()
	{
		LoadScene(relativePathMenuStart+"/GA/START_GA.unity");
	}

    [MenuItem("Load level/MAINMENU/GD")]
	private static void GoToGDStartMenu()
	{
		LoadScene(relativePathMenuStart+"/GD/START_GD.unity");
	}

    [MenuItem("Load level/MAINMENU/GDP")]
	private static void GoToGDPStartMenu()
	{
		LoadScene(relativePathMenuStart+"/GDP/START_GDP.unity");
	}

    [MenuItem("Load level/MAINMENU/MSD")]
	private static void GoToMSDStartMenu()
	{
		LoadScene(relativePathMenuStart+"/MSD/START_MSD.unity");
	}

    [MenuItem("Load level/MAINMENU/MAINSCENE")]
	private static void GoToMainStartMenu()
	{
		LoadScene(relativePathMenuStart+"/START.unity");
	}
	
	private static void LoadScene(string levelToLoad)
	{
		EditorApplication.SaveCurrentSceneIfUserWantsTo();
		EditorApplication.OpenScene(levelToLoad);
	}

}