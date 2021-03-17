using System.Collections;
using System.Collections.Generic;
using Environment.Hex;
using Helpers;
using UnityEngine;

namespace Units.Possibilities.Move
{
    public class MobilityMovement : CommonMovement
    {
        public Queue<Hex> Hexes { get; set; }
        public Hex TargetHex;
        
        public override void Move(Unit unit, Hex targetHex)
        {
            if (targetHex.Points[0] <= 0)
                return;
            
            Hexes = new Queue<Hex>();
            
            unit.MapManager.ResetCheckAllHexes();
            
            PavePath(unit, targetHex);
            //StartCoroutine(BezierMovingTo(unit, MovementSpeed));
            StartCoroutine(MovingTo(unit, MovementSpeed));

            CountMovePoints -= targetHex.Points[0];
        }

        public override void FindPath(Unit unit)
        {
            unit.Hex.MovementPathFinding(unit);
        }
        
        private void PavePath(Unit unit, Hex targetHex)
        {
            Hexes = unit.Hex.PaveMovementPath(unit, targetHex);
            TargetHex = targetHex;
        }

        private IEnumerator BezierMovingTo(Unit unit, float duration)
        {
            duration *= Hexes.Count;

            Vector3 offset = Vector3.up * unit.DistanceAboveGround;
            Vector3 startPosition = unit.Hex.transform.position + offset;
            List<Vector3> points = new List<Vector3>();
            
            float t = 0;
            Hex targetHex = unit.Hex;

            if (unit.Animators.Count > 0)
            {
                foreach (var animator in unit.Animators)
                {
                    animator.SetBool("Run", true);
                }
            }
            switch (Hexes.Count)
            {
                case 1:
                {
                    points.Add(Hexes.Peek().transform.position);
                    targetHex = Hexes.Dequeue();

                    unit.Hex.Units.Remove(unit);
                    unit.Hex = targetHex;
                    unit.Hex.Units.Add(unit);
                    
                    while (t < duration)
                    {
                        unit.transform.position = BezierCurve.GetPoint(startPosition, points[0] + offset, t / duration);
                        unit.transform.rotation =
                            Quaternion.LookRotation(BezierCurve.GetDerivative(startPosition, points[0] + offset,
                                t / duration));
                        
                        t += Time.deltaTime;
                        yield return null;
                    }

                    break;
                }
                case 2:
                {
                    points.Add(Hexes.Dequeue().transform.position);
                    points.Add(Hexes.Peek().transform.position);
                    targetHex = Hexes.Dequeue();

                    unit.Hex.Units.Remove(unit);
                    unit.Hex = targetHex;
                    unit.Hex.Units.Add(unit);
                    
                    while (t < duration)
                    {
                        unit.transform.position = BezierCurve.GetPoint(startPosition, points[0] + offset,
                            points[1] + offset, t / duration);
                        unit.transform.rotation = Quaternion.LookRotation(BezierCurve.GetDerivative(startPosition,
                            points[0] + offset, points[1] + offset, t / duration));
                        
                        t += Time.deltaTime;
                        yield return null;
                    }

                    break;
                }
                case 3:
                {
                    points.Add(Hexes.Dequeue().transform.position);
                    points.Add(Hexes.Dequeue().transform.position);
                    points.Add(Hexes.Peek().transform.position);
                    targetHex = Hexes.Dequeue();

                    unit.Hex.Units.Remove(unit);
                    unit.Hex = targetHex;
                    unit.Hex.Units.Add(unit);
                    
                    while (t < duration)
                    {
                        unit.transform.position = BezierCurve.GetPoint(startPosition, points[0] + offset,
                            points[1] + offset, points[2] + offset, t / duration);
                        unit.transform.rotation = Quaternion.LookRotation(BezierCurve.GetDerivative(startPosition,
                            points[0] + offset, points[1] + offset, points[2] + offset, t / duration));
                        
                        t += Time.deltaTime;
                        yield return null;
                    }

                    break;
                }
                case 4:
                {
                    points.Add(Hexes.Dequeue().transform.position);
                    points.Add(Hexes.Dequeue().transform.position);
                    points.Add(Hexes.Dequeue().transform.position);
                    points.Add(Hexes.Peek().transform.position);
                    targetHex = Hexes.Dequeue();

                    unit.Hex.Units.Remove(unit);
                    unit.Hex = targetHex;
                    unit.Hex.Units.Add(unit);

                    duration /= 2;
                    while (t < duration)
                    {
                        unit.transform.position = BezierCurve.GetPoint(startPosition, points[0] + offset,
                            points[1] + offset, t / duration);
                        unit.transform.rotation = Quaternion.LookRotation(BezierCurve.GetDerivative(startPosition,
                            points[0] + offset, points[1] + offset, t / duration));
                        
                        t += Time.deltaTime;
                        yield return null;
                    }
                    t = 0;
                    
                    while (t < duration)
                    {
                        unit.transform.position = BezierCurve.GetPoint(points[1] + offset, points[2] + offset,
                            points[3] + offset, t / duration);
                        unit.transform.rotation = Quaternion.LookRotation(BezierCurve.GetDerivative(points[1] + offset,
                            points[2] + offset, points[3] + offset, t / duration));
                        
                        t += Time.deltaTime;
                        yield return null;
                    }

                    break;
                }
                case 5:
                {
                    points.Add(Hexes.Dequeue().transform.position);
                    points.Add(Hexes.Dequeue().transform.position);
                    points.Add(Hexes.Dequeue().transform.position);
                    points.Add(Hexes.Dequeue().transform.position);
                    points.Add(Hexes.Peek().transform.position);
                    targetHex = Hexes.Dequeue();

                    unit.Hex.Units.Remove(unit);
                    unit.Hex = targetHex;
                    unit.Hex.Units.Add(unit);

                    duration /= 2.2f;
                    while (t < duration)
                    {
                        unit.transform.position = BezierCurve.GetPoint(startPosition, points[0] + offset,
                            points[1] + offset, points[2] + offset, t / duration);
                        unit.transform.rotation = Quaternion.LookRotation(BezierCurve.GetDerivative(startPosition,
                            points[0] + offset, points[1] + offset, points[2] + offset, t / duration));
                        
                        t += Time.deltaTime;
                        yield return null;
                    }
                    t = 0;
                    
                    while (t < duration)
                    {
                        unit.transform.position = BezierCurve.GetPoint(points[2] + offset, points[3] + offset,
                            points[4] + offset, t / duration);
                        unit.transform.rotation = Quaternion.LookRotation(BezierCurve.GetDerivative(points[2] + offset,
                            points[3] + offset, points[4] + offset, t / duration));
                        
                        t += Time.deltaTime;
                        yield return null;
                    }

                    break;
                }
            }
            if (unit.Animators.Count > 0)
            {
                foreach (var animator in unit.Animators)
                {
                    animator.SetBool("Run", false);
                }
            }
        }

        private IEnumerator MovingTo(Unit unit, float duration)
        {
            Vector3 heightOffset = Vector3.up * unit.DistanceAboveGround;
            float smoothRotation;
            float t;
            
            unit.Hex.Units.Remove(unit);
            unit.Hex = TargetHex;
            TargetHex.Units.Add(unit);
            
            if (unit.Animators.Count > 0)
            {
                foreach (var animator in unit.Animators)
                {
                    animator.SetBool("Run", true);
                }
            }
            
            while (Hexes.Count > 0)
            {
                Vector3 prevPosition = unit.transform.position;
                Hex targetHex = Hexes.Dequeue();
                
                Quaternion prevRotation = unit.transform.rotation;
                Quaternion targetRotation = Quaternion.LookRotation(targetHex.transform.position - prevPosition);

                t = 0;
                while (t < duration)
                {
                    unit.transform.rotation = Quaternion.Slerp(prevRotation, targetRotation, 6f * t / duration);
                    unit.transform.position = Vector3.Lerp(prevPosition,
                        targetHex.transform.position + heightOffset, t / duration);
                    t += Time.deltaTime;
                    yield return null;
                }

                unit.transform.position = targetHex.transform.position + heightOffset;
            }

            if (unit.Animators.Count > 0)
            {
                foreach (var animator in unit.Animators)
                {
                    animator.SetBool("Run", false);
                }
            }
        }
    }
}