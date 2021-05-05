using Unity.Entities;
using UnityEngine;

namespace Peko.Authoring
{
    public class PlayerWaitAnimComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent<PlayerWaitAnimTag>(entity);
        }
        
    }
}
