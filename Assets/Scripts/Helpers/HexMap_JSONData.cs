using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Units;
using UnityEngine;
using UnityEngine.UIElements;
using System.Text.RegularExpressions;

namespace Helpers
{
    public class HexMap_JSONData : MonoBehaviour
    {
        public int Size;
        public int Version;

        public string PathToMapsFolder;
        public string PathToMapFile;
        public string PathToSpawnDatasFolder;
        public string PathToSpawnDatasFile;
        
        public Map Map;
        public SpawnData SpawnData;

        private void Awake()
        {
            PathToMapFile = PathToMapsFolder + $"Map{Size}v{Version}.json";
            PathToSpawnDatasFile = PathToSpawnDatasFolder + $"SpawnData{Size}v{Version}.json";
        }

        public void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift))
                if (Input.GetKeyDown(KeyCode.S))
                {
                    SaveMap();
                    SaveSpawnData();
                }
        }

        public void LoadMap()
        {
            if (File.Exists(Application.dataPath + "/../" + PathToMapFile))
            {
                string json = File.ReadAllText(Application.dataPath + "/../" + PathToMapFile);
                Map = JsonUtility.FromJson<Map>(json);
            }
            else
            {
                Debug.Log($"File {Application.dataPath + "/../" + PathToMapFile} is not found...");
            }
        }
        
        public void LoadSpawnData()
        {
            if (File.Exists(Application.dataPath + "/../" + PathToSpawnDatasFile))
            {
                string json = File.ReadAllText(Application.dataPath + "/../" + PathToSpawnDatasFile);
                SpawnData = JsonUtility.FromJson<SpawnData>(json);
            }
            else
            {
                Debug.Log($"File {Application.dataPath + "/../" + PathToSpawnDatasFile} is not found...");
            }
        }

        public void SaveMap()
        {            
            PathToMapFile = PathToMapsFolder + $"Map{Size}v{Version}.json";
            
            if (File.Exists(Application.dataPath + "/../" + PathToMapFile))
                File.Delete(Application.dataPath + "/../" + PathToMapFile);
            
            File.WriteAllText(Application.dataPath + "/../" + PathToMapFile, JsonUtility.ToJson(Map));
            
            Debug.Log("Map is saved");
        }
        
        public void SaveSpawnData()
        {            
            PathToSpawnDatasFile = PathToSpawnDatasFolder + $"SpawnData{Size}v{Version}.json";
            
            if (File.Exists(Application.dataPath + "/../" + PathToSpawnDatasFile))
                File.Delete(Application.dataPath + "/../" + PathToSpawnDatasFile);
            
            File.WriteAllText(Application.dataPath + "/../" + PathToSpawnDatasFile, JsonUtility.ToJson(SpawnData));
            
            Debug.Log("SpawnData is saved");
        }
    }

    [Serializable]
    public struct Map
    {
        public List<Vector2Int> Coordinates;
        public List<bool> Playables;
        public List<string> HexCodes;
        public List<int> Fractions;
        public List<Vector3> Positions;
        public List<Quaternion> Rotations;
    }

    [Serializable]
    public struct SpawnData
    {
        public float SpawnDistance;
        public List<Vector2Int> SpawnCoordinates;
        public List<Vector2Int> NPC_Coordinates;
    }
}