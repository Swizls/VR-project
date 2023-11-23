using UnityEngine;

namespace EnemyAI.Utilites
{
    public static class EnemyBehaviourUtilities
    {
        private const float DISTANCE_TO_CONFIRM_POSITION = 0.2f;
        public static bool CheckIsDestinationReached(EnemyBehaviourHandler _enemyReference, Vector3 targetPosition)
        {
            float distance = Vector3.Distance(_enemyReference.transform.position, targetPosition);
            if (distance < DISTANCE_TO_CONFIRM_POSITION)
            {
                return true;
            }
            return false;
        }
    }
}