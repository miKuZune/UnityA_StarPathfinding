using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour {



    public LayerMask hitLayers;
	// Update is called once per frame
	void Update ()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if(Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
        {
            this.transform.position = hit.point;
        }
	}
}
