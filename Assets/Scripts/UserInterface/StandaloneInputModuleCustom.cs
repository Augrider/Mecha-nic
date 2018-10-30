using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StandaloneInputModuleCustom : StandaloneInputModule {

	public PointerEventData GetMouseData(int id){
		return GetLastPointerEventData(id);
	}
}
