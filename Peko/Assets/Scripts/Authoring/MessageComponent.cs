using UnityEngine;
using Unity.Entities;

namespace Peko.Authoring
{
    public class MessageComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public Sprite start;
        public Sprite conti;
        public Sprite def;
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var startEntity = conversionSystem.GetPrimaryEntity(start);
            dstManager.AddComponentData<StartMessage>(entity, new StartMessage
            {
                Value = startEntity
            });

            var contiEntity = conversionSystem.GetPrimaryEntity(conti);
            dstManager.AddComponentData<ContinueMessage>(entity, new ContinueMessage
            {
                Value = contiEntity
            });

            var defaultEntity = conversionSystem.GetPrimaryEntity(def);
            dstManager.AddComponentData<DefaultMessage>(entity, new DefaultMessage
            {
                Value = defaultEntity
            });

            dstManager.AddComponent<MessageTag>(entity);

        }
    }

    [UpdateInGroup(typeof(GameObjectDeclareReferencedObjectsGroup))]
    class DeclareMessageComponentReference : GameObjectConversionSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((MessageComponent mgr) =>
            {
                if(mgr.start != null)
                {
                    DeclareReferencedAsset(mgr.start);
                }                
                if(mgr.conti != null)
                {
                    DeclareReferencedAsset(mgr.conti);
                }
                if(mgr.def != null)
                {
                    DeclareReferencedAsset(mgr.def);
                }
            });
        }
    }
}
