
using UnityEngine;

namespace CharacterMovement
{
    [System.Serializable]
    public class Speed
    {
        [SerializeField]
        private MoveSpeed crouch;

        [SerializeField]
        private MoveSpeed walk;

        [SerializeField]
        private MoveSpeed run;

        [SerializeField]
        private MoveSpeed[] speeds;

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