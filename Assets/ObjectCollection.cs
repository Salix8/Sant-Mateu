using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollection : MonoBehaviour
{
	[SerializeField] private GameObject[] objects;
	public GameObject[] Objects => objects;

}
