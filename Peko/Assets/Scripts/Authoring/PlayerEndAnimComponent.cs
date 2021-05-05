using Unity.Entities;
using UnityEngine;

namespace Peko.Authoring
{
    public class PlayerEndAnimComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent<PlayerEndAnimTag>(entity);
        }
        
    }
}
