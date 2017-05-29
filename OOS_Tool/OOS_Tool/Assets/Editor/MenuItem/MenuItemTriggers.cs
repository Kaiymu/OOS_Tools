using UnityEngine;
using UnityEditor;
using System.Collections;

public class MenuItemTriggers : MonoBehaviour
{
    [MenuItem("Custom Objects/TRIGGERS/GD/Force Movement On Path")]
    private static void CreateForceMovementOnPath()
    {
        Object o = Resources.Load("Prefabs/Triggers/TriggerForceMovementOnPath");
        InstantiateObject(o);
    }

    [MenuItem("Custom Objects/TRIGGERS/GD/Look At")]
    private static void CreateLookAt()
    {
        Object o = Resources.Load("Prefabs/Triggers/TriggerLookAt");
        InstantiateObject(o);
    }

	[MenuItem("Custom Objects/TRIGGERS/GD/Moving Object")]
    private static void CreateMovingObject()
    {
        Object o = Resources.Load("Prefabs/Triggers/TriggerMovingObject");
        InstantiateObject(o);
    }

	[MenuItem("Custom Objects/TRIGGERS/GD/Multi Path")]
    private static void CreateMultiPath()
    {
        Object o = Resources.Load("Prefabs/Triggers/TriggerMultiPath");
        InstantiateObject(o);
    }

	[MenuItem("Custom Objects/TRIGGERS/GD/Ignore Direction")]
	private static void CreateIgnoreDirection()
	{
		Object o = Resources.Load("Prefabs/Triggers/TriggerIgnoreOneDirection");
		InstantiateObject(o);
	}

	[MenuItem("Custom Objects/TRIGGERS/GD/Shake")]
    private static void CreateShake()
    {
        Object o = Resources.Load("Prefabs/Triggers/TriggerShake");
        InstantiateObject(o);
    }

	[MenuItem("Custom Objects/TRIGGERS/GD/Zoom")]
    private static void CreateZoom()
    {
        Object o = Resources.Load("Prefabs/Triggers/TriggerZoom");
        InstantiateObject(o);
    }

	[MenuItem("Custom Objects/TRIGGERS/GD/Mum/Change state mum angry")]
	private static void CreateStateMumAngry()
	{
		Object o = Resources.Load("Prefabs/Triggers/TriggerStateMumAngry");
		InstantiateObject(o);
	}

	[MenuItem("Custom Objects/TRIGGERS/GD/Mum/Change state mum neutral")]
	private static void CreateStateMumNeutal()
	{
		Object o = Resources.Load("Prefabs/Triggers/TriggerStateMumNeutral");
		InstantiateObject(o);
	}

	[MenuItem("Custom Objects/TRIGGERS/GD/Mum/Change state mum happy")]
	private static void CreateStateMumHappy()
	{
		Object o = Resources.Load("Prefabs/Triggers/TriggerStateMumHappy");
		InstantiateObject(o);
	}

	[MenuItem("Custom Objects/TRIGGERS/GA/Play Animation")]
	private static void CreateAnimation()
	{
		Object o = Resources.Load("Prefabs/Triggers/TriggerPlayAnimation");
		InstantiateObject(o);
	}

	[MenuItem("Custom Objects/TRIGGERS/MSD/Sound events")]
	private static void CreateSoundEvent()
	{
		Object o = Resources.Load("Prefabs/Triggers/TriggerSoundEvents");
		InstantiateObject(o);
	}
     /// <summary>
     /// /
     /// </summary>
    [MenuItem("Custom Objects/TRIGGERS/Tutorial")]
    private static void CreateATrigger() {
        Object o = Resources.Load("Prefabs/Triggers/Tutorial/TriggerTutorial");
        InstantiateObject(o);
    }

    /// <summary>
    /// /
    /// </summary>
    [MenuItem("Custom Objects/TARGET OBJECTS/Move object")]
    private static void CreateMoveObject()
    {
        Object o = Resources.Load("Prefabs/Triggers/MoveObject/MoveObject");
        InstantiateObject(o);
    }

    [MenuItem("Custom Objects/TARGET OBJECTS/Mother teleport")]
    private static void CreateMotherTeleporter()
    {
        Object o = Resources.Load("Prefabs/Triggers/MoveObject/TeleportMum");
        InstantiateObject(o);
    }

	[MenuItem("Custom Objects/SOUND/Event emitter")]
	private static void CreateEventeEmitter()
	{
		Object o = Resources.Load("Prefabs/Sound emitter/FMOD_EventEmitter");
		InstantiateObject(o);
	}

	[MenuItem("Custom Objects/PATHS/Path")]
	private static void CreatePath()
	{
		Object o = Resources.Load("Prefabs/Paths/Path");
		InstantiateObject(o);
	}

    [MenuItem("Custom Objects/CUTSCENES/CutSceneMain")]
	private static void CreateCutSceneMain()
	{
        Object o = Resources.Load("Prefabs/CutScene/CutSceneMain");
		InstantiateObject(o);
	}

	[MenuItem("Custom Objects/CUTSCENES/CutSceneCamera")]
	private static void CreateCutCamera() {
		Object o = Resources.Load("Prefabs/Cameras/CutSceneCamera");
		InstantiateObject(o);
	}

    [MenuItem("Custom Objects/CUTSCENES/CutSceneSpline")]
    private static void CreateCutSceneSpline() {
        Object o = Resources.Load("Prefabs/CutScene/CutScene_Spline");
        InstantiateObject(o);
    }

    [MenuItem("Custom Objects/CUTSCENES/CutScene3DText")]
    private static void CreateCutScene3DText() {
        Object o = Resources.Load("Prefabs/CutScene/3DText");
        InstantiateObject(o);
    }
	

    [MenuItem("Custom Objects/MOTHER")]
    private static void CreateMother2D()
    {
		Object o = Resources.Load("Prefabs/Mother/Mother_prefab");
        InstantiateObject(o);
    }

    private static void InstantiateObject(Object o)
    {
        GameObject g = Instantiate(o, Vector3.zero, Quaternion.identity) as GameObject;

        if (SelectParentObject() != null)
            g.transform.parent = SelectParentObject().transform;

        FocusObject(g);
    }

    private static void FocusObject(GameObject g)
    {
        GameObject[] gArray = new GameObject[1];
        gArray[0] = g;
        Selection.objects = gArray;
    }

    private static GameObject SelectParentObject()
    {
        return Selection.activeObject as GameObject;
    }
}