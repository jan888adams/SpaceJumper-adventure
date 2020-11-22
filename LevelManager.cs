using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class LevelManager : MonoBehaviour
{

    // The object you want to at this Script must be in position X=0, Y=0, Z=0!
    public static LevelManager SharedInstance;
    public List<LevelBlock> AllTheLevelBlocks = new List<LevelBlock>();
    public List<LevelBlock> CurrentLevelBlocks = new List<LevelBlock>();
    public Transform LevelStartPosition;


    void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
    }

    public void AddLevelBlock()
    {
        int randomIdx = Random.Range(0, AllTheLevelBlocks.Count);
        // same chance

        LevelBlock block;

        Vector3 SpanwnPosition = Vector3.zero;

        if (CurrentLevelBlocks.Count == 0)
        {
            block = Instantiate(AllTheLevelBlocks[0]);
            //start zone
            SpanwnPosition = LevelStartPosition.position;

        }
        else
        {
            block = Instantiate(AllTheLevelBlocks[randomIdx]);
            // instantiate level blocks randomly

            SpanwnPosition = CurrentLevelBlocks[CurrentLevelBlocks.Count - 1].endPoint.position;
            // reverse start and endpoint
        }

        block.transform.SetParent(this.transform,false); 
        // Set has child of LevelManager
        
        Vector3 correction = new Vector3(
            // Correct the position from the level blocks
            // -> for secure! 
            SpanwnPosition.x-block.startPoint.position.x,
            SpanwnPosition.y-block.startPoint.position.y,
            0  // its 2D -> no position needed
            );
        block.transform.position = correction;
        CurrentLevelBlocks.Add(block);

    }

    public void RemoveLevelBlock()
    {
        LevelBlock oldBlock = CurrentLevelBlocks[0];
        CurrentLevelBlocks.Remove(oldBlock);
        Destroy(oldBlock.gameObject);
    }

    public void RemoveAll()
    {
        while (CurrentLevelBlocks.Count > 0)
        {
            RemoveLevelBlock();
        }
    }

    public void GenerateInitialLevelBlocks()
    {
        for (int i = 0; i < 2; i++)
        {
            AddLevelBlock();
        }
    }
}
