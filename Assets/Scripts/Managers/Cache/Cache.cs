using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache : MonoBehaviour, IGameManager {

	public ManagerStatus status { get; private set; }

	public List<Cached> _cache;
	public int quantity;

	public void Startup() {
		Debug.Log ("Cache Manager starting... ");
		_cache = new List<Cached> ();
		quantity = 0;
		status = ManagerStatus.Started;
	}

	public void AddToCache(LineRenderer liner, GameObject point) {
		Cached addition = new Cached ();
		addition.liner = liner;
		addition.point = point;
		_cache.Add (addition);
		quantity++;
	}

	public void Enable(int id, Vector3 pointPos) {
		_cache [id].liner.gameObject.SetActive (true);
		_cache [id].point.SetActive (true);
		_cache [id].point.transform.position = pointPos;
	}
}
