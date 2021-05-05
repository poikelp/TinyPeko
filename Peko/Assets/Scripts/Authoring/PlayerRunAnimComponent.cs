using Unity.Entities;
using UnityEngine;

namespace Peko.Authoring
{
    public class PlayerRunAnimComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent<PlayerRunAnimTag>(entity);
        }
        
    }
}
