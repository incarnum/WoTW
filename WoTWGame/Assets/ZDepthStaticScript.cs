﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class ZDepthStaticScript : MonoBehaviour {

	[SerializeField] private float m_floorHeight;
	private float 		       	   m_spriteLowerBound;
	private float 		           m_spriteHalfWidth;
	//private readonly float         m_tan30 = Mathf.Tan(Mathf.PI / 5);

	void Start()
	{
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		m_spriteLowerBound  = spriteRenderer.bounds.size.y * 0.5f;
		m_spriteHalfWidth   = spriteRenderer.bounds.size.x * 0.5f;
		transform.position = new Vector3
			(
				transform.position.x, 
				transform.position.y, 
				(transform.position.y - m_spriteLowerBound + m_floorHeight)
			);
	}

	void Awake() {

	}

	// Use this condition for objects that don’t move in the scene. 
	//#if UNITY_EDITOR
	void LateUpdate()
	{
		// Use this condition for objects that don’t move in the scene. 
		//	if (!Application.isPlaying)
		//	{

		// Update the position in the Z axis:

		//	}
	}
	//#endif

	void OnDrawGizmos()
	{
		Vector3 floorHeightPos = new Vector3
			(
				transform.position.x,
				transform.position.y - m_spriteLowerBound + m_floorHeight, 
				transform.position.z
			);

		Gizmos.color = Color.magenta;
		Gizmos.DrawLine(floorHeightPos + Vector3.left * m_spriteHalfWidth, floorHeightPos + Vector3.right * m_spriteHalfWidth);
	}

	public void RecheckZDepth() {
		transform.position = new Vector3
			(
				transform.position.x, 
				transform.position.y, 
				(transform.position.y - m_spriteLowerBound + m_floorHeight)
			);
	}
}