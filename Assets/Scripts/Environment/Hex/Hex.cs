using System;
using System.Collections.Generic;
using Helpers;
using InputController;
using TMPro;
using Units;
using Units.UnitStats;
using UnityEngine;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

namespace Environment.Hex
{
    public class Hex : MonoBehaviour, ISelectable
    {
        [Header("UI")]
        public TextMeshProUGUI CoordinateText;

        [Header("Gameplay")]
        public int Code;
        public int Fraction;
        public Element Element;
        public List<int> Points;
        public bool Visible = true;
        public List<IEffectable> Effects;
        
        [Header("Units")]
        public List<Unit> Units;
        public List<Hex> NeighborHexes;

        [Header("Highlighters")]
        public Transform HighlighterContainer;
        public List<GameObject> HighlighterPrefabs;
        
        [Header("VFX")]
        public Transform VFXContainer;
        public List<GameObject> VisualEffectPrefabs;
        public GameObject FogPrefab;

        [HideInInspector]
        public List<GameObject> Highlighters;
        [HideInInspector] 
        public Dictionary<VisualEffectType, GameObject> VisualEffects;
        [HideInInspector] 
        public GameObject Fog;
        [HideInInspector]
        public Vector2Int Coordinate;
        [HideInInspector]
        public bool Checked;

        private bool _isSelect;

        public bool IsSelect
        {
            get => _isSelect;
            set => _isSelect = value;
        }
        
        [SerializeField] private bool _playable;

        public bool Playable
        {
            get => _playable;
            set
            {
                foreach (Transform child in gameObject.transform)
                    child.gameObject.SetActive(value);

                _playable = value;
            }
        }
        

        private void Start()
        {
            Points = new List<int>();
            Effects = new List<IEffectable>();
            VisualEffects = new Dictionary<VisualEffectType, GameObject>();

            for (int i = 0; i < 6; i++)
                Points.Add(0);
            
            for (int i = 0; i < HighlighterPrefabs.Count; i++)
                Highlighters.Add(Instantiate(HighlighterPrefabs[i], HighlighterContainer));

            Fog = Instantiate(FogPrefab, VFXContainer);
            Fog.transform.position += Vector3.up * 0.5f;
            
            HideHighlighters();
        }

        public void HideHighlighters()
        {
            foreach (var highlighter in Highlighters)
                highlighter.gameObject.SetActive(false);
        }
        
        public bool HaveNearHexAt(Vector3 position, bool neighborMode)
        {
            float heightOffset = 5f;

            Ray ray = new Ray(position + Vector3.up * heightOffset,  Vector3.down * heightOffset * 2f);
        
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out Hex hex))
                {
                    if (neighborMode)
                        NeighborHexes.Add(hex);
                
                    return true;
                }
            }
        
            return false;
        }

        public bool HaveAnyUnit() => Units.Count > 0;
        
        public bool HaveEnemyUnit(Unit selfUnit)
        {
            if (HaveAnyUnit())
            {
                foreach (var unit in Units)
                {
                    if (selfUnit.Fraction == unit.Fraction)
                        return false;
                }
            }
            else
            {
                return false;
            }
            
            return true;
        }

        public void FindAttackUnits(Unit unit)
        {
            List<Hex> hexes = new List<Hex>();
            List<Hex> newHexes = new List<Hex>();
            
            hexes.Add(this);

            int countAttackPoints = unit.Attackable.AttackDistance;
            while (countAttackPoints > 0)
            {
                foreach (var hex in hexes)
                {
                    foreach (var neighbor in hex.NeighborHexes)
                    {
                        if (neighbor == this)
                            continue;
                        
                        newHexes.Add(neighbor);
                        
                        if (neighbor.Points[1] < 0)
                            continue;
                        
                        if (neighbor.HaveEnemyUnit(unit) && (unit.Attackable.AttackDistance - countAttackPoints + 1) > unit.Attackable.BlockedDistance)
                        {
                            neighbor.Points[1] = 1;
                            neighbor.Highlighters[1].gameObject.SetActive(true);
                        }
                        else
                        {
                            neighbor.Points[1] = -1;
                        }
                    }
                }

                hexes.Clear();
                
                foreach (var newHex in newHexes)
                    hexes.Add(newHex);
                
                newHexes.Clear();

                countAttackPoints--;
            }
        }

        public void ChangeBiom(int code)
        {
            Code = code;

            switch (Mathf.FloorToInt(Code / 100f))
            {
                case 1: Element = Element.Fire; break;
                case 2: Element = Element.Earth; break;
                case 3: Element = Element.Metal; break;
                case 4: Element = Element.Ice; break;
                case 5: Element = Element.Tree; break;
            }
        }
        
        public void CreateBiomAt(int element)
        {
            int iteration = 0;
            
            List<Hex> hexes = new List<Hex>();
            List<Hex> newHexes = new List<Hex>();
            
            hexes.Add(this);

            int countCircles = 0;
            switch (FindObjectOfType<HexMap_JSONData>().Size)
            {
                case 9: countCircles = 3; break;
                case 11: countCircles = 4; break;
                case 13: countCircles = 5; break;
            }

            if (element < 0)
                element = Random.Range(1, 6);

            element *= 100;
            
            while (countCircles > 0)
            {
                iteration++;
                
                foreach (var hex in hexes)
                {
                    foreach (var neighbor in hex.NeighborHexes)
                    {
                        if (neighbor.Checked)
                            continue;
                
                        if (neighbor == this)
                            continue;

                        int codeVariant = GetCodeVariant(neighbor.Code);
                        neighbor.Code = element + codeVariant;
                        
                        newHexes.Add(neighbor);
                    }
                }

                hexes.Clear();
                
                foreach (var newHex in newHexes)
                    hexes.Add(newHex);
                
                newHexes.Clear();

                countCircles--;
            }
            
            
        }

        public int GetCodeVariant(int code)
        {
            while (code > 0)
            {
                code -= 100;
            }

            return Mathf.Abs(code);
        }
        
        public void MovementPathFinding(Unit unit)
        {
            int iteration = 0;
            
            List<Hex> hexes = new List<Hex>();
            List<Hex> newHexes = new List<Hex>();
            
            hexes.Add(this);

            int countMovePoints = unit.Movement.CountMovePoints;
            while (countMovePoints > 0)
            {
                iteration++;
                
                foreach (var hex in hexes)
                {
                    foreach (var neighbor in hex.NeighborHexes)
                    {
                        if (neighbor.Points[0] != 0)
                            continue;
                
                        if (neighbor == this)
                            continue;
                        
                        if (neighbor.CanReceiveUnit(unit))
                        {
                            neighbor.Points[0] = iteration;
                            neighbor.Highlighters[0].gameObject.SetActive(true);
                            newHexes.Add(neighbor);
                        }
                        else
                        {
                            neighbor.Points[0] = -1;
                        }
                    }
                }

                hexes.Clear();
                
                foreach (var newHex in newHexes)
                    hexes.Add(newHex);
                
                newHexes.Clear();

                countMovePoints--;
            }
        }

        public Queue<Hex> PaveMovementPath(Unit unit, Hex targetHex)
        {
            Queue<Hex> queueHexes = new Queue<Hex>();
            int iteration = targetHex.Points[0];

            Hex hex;
            Hex newHex = null;

            Stack<Hex> stackHexes = new Stack<Hex>();
            
            hex = targetHex;
            stackHexes.Push(hex);
            iteration--;
            
            while (iteration > 0)
            {
                foreach (var neighbor in hex.NeighborHexes)
                {
                    if (neighbor.Points[0] == iteration)
                    {
                        newHex = neighbor;
                        stackHexes.Push(newHex);
                        break;
                    }
                }

                hex = newHex;
                iteration--;
            }

            int lenghtStack = stackHexes.Count;
            for (int i = 0; i < lenghtStack; i++)
                queueHexes.Enqueue(stackHexes.Pop());

            return queueHexes;
        }
        
        public bool CanReceiveUnit(Unit selfUnit)
        {
            if (Playable == false)
                if (selfUnit.Movement.IsAir == false)
                    return false;
            
            if (HaveAnyUnit())
            {
                foreach (var unit in Units)
                {
                    if (unit.Fraction != selfUnit.Fraction)
                        if (unit.Movement?.IsAir == selfUnit.Movement?.IsAir)
                            return false;

                    if (unit.Movement?.IsAir == selfUnit.Movement?.IsAir)
                        return false;
                }
            }
            
            return true;
        }

        public void CreateVisualEffect(VisualEffectType effectType)
        {
            if (VisualEffects.ContainsKey(effectType) == false)
            {
                GameObject VFX = Instantiate(VisualEffectPrefabs[(int) effectType], VFXContainer);
                VFX.transform.position = transform.position + Vector3.up * 0.5f;
                VisualEffects.Add(effectType, VFX);
            }
        }

        public void DestroyVisualEffect(VisualEffectType effectType)
        {
            if (VisualEffects.ContainsKey(effectType))
            {
                Destroy(VisualEffects[effectType]);
                VisualEffects.Remove(effectType);
            }
        }
        
        public void Select()
        {
        }

        public void Deselect()
        {
        }

        public void Action(GameObject selectObject)
        {
        }
    }
}