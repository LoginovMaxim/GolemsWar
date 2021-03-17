using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;

namespace Gameplay
{
    public class MapManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }
        public MapGenerator MapGenerator;
        
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.M))
            {
                UpdateHexes();
                MapGenerator.WriteMapData();
            }
            
            if(Input.GetKeyDown(KeyCode.C))
                CalculateCountNeighbors();
        }

        public void Startup()
        {
            //MapGenerator.SquareMapSize = MapGenerator.HexMapJsonData.Size;
            
            if (MapGenerator.MapMode == MapMode.Default)
            {
                MapGenerator.GenerateSquareMap();
            }
            else if (MapGenerator.MapMode == MapMode.Load)
            {
                MapGenerator.HexMapJsonData.LoadMap();
                MapGenerator.HexMapJsonData.LoadSpawnData();
                MapGenerator.GenerateBy_JSONData();
            }
            
            SetNeighborsСonnection();

            Status = ManagerStatus.Started;
        }

        private void UpdateHexes()
        {
            foreach (var hex in MapGenerator.Hexes)
            {
                if (hex.Playable == false)
                    continue;
                
                Transform[] childs = hex.GetComponentsInChildren<Transform>(true);
                
                hex.Playable = false;
                foreach (Transform child in childs)
                {
                    if (child.name == "HexView")
                    {
                        hex.Playable = true;
                        break;
                    }
                }
            }
        }

        private void SetNeighborsСonnection()
        {
            foreach (var hex in MapGenerator.Hexes)
                for (int i = 0; i < 6; i++)
                    hex.HaveNearHexAt(hex.transform.position + HexParameters.Offsets[i], true);
        }
        
        public void HideAllHighlighters()
        {
            foreach (var hex in MapGenerator.Hexes)
                hex.HideHighlighters();
        }

        public void ResetCheckAllHexes()
        {
            foreach (var hex in MapGenerator.Hexes)
                hex.Checked = false;
        }

        public void HideAllCoordinates()
        {
            foreach (var hex in MapGenerator.Hexes)
            {
                if (hex.Playable)
                    hex.CoordinateText.gameObject.SetActive(false);
            }
        }
        
        public void ResetHexPoints(int index)
        {
            foreach (var hex in MapGenerator.Hexes)
                hex.Points[index] = 0;
        }

        public void ActivateAllCapableHexes()
        {
            foreach (var hex in MapGenerator.Hexes)
                if (hex.Effects.Count > 0)
                    foreach (var capable in hex.Effects.ToList())
                        capable.DoEffect(hex);
        }

        public void FogMode(bool show)
        {
            foreach (var hex in MapGenerator.Hexes)
                hex.Fog.SetActive(show);
        }
        
        public void CalculateCountNeighbors()
        {
            int[] counts = new int[7];
            
            foreach (var hex in MapGenerator.Hexes)
            {
                if (hex.Playable)
                    counts[hex.NeighborHexes.Count]++;
            }

            for (int i = 0; i < counts.Length; i++)
            {
                Debug.Log(i + " соседей у " + counts[i] + " количества клеток");
            }
        }
    }
}