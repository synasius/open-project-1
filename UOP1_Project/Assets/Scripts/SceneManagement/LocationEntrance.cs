﻿using Unity.Cinemachine;
using System.Collections;
using UnityEngine;

public class LocationEntrance : MonoBehaviour
{
	[SerializeField] private PathSO _entrancePath;
	[SerializeField] private PathStorageSO _pathStorage = default; //This is where the last path taken has been stored
	[SerializeField] private CinemachineCamera entranceShot;

	[Header("Lisenting on")]
	[SerializeField] private VoidEventChannelSO _onSceneReady;
	public PathSO EntrancePath => _entrancePath;

	private void Awake()
	{
		if(_pathStorage.lastPathTaken == _entrancePath)
		{
			entranceShot.Priority.Value = 100;
			_onSceneReady.OnEventRaised += PlanTransition;
		}
	}

	private void PlanTransition()
	{
		StartCoroutine(TransitionToGameCamera());
	}

	private IEnumerator TransitionToGameCamera()
	{

		yield return new WaitForSeconds(.1f);

		entranceShot.Priority.Value = 100;
		_onSceneReady.OnEventRaised -= PlanTransition;
	}
}
