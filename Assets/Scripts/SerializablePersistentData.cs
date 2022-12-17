using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializablePersistentData
{
    public string spawnMap;
    public float[] spawnLoc;
    public float[] spawnRot;

    public SerializablePersistentData(PersistentData data)
    {
        spawnMap = data.GetCurrentSceneName();

        spawnLoc = new float[3];
        spawnLoc[0] = data.spawnLoc.x;
        spawnLoc[1] = data.spawnLoc.y;
        spawnLoc[2] = data.spawnLoc.z;

        spawnRot = new float[4];
        spawnRot[0] = data.spawnRot.x;
        spawnRot[1] = data.spawnRot.y;
        spawnRot[2] = data.spawnRot.z;
        spawnRot[3] = data.spawnRot.w;
    }
}
