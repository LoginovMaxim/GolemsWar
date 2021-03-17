using Environment;
using Environment.Hex;
using Gameplay;
using UnityEngine;

namespace Units.Possibilities.View
{
    public class Viewer : MonoBehaviour, IViewable
    {
        [SerializeField] private int _viewRadius;

        private MapManager _mapManager;
        
        public int ViewRadius
        {
            get => _viewRadius;
            set => _viewRadius = value;
        }

        private void Start()
        {
            _mapManager = FindObjectOfType<MapManager>();
        }

        public void DispelWarFog(Hex hex)
        {
            hex.Fog.SetActive(false);

            if (ViewRadius < 0)
            {
                _mapManager.FogMode(false);
                return;
            }
            
            if (ViewRadius > 0)
            {
                foreach (var neighborHex in hex.NeighborHexes)
                {
                    if (neighborHex.Fog.activeSelf)
                        neighborHex.Fog.SetActive(false);
                }
            }
            
            if (ViewRadius > 1)
            {
                foreach (var neighborHex in hex.NeighborHexes)
                {
                    foreach (var neighborHex2 in neighborHex.NeighborHexes)
                    {
                        if (neighborHex2.Fog.activeSelf)
                            neighborHex2.Fog.SetActive(false);
                    }
                }
            }
            
            if (ViewRadius > 2)
            {
                foreach (var neighborHex in hex.NeighborHexes)
                {
                    foreach (var neighborHex2 in neighborHex.NeighborHexes)
                    {
                        foreach (var neighborHex3 in neighborHex2.NeighborHexes)
                        {
                            if (neighborHex3.Fog.activeSelf)
                                neighborHex3.Fog.SetActive(false);
                        }
                    }
                }
            }
            
            if (ViewRadius > 3)
            {
                foreach (var neighborHex in hex.NeighborHexes)
                {
                    foreach (var neighborHex2 in neighborHex.NeighborHexes)
                    {
                        foreach (var neighborHex3 in neighborHex2.NeighborHexes)
                        {
                            foreach (var neighborHex4 in neighborHex3.NeighborHexes)
                            {
                                if (neighborHex4.Fog.activeSelf)
                                    neighborHex4.Fog.SetActive(false);
                            }
                        }
                    }
                }
            }
            
            if (ViewRadius > 4)
            {
                foreach (var neighborHex in hex.NeighborHexes)
                {
                    foreach (var neighborHex2 in neighborHex.NeighborHexes)
                    {
                        foreach (var neighborHex3 in neighborHex2.NeighborHexes)
                        {
                            foreach (var neighborHex4 in neighborHex3.NeighborHexes)
                            {
                                foreach (var neighborHex5 in neighborHex4.NeighborHexes)
                                {
                                    if (neighborHex5.Fog.activeSelf)
                                        neighborHex5.Fog.SetActive(false);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}