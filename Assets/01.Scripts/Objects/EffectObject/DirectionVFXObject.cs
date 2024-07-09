using UnityEngine;

namespace ObjectManage
{
    public class DirectionVFXObject : EffectObject
    {
        private ActionData _actionData;
        
        public override void Initialize(ActionData actionData)
        { 
            _actionData = actionData;
            transform.localScale = new Vector3(_actionData.direction.x > 0 ? 1f : -1f, 1, 1);
            transform.right = _actionData.direction;
            
        }
    }
}