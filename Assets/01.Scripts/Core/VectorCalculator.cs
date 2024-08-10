using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculator
{
    
    public class VectorCalculator
    {
        public static Vector2[] DirectionsFromCenter(int directionAmount)
        {
            float angleStep = 360f / directionAmount; // 각도 단계 계산
            Vector2[] result = new Vector2[directionAmount];
            for (int i = 0; i < directionAmount; i++)
            {
                // 각도 계산
                float angle = i * angleStep;
                float radians = angle * Mathf.Deg2Rad;
                result[i] = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            }

            return result;
        }

        
        public static Vector2 RotateVector2(Vector2 vec, float degrees)
        {
            float radians = degrees * Mathf.Deg2Rad;
            float sin = Mathf.Sin(radians);
            float cos = Mathf.Cos(radians);
            float x = vec.x;
            float y = vec.y;
            vec.x = x * cos - y * sin;
            vec.y = x * sin + y * cos;
            return vec;
        }
    }
    
    
}
