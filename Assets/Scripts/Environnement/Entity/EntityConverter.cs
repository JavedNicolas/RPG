using UnityEngine;
using System.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Sirenix.OdinInspector;

namespace RPG.Environnement.Entities
{
    public class EntityConverter : MonoBehaviour
    {
        static EntityManager _entityManager;
        MeshRenderer _meshRenderer;
        MeshFilter _meshFilter;
        Transform _transform;

        private void Start()
        {
            createEntity();
        }

        /// <summary>
        /// Convert the gameObject to an entity
        /// </summary>
        [Button("Create Entity")]
        public void createEntity()
        {
            if (_entityManager == null)
            {
                DefaultWorldInitialization.DefaultLazyEditModeInitialize();
                _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            }
               

            // Get the needed gameobject component
            if (!gameObject.TryGetComponent<MeshRenderer>(out _meshRenderer) ||
                !gameObject.TryGetComponent<MeshFilter>(out _meshFilter) ||
                !gameObject.TryGetComponent<Transform>(out _transform))
                return;

            Entity entity = _entityManager.CreateEntity(getArchetype());

            setEntity(entity);
            gameObject.SetActive(false);
        }

        /// <summary> Create the archetype</summary>
        EntityArchetype getArchetype()
        {
            EntityArchetype entityArchetype = _entityManager.CreateArchetype(
                typeof(Translation),
                typeof(Rotation),
                typeof(RenderMesh),
                typeof(LocalToWorld),
                typeof(NonUniformScale),
                typeof(RenderBounds)
            );

            return entityArchetype;
        }

        void setEntity(Entity entity)
        {
            // set the enetity
            _entityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = _meshFilter.sharedMesh,
                material = _meshRenderer.sharedMaterial
            });
            _entityManager.SetComponentData(entity, new Translation { Value = _transform.position });
            _entityManager.SetComponentData(entity, new Rotation { Value = _transform.localRotation });
            _entityManager.SetComponentData(entity, new NonUniformScale { Value = _transform.localScale });
        }
    }
}


