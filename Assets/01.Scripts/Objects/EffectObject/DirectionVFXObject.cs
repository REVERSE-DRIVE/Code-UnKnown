using UnityEngine;

namespace ObjectManage
{
    public class DirectionVFXObject : EffectObject
    {
        private ActionData _actionData;
        
        public override void Initialize(ActionData actionData)
        { 
            _actionData = actionData;
            transform.position = actionData.origin;
            transform.localScale = new Vector3(_actionData.direction.x > 0 ? 1f : -1f, 1, 1);
            Quaternion rotate = Quaternion.Euler(0, 0,
                Mathf.Atan2(_actionData.direction.y, _actionData.direction.x) * Mathf.Rad2Deg);
            for (int i = 0; i < _particles.Length; i++)
            {
                _particles[i].startRotation = rotate.z;
            }
        }
    }
}