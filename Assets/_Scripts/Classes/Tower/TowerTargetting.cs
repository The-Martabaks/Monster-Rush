using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;

public class TowerTargetting
{   
    // For handling target type/target position
    public enum TargetType
    {
        First,
        Last,
        Close
    }

   public static Enemy GetTarget(TowerBehavior CurrentTower,TargetType TargetMethod)
    {
        Collider[] EnemiesInRange = Physics.OverlapSphere(CurrentTower.transform.position, CurrentTower.Range, CurrentTower.EnemiesLayer);
        if(EnemiesInRange.Length == 0 )
        {
            return null;
        }

        NativeArray<EnemyData> EnemiesToCalculate = new NativeArray<EnemyData>(EnemiesInRange.Length, Allocator.TempJob);
        NativeArray<Vector3> NodePositions = new NativeArray<Vector3>(GameLoopManager.NodePositions, Allocator.TempJob);
        NativeArray<float> NodeDistance = new NativeArray<float>(GameLoopManager.NodeDistance, Allocator.TempJob);
        NativeArray<int> EnemyToIndex = new NativeArray<int>(1, Allocator.TempJob);
        int EnemyIndexToReturn = -1;

        for(int i = 0; i< EnemiesToCalculate.Length; i++)
        {
            Enemy CurrentEnemy = EnemiesInRange[i].transform.parent.GetComponent<Enemy>();
            int EnemyIndexInList = EntitySummoner.EnemiesInGame.FindIndex(x => x == CurrentEnemy);

            EnemiesToCalculate[i] = new EnemyData(CurrentEnemy.transform.position, CurrentEnemy.NodeIndex, CurrentEnemy.Healt, EnemyIndexInList);
        }

        SearchForEnemy EnemySearchJob = new SearchForEnemy
        {
            _EnemiesToCalculate =  EnemiesToCalculate,
            _NodeDistances = NodeDistance,
            _NodePositions = NodePositions,
            _EnemyToIndex = EnemyToIndex,
            CompareValue = Mathf.Infinity,
            TargetingType = (int)TargetMethod,
            TowerPosition = CurrentTower.transform.position
        };

        switch ((int)TargetMethod)
        {
            case 0: // Fisrt
                EnemySearchJob.CompareValue = Mathf.Infinity;
                break;

            case 1:
                EnemySearchJob.CompareValue = Mathf.NegativeInfinity;
                break;

            case 2: // Close
                goto case 0;
        }

        JobHandle dependency = new JobHandle();
        JobHandle SearchJobHandle = EnemySearchJob.Schedule(EnemiesToCalculate.Length, dependency);
        SearchJobHandle.Complete();

        EnemyIndexToReturn = EnemiesToCalculate[EnemyToIndex[0]].EnemyIndex;

        EnemiesToCalculate.Dispose();
        NodePositions.Dispose();
        NodeDistance.Dispose();
        EnemyToIndex.Dispose();

        if(EnemyIndexToReturn == -1)
        {
            return null;
        }
        return EntitySummoner.EnemiesInGame[EnemyIndexToReturn];
    }

    struct EnemyData
    {
        public EnemyData(Vector3 position, int nodeindex, float hp, int enemyIndex)
        {
            EnemyPosition = position;
            NodeIndex = nodeindex;
            EnemyIndex = enemyIndex;
            Healt = hp; 
        }

        public Vector3 EnemyPosition;
        public int EnemyIndex;
        public int NodeIndex;
        public float Healt;

        
    }

    struct SearchForEnemy : IJobFor
    {
        [ReadOnly] public NativeArray<EnemyData> _EnemiesToCalculate;
        [ReadOnly] public NativeArray<Vector3> _NodePositions;
        [ReadOnly] public NativeArray<float> _NodeDistances;
        [NativeDisableParallelForRestriction] public NativeArray<int> _EnemyToIndex;
        public Vector3 TowerPosition;
        public float CompareValue;
        public int TargetingType;

        public void Execute(int index)
        {
            float CurrentEnemyDistanceToEnd = 0;
            float DistanceToEnemy = 0;
            switch(TargetingType)
            {
                case 0: // First

                    CurrentEnemyDistanceToEnd = GetDistanceToEnd(_EnemiesToCalculate[index]);
                    if( CurrentEnemyDistanceToEnd < CompareValue)
                    {
                        _EnemyToIndex[0] = index;
                        CompareValue = CurrentEnemyDistanceToEnd;
                    }

                    break;

                case 1: // Last

                    CurrentEnemyDistanceToEnd = GetDistanceToEnd(_EnemiesToCalculate[index]);
                    if( CurrentEnemyDistanceToEnd < CompareValue)
                    {
                        _EnemyToIndex[0] = index;
                        CompareValue = CurrentEnemyDistanceToEnd;
                    }

                    break;

                case 2: // Close

                    DistanceToEnemy = Vector3.Distance(TowerPosition, _EnemiesToCalculate[index].EnemyPosition);
                    if( DistanceToEnemy < CompareValue)
                    {
                        _EnemyToIndex[0] = index;
                        CompareValue = DistanceToEnemy;
                    }

                    break;
            }
        }
        private float GetDistanceToEnd(EnemyData EnemyToEvaluate)
        {
            float FinalDistance = Vector3.Distance(EnemyToEvaluate.EnemyPosition, _NodePositions[EnemyToEvaluate.NodeIndex - 1]); 

            for(int i = EnemyToEvaluate.NodeIndex; i < _NodePositions.Length; i++)
            {
                FinalDistance += _NodeDistances[i];
            }

            return FinalDistance;
        }
    }
}
