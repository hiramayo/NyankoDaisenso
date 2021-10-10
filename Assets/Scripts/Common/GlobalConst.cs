using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Common
{
    /// <summary>
    /// 共通定数
    /// </summary>
    public static class GlobalConst
    {
        public const float REDUCTION_RATIO = 90;
        public const int FACING_LEFT = 1;
        public const int FACING_RIGHT = -1;
        public const int ENEMY_CAMP = FACING_RIGHT; //敵軍
        public const int OWN_CAMP = FACING_LEFT; //自軍
        public const int MOVE_LEFT = -1;
        public const int MOVE_RIGHT = 1;
        public const string OWN_CASTLE = "house";
        public const string ENEMY_CASTLE = "face-block";
        public const int FLAME_RATE_ORIGINAL = 30; 

        public static Vector3 OWN_APPERRENCE_PLACE = getOwnApperencePlace; //キャッシュ
        private static Vector3 getOwnApperencePlace
        {
            get
            {
                return new Vector3(8, -1, 0);
            }
        }
        public static Vector3 ENEMY_APPERRENCE_PLACE = getEnemyApperencePlace; //キャッシュ
        private static Vector3 getEnemyApperencePlace
        {
            get
            {
                return new Vector3(-8, -1, 0);
            }
        }
    }

}