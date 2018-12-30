using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache : MonoBehaviour, IGameManager {

	public ManagerStatus status { get; private set; }

	public List<Cached> _cache;
	public int quantity = 0;
	private int used=0;

	[SerializeField] private GameObject _point;
	[SerializeField] private GameObject _Liner;

	public void Startup() {
		Debug.Log ("Cache Manager starting... ");
		_cache = new List<Cached> ();
		status = ManagerStatus.Started;
	}

	public void AddToCache(int playerId, LineRenderer liner, MeshRenderer point) {
		Cached addition = new Cached ();
		addition.liner = liner;
		addition.point = point;
		_cache.Add (addition);
		quantity++;
	}

	public void Enable(int id, Vector3 pointPos) {
		_cache [id].liner.gameObject.SetActive (true);
		_cache [id].point.transform.position = pointPos;
		_cache [id].point.enabled=true;
	}

	public void DisableAll() {
		foreach(Cached pair in _cache){
			pair.liner.gameObject.SetActive (false);
			pair.point.enabled=false;
			pair.liner.positionCount=0;
			used = 0;
		}
	}

	public void CacheCheck(int playerId, int id, Vector3 pointPos){
		if (used == quantity) {
			GameObject point = Instantiate (_point, pointPos, Quaternion.identity);
			GameObject liner = Instantiate (_Liner);
			point.transform.parent = this.gameObject.transform;
			liner.transform.parent = this.gameObject.transform;
			LineRenderer line = liner.GetComponent<LineRenderer> ();
			MeshRenderer ball = point.GetComponent<MeshRenderer> ();
			AddToCache (playerId, line, ball);
		} else {
			Enable (used, pointPos);
		}
		_cache[used]._linerPointer.myId = id;
		_cache[used]._linerPointer.playerId = playerId;
		_cache[used]._pointPointer.myId = id;
		_cache[used]._pointPointer.playerId = playerId;
		used++;
	}
}
