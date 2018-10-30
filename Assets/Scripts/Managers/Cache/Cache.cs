using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache : MonoBehaviour, IGameManager {

	public ManagerStatus status { get; private set; }

	public List<Cached> _cache;
	public int quantity;

	[SerializeField] private GameObject _point;
	[SerializeField] private GameObject _Liner;

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

	public void CacheCheck(int id, Vector3 pointPos){
		if (id == Managers.cache.quantity) {
			GameObject point = Instantiate (_point, pointPos, Quaternion.identity);
			GameObject liner = Instantiate (_Liner, new Vector3 (0, 3, 0), Quaternion.identity);
			point.transform.parent = this.gameObject.transform;
			liner.transform.parent = this.gameObject.transform;
			LineRenderer line = liner.GetComponent<LineRenderer> ();
			AddToCache (line, point);
		} else {
			Enable (id, pointPos);
		}
	}
}
