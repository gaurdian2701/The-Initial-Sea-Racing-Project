using System;
using System.Collections.Generic;
using ProceduralTracks;
using UnityEngine;

public class ProgressTracking : MonoBehaviour
{
    
    public static ProgressTracking Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private int numberOfLaps;
    
    
    private Tracks tracks;
    
    [HideInInspector]
    public List<RaceProgress>  racersProgress = new List<RaceProgress>();
    
    private List<Checkpoint> checkpoints = new List<Checkpoint>();
    
    void Start()
    {
        tracks = FindAnyObjectByType<Tracks>();

        int i = 0;
        foreach (var checkpoint in tracks.m_lRacingCheckPoints)
        {
            Checkpoint newCheckpoint = checkpoint.AddComponent<Checkpoint>();
            newCheckpoint.tracking = this;
            newCheckpoint.requiredCheckpoints = i;
            checkpoints.Add(newCheckpoint);
            
            i++;
        }

        checkpoints[^1].isFinishLine = true;

    }


    public void AddProgress(RacerData  toRacer)
    {
        foreach (var racer in racersProgress)
        {
            if (racer.racer.racerId == toRacer.racerId)
            {
                racer.checkpointsCompleted++;
                Debug.Log(toRacer.racerName + " scored a point and is now at: " + racer.checkpointsCompleted );
            }
        }
        
        
    }

    public int CheckProgress(RacerData ofRacer)
    {
        foreach (var racer in racersProgress)
        {
            if (ofRacer.racerId == racer.racer.racerId) return racer.checkpointsCompleted;
        }
        
        Debug.LogError("RacerData not found while checking progress");
        return 404;
    }

    public void AddLap(RacerData toRacer)
    {
        foreach (var racer in racersProgress)
        {
            if (racer.racer.racerId == toRacer.racerId)
            {
                racer.lapsCompleted++;
                Debug.Log(toRacer.racerName + " scored a lap and is now at: " + racer.lapsCompleted);
                if (racer.lapsCompleted >= numberOfLaps)
                {
                    EndRace(racer.racer);
                }

                racer.checkpointsCompleted = 0;
                
                UnbanIdAtEveryCheckpoint(racer.racer.racerId);
            }
        }
    }

    private void EndRace(RacerData winner)
    {
        Debug.Log("Race ended, winner is: " + winner.racerName);
    }

    private void UnbanIdAtEveryCheckpoint(int idToUnban)
    {
        foreach (var checkpoint in checkpoints)
        {
            checkpoint.UnbanID(idToUnban);
        }
    }
    
}




public class RaceProgress
{
    public RacerData racer;
    public int checkpointsCompleted;
    public int lapsCompleted;
}