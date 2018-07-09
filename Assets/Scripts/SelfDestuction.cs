using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestuction : MonoBehaviour {

    [Tooltip("In seconds")][SerializeField] float destroyTimer = 3f;

	void Start ()
    {
        Destroy(gameObject, destroyTimer);
	}
}
