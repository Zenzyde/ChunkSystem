using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
	[SerializeField] private float chunkSpawningInterval;
	[SerializeField] private float visibilityRadius;
	[SerializeField] private float chunkRadius;

	public delegate void OnUpdateChunkEvent(Vector3 position);
	public event OnUpdateChunkEvent OnUpdateChunk;

	public HashSet<Vector3Int> CHUNKPOSITIONS { get { return chunkPositions; } }
	public bool UpdateChunkList(Vector3Int value) => chunkPositions.Add(value);
	protected HashSet<Vector3Int> chunkPositions = new HashSet<Vector3Int>();

	private Vector3Int lastCappedPosition;

	// Start is called before the first frame update
	void Start()
	{
		SpawnChunk();
	}

	// Update is called once per frame
	void Update()
	{
		UpdateCappedPosition();
	}

	void SpawnChunk()
	{
		GameObject chunkObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Chunk chunk = chunkObj.AddComponent<Chunk>();
		chunkObj.transform.position = transform.position;

		chunk.visibilityRadius = visibilityRadius;
		chunk.chunkSpawningInterval = chunkSpawningInterval;
		chunk.chunkRadius = chunkRadius;
		chunk.InitialForcedSpawn(transform.position);
	}

	Vector3Int GetCappedPosition()
	{
		return transform.position.ToVector3Int();
	}

	void UpdateCappedPosition()
	{
		if (lastCappedPosition != GetCappedPosition())
		{
			lastCappedPosition = GetCappedPosition();
			OnUpdateChunk?.Invoke(lastCappedPosition);
		}
	}
}
