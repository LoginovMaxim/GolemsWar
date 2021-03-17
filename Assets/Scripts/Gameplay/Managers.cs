using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Managers : MonoBehaviour
    {
        public static UnitsManager UnitsManager;
        public static MapManager MapManager;
        
        private Queue<IGameManager> _startSequence;

        public bool Ready;
        
        private void Start()
        {
            UnitsManager = GetComponent<UnitsManager>();
            MapManager = GetComponent<MapManager>();
            
            _startSequence = new Queue<IGameManager>();
            _startSequence.Enqueue(MapManager);
            _startSequence.Enqueue(UnitsManager);

            StartCoroutine(StartupManagers());
        }

        private IEnumerator StartupManagers() 
        {
            IGameManager currentGameManager;
            int numModules = _startSequence.Count;
            int numReady = 0;
            
            while (_startSequence.Count > 0)
            {
                currentGameManager = _startSequence.Dequeue();
                currentGameManager.Startup();
                
                while (currentGameManager.Status != ManagerStatus.Started)
                {
                    Debug.Log($"GameManager {currentGameManager} loading...");
                    yield return null;
                }

                numReady++;
                Debug.Log("Progress: " + numReady + "/" + numModules);
            }
            
            Debug.Log("All managers started up");

            Ready = true;
        }
    }
}