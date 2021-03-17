using System;
using System.Collections.Generic;
using System.Linq;
using Environment.Hex;
using Helpers;
using Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class MapGenerator : MonoBehaviour
    {
        [Header("Players")]
        public Unit[] Plyers; 
        
        [Header("Main")]
        public MapMode MapMode;
        public HexMap_JSONData HexMapJsonData;
        
        [Header("Create params")]
        public Transform Container;
        public Environment.Hex.Hex HexPrefab;
        public int SquareMapSize;
        
        [Header("Adjust")]
        //[Range(3, 5)] public int MapSize;
        public bool ShowCoordinateText;
        public List<Environment.Hex.Hex> Hexes = new List<Environment.Hex.Hex>();

        [HideInInspector]
        public List<Environment.Hex.Hex> CorrectHexes = new List<Environment.Hex.Hex>();
        
        public void GenerateSquareMap()
        {
            int commonElement = Random.Range(1, 6);
            commonElement *= 100;
            
            Vector3 position = Vector3.zero;
            Vector3 startRowPosition = Vector3.zero;
            
            for (int i = 0; i < SquareMapSize; i++)
            {
                for (int j = 0; j < SquareMapSize; j++)
                {
                    if (j == 0)
                        startRowPosition = position;
                    
                    if (j == SquareMapSize - 1)
                        if (i % 2 != 0)
                            continue;
                    
                    Environment.Hex.Hex hex = Instantiate(HexPrefab, position, Quaternion.identity, Container);
                    hex.Code = commonElement + Random.Range(0, 6);
                    hex.Coordinate = new Vector2Int(i, j);

                    hex.Fraction = 0;
                    
                    if (ShowCoordinateText)
                        hex.CoordinateText.text = $"({i}; {j})";
                    else
                        hex.CoordinateText.gameObject.SetActive(false);

                    hex.Playable = true;
                    
                    Hexes.Add(hex);
                    
                    position += HexParameters.Offsets[0];
                }

                if (i % 2 == 0)
                {
                    position = startRowPosition + HexParameters.Offsets[2];
                }
                else
                {
                    position = startRowPosition + HexParameters.Offsets[3];
                }
            }
        }

        public void WriteMapData()
        {
            Debug.Log("Map is written");
            
            HexMapJsonData.Map.Playables.Clear();
            HexMapJsonData.Map.Coordinates.Clear();
            HexMapJsonData.Map.HexCodes.Clear();
            HexMapJsonData.Map.Fractions.Clear();
            HexMapJsonData.Map.Positions.Clear();
            HexMapJsonData.Map.Rotations.Clear();
            
            foreach (var hex in Hexes)
            {
                HexMapJsonData.Map.Playables.Add(hex.Playable);
                HexMapJsonData.Map.Coordinates.Add(hex.Coordinate);
                //HexMapJsonData.Map.HexCodes.Add(hex.Code);
                HexMapJsonData.Map.Fractions.Add(hex.Fraction);
                HexMapJsonData.Map.Positions.Add(hex.transform.position);
                HexMapJsonData.Map.Rotations.Add(hex.transform.rotation);
            }
        }
        
        public void GenerateBy_JSONData()
        {
            int commonElement = Random.Range(1, 6);
            commonElement *= 100;
            
            for (int i = 0; i < HexMapJsonData.Map.Coordinates.Count; i++)
            {
                Environment.Hex.Hex hex = Instantiate(HexPrefab, HexMapJsonData.Map.Positions[i], HexMapJsonData.Map.Rotations[i], Container);
                hex.Code = commonElement + Random.Range(0, 6);
                hex.Coordinate = HexMapJsonData.Map.Coordinates[i];
                hex.name = $"Hex[{hex.Coordinate.x}, {hex.Coordinate.y}]";
                
                switch (HexMapJsonData.Map.Fractions[i])
                {
                    case 1:
                        hex.Fraction = 2;
                        break;
                    case -1:
                        hex.Fraction = 1;
                        break;
                    case 0:
                        hex.Fraction = 0;
                        break;
                }
                
                if (ShowCoordinateText)
                {
                    hex.CoordinateText.text = $"({HexMapJsonData.Map.Coordinates[i].x}; {HexMapJsonData.Map.Coordinates[i].y})";
                    if (hex.Fraction % 2 == 0)
                        hex.CoordinateText.color = Color.black;
                    if (hex.Fraction % 2 != 0)
                        hex.CoordinateText.color = Color.red;
                }
                else
                    hex.CoordinateText.gameObject.SetActive(false);

                hex.Playable = HexMapJsonData.Map.Playables[i];
                
                Hexes.Add(hex);
            }

            CreatePortalUnits();
        }
        
        public void CreatePortalUnits()
        {
            int countSpawnPoints = HexMapJsonData.SpawnData.SpawnCoordinates.Count;
            float hexDiameter = HexParameters.InnerRadius * 2f;
            
            Vector2Int playerPos1 = HexMapJsonData.SpawnData.SpawnCoordinates[Random.Range(0, countSpawnPoints)];
            Vector2Int playerPos2;

            do
            {
                playerPos2 = HexMapJsonData.SpawnData.SpawnCoordinates[Random.Range(0, countSpawnPoints)];
                
                if (Vector2Int.Distance(playerPos1, playerPos2) > HexMapJsonData.SpawnData.SpawnDistance)
                    break;
                
            } 
            while (true);

            CreatePlayerUnit(playerPos1, 0);
            CreatePlayerUnit(playerPos2, 1);
        }

        public void CreatePlayerUnit(Vector2Int position, int index)
        {
            Hex playerHex = Hexes.First(h => h.Coordinate == position);
            playerHex.Units.Add(Plyers[index]);
            Plyers[index].transform.position = playerHex.transform.position + Plyers[index].DistanceAboveGround * Vector3.up;
        }

        public void CreateBioms(Unit player1, Unit player2)
        {
            
        }
    }
    
    public enum MapMode
    {
        Default,
        Load
    }
}
