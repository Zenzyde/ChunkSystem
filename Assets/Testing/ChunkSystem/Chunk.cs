using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Chunk : MonoBehaviour
{
	public float chunkSpawningInterval;
	public float visibilityRadius;
	public float chunkRadius;
	public Renderer renderer;

	private ChunkManager manager;

	void OnEnable()
	{
		manager.OnUpdateChunk += UpdateChunk;
	}

	void OnDisable()
	{
		manager.OnUpdateChunk -= UpdateChunk;
	}

	void OnDestroy()
	{
		manager.OnUpdateChunk -= UpdateChunk;
	}

	void Awake()
	{
		manager = FindObjectOfType<ChunkManager>();
		manager.OnUpdateChunk += UpdateChunk;
		if (!renderer)
			renderer = GetComponent<Renderer>();
	}

	public void InitialForcedSpawn(Vector3 position)
	{
		SpawnChunksForced(position);
	}

	public void UpdateChunk(Vector3 position)
	{
		SpawnChunks(position);
		DeleteChunk(position);
	}

	void SpawnChunks(Vector3 pos)
	{
		float dist = Vector3.SqrMagnitude(transform.position - pos);

		if (dist > visibilityRadius * visibilityRadius)
			return;

		for (float i = 0; i < 360; i += (360 / chunkSpawningInterval))
		{
			float x = Mathf.Sin(i * Mathf.Deg2Rad);
			float z = Mathf.Cos(i * Mathf.Deg2Rad);
			Vector3 direction = new Vector3(x, 0, z);
			Vector3Int position = (transform.position + direction * chunkRadius * 2).ToVector3Int();

			Vector3 objectToCamDirection = (transform.position - pos).normalized;
			bool inFrontOfCamera = Vector3.Dot(objectToCamDirection, Camera.main.transform.forward) >= 0f;

			if (Physics.OverlapSphere(position, chunkRadius * 0.9f).Length > 0 || !inFrontOfCamera)
			{
				continue;
			}

			GameObject chunkObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			Chunk chunk = chunkObj.AddComponent<Chunk>();
			chunkObj.transform.position = position;

			chunk.visibilityRadius = visibilityRadius;
			chunk.chunkSpawningInterval = chunkSpawningInterval;
			chunk.chunkRadius = chunkRadius;

			if (!manager.CHUNKPOSITIONS.Contains(position))
				manager.UpdateChunkList(position);
		}
	}

	void SpawnChunksForced(Vector3 pos)
	{
		float dist = Vector3.SqrMagnitude(transform.position - pos);

		if (dist > visibilityRadius * visibilityRadius)
			return;

		for (float i = 0; i < 360; i += (360 / chunkSpawningInterval))
		{
			float x = Mathf.Sin(i * Mathf.Deg2Rad);
			float z = Mathf.Cos(i * Mathf.Deg2Rad);
			Vector3 direction = new Vector3(x, 0, z);
			Vector3Int position = (transform.position + direction * chunkRadius * 2).ToVector3Int();

			Vector3 objectToCamDirection = (transform.position - pos).normalized;
			bool inFrontOfCamera = Vector3.Dot(objectToCamDirection, Camera.main.transform.forward) >= 0f;

			if (Physics.OverlapSphere(position, chunkRadius * 0.9f).Length > 0 || !inFrontOfCamera)
			{
				continue;
			}

			GameObject chunkObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			Chunk chunk = chunkObj.AddComponent<Chunk>();
			chunkObj.transform.position = position;

			chunk.visibilityRadius = visibilityRadius;
			chunk.chunkSpawningInterval = chunkSpawningInterval;
			chunk.chunkRadius = chunkRadius;
			chunk.InitialForcedSpawn(pos);

			if (!manager.CHUNKPOSITIONS.Contains(position))
				manager.UpdateChunkList(position);
		}
	}

	void DeleteChunk(Vector3 position)
	{
		float dist = Vector3.SqrMagnitude(transform.position - position);

		Vector3 objectToCamDirection = (transform.position - position).normalized;
		bool inFrontOfCamera = Vector3.Dot(objectToCamDirection, Camera.main.transform.forward) >= 0f;

		if (dist > visibilityRadius * visibilityRadius || !inFrontOfCamera)
			Destroy(gameObject);
	}
}
