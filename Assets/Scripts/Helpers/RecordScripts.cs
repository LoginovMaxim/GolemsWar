using UnityEngine;

namespace Helpers
{
    public class RecordScripts : MonoBehaviour
    {
        /*
     
    class MapGenerator
     
    private void Start()
    {
        StartCoroutine(GenerateAllMaps());
    }
    
    private IEnumerator GenerateAllMaps()
    {
        List<Hex> newHexes = new List<Hex>();
        
        _hexes.Add(Instantiate(HexPrefab, Container));
        
        for (int i = 0; i < MapSize; i++)
        {
            foreach (var hex in _hexes)
            {
                if (hex.Verified)
                    continue;
                
                for (int j = 0; j < 6; j++)
                {
                    if (hex.HaveNearHexAt(hex.transform.position + HexParameters.Offsets[j], false) == false)
                    {
                        Hex newHex = Instantiate(HexPrefab, Container);
                        newHex.transform.position = hex.transform.position + HexParameters.Offsets[j];
                        newHexes.Add(newHex);
                        yield return new WaitForSeconds(0.15f);
                    }
                    
                }

                hex.Verified = true;
            }

            foreach (var newHex in newHexes)
                _hexes.Add(newHex);
            
            newHexes.Clear();
        }

        if (MapSize == 3)
        {
            HexMapJsonData.MapData.Maps.Add(new HexPosition());

            HexPosition hexPosition = HexMapJsonData.MapData.Maps[0];
            hexPosition.Positions = new List<Vector3>();
            HexMapJsonData.MapData.Maps[0] = hexPosition;
            
            foreach (var hex in _hexes)
                HexMapJsonData.MapData.Maps[0].Positions.Add(hex.transform.position);
        }
        else if (MapSize == 4)
        {
            HexMapJsonData.MapData.Maps.Add(new HexPosition());
            
            HexPosition hexPosition = HexMapJsonData.MapData.Maps[1];
            hexPosition.Positions = new List<Vector3>();
            HexMapJsonData.MapData.Maps[1] = hexPosition;
            
            foreach (var hex in _hexes)
                HexMapJsonData.MapData.Maps[1].Positions.Add(hex.transform.position);
        }
        else if (MapSize == 5)
        {
            HexMapJsonData.MapData.Maps.Add(new HexPosition());
            
            HexPosition hexPosition = HexMapJsonData.MapData.Maps[2];
            hexPosition.Positions = new List<Vector3>();
            HexMapJsonData.MapData.Maps[2] = hexPosition;
            
            foreach (var hex in _hexes)
                HexMapJsonData.MapData.Maps[2].Positions.Add(hex.transform.position);
        }
        
        MapSize++;
            
        if (MapSize == 6)
        {
            SaveAllPositions();
            yield break;
        }
        
        _hexes.Clear();

        foreach (Transform child in Container)
        {
            Destroy(child.gameObject);
        }
        
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(GenerateAllMaps());
    }

    private void SaveAllPositions()
    {
        HexMapJsonData.SaveDataMap();
    }
     */
    }
}
