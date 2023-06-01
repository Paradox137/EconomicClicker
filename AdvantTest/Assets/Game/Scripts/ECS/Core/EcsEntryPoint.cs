using AdvantTest.Data;
using AdvantTest.ECS.BusinessEntity.Systems;
using AdvantTest.ECS.UpgradeEntity.Systems;
using Leopotam.Ecs;
using UnityEngine;

namespace AdvantTest.ECS.Core
{
	public class EcsEntryPoint : MonoBehaviour
	{
		private EcsWorld _world;
		private EcsSystems _systems;

		[SerializeField]
		private ConfigsData _configsData;
		[SerializeField]
		private ViewsData _viewsData;

		void Start()
		{
			_world = new EcsWorld();
			_systems = new EcsSystems(_world);

#if UNITY_EDITOR
			Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
			Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif

			_systems
				.Add(new GameInitSystem())
				.Add(new BusinessInitSystem())
				.Add(new UpgradeInitSystem())
				.Add(new BusinessIncomeSystem())
				.Add(new TryBuyNextLvlSystem())
				.Add(new TryBuyUpgradeSystem())
				.Add(new UpdateIncomeEarningSystem())
				.Inject(_world)
				.Inject(_configsData)
				.Inject(_viewsData)
				.Init();
		}

		private void Update()
		{
			_systems?.Run();
		}

		void OnDestroy()
		{
			_systems?.Destroy();

			_systems = null;

			if (_world.IsAlive())
			{
				_world?.Destroy();
			}

			_world = null;
		}
	}
}
