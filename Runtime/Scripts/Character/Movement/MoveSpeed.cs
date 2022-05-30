using UnityEngine;

namespace CharacterMovement
{
    [System.Serializable]
    public class MoveSpeed
    {
        [SerializeField]
        private float minSpeed;

        [SerializeField]
        private float maxSpeed;

        [SerializeField]
        private float acceleration;

        [SerializeField]
        private float deceleration;

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
}