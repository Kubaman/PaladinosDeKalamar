using UnityEngine;
using System.Collections;
using Characters;

public class ObjectManager
{
	public static GameObject InstantiateObject(string path, Vector3 position)
	{
		GameObject prefab = (GameObject)Resources.Load("Prefabs/" + path);
		GameObject returnObject = (GameObject)GameObject.Instantiate(prefab, position, Quaternion.identity);

		return returnObject;
	}

	public static Characters.CharacterController CharacterController(string path, Vector3 position)
	{
		GameObject prefab = (GameObject)Resources.Load("Prefabs/" + path);
		GameObject returnObject = (GameObject)GameObject.Instantiate(prefab, position, Quaternion.identity);
		
		return CharacterManager.GetController(returnObject);
	}
}
