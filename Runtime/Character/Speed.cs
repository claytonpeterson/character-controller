using UnityEngine;

namespace CharacterMovement
{
    [System.Serializable]
    public class Speed
    {
        [System.Serializable]
        private class MoveSpeed
        {
            [SerializeField] private float minSpeed;
            [SerializeField] private float maxSpeed;
            [SerializeField] private float acceleration;
            [SerializeField] private float deceleration;

            private float lerp;

            public float Speed()
            {
                return Mathf.Lerp(minSpeed, maxSpeed, lerp);
            }

            public float Percent()
            {
                return lerp;
            }

            public void Accelerate()
            {
                if (lerp < 1)
                    lerp += Time.deltaTime / acceleration;
            }

            public void Decelerate()
            {
                if (lerp > 0)
                    lerp -= Time.deltaTime / deceleration;
            }

            public void Reset()
            {
                lerp = 0;
            }
        }

        [SerializeField] private MoveSpeed[] speeds;

        private int speedIndex = 0;

        public void SetSpeed(int speedIndex)
        {
            if (speedIndex < speeds.Length)
            {
                speeds[speedIndex].Reset();
                this.speedIndex = speedIndex;
            }
        }

        public float CurrentSpeed()
        {
            return speeds[speedIndex].Speed();
        }

        public float Percent()
        {
            return speeds[speedIndex].Percent();
        }

        public void UpdateSpeed(bool isAccelerating)
        {
            if(isAccelerating)
                speeds[speedIndex].Accelerate();
            else
                speeds[speedIndex].Decelerate();
        }
    }
}