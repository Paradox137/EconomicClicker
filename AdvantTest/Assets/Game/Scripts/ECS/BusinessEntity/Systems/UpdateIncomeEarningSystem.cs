using AdvantTest.ECS.BusinessEntity.Components;
using AdvantTest.ECS.BusinessEntity.Requests;
using AdvantTest.Service.Player;
using Leopotam.Ecs;

namespace AdvantTest.ECS.BusinessEntity.Systems
{
	public class UpdateIncomeEarningSystem : IEcsRunSystem
	{
		private EcsFilter<BusinessComponent, UpgradeBought> _filter;
		public void Run()
		{
			foreach (int i in _filter)
			{
				ref var business = ref _filter.Get1(i);
				ref var sender = ref _filter.Get2(i);
				ref var entity = ref _filter.GetEntity(i);

				business.AvailableUpgrades[sender.UpgradeNumber] = true;
				
				business.BusinessIncome = UpgradeConfig.CalculateIncome(
					business.BusinessLvl, business.BusinessNumber, 
					business.AvailableUpgrades[0], 
					business.AvailableUpgrades[1]);
				
				entity.Del<UpgradeBought>();
			}
		}
	}
}
