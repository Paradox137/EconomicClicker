using AdvantTest.ECS.BusinessEntity.Components;
using AdvantTest.ECS.BusinessEntity.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace AdvantTest.ECS.BusinessEntity.Systems
{
	public class BusinessIncomeSystem : IEcsRunSystem
	{
		private EcsFilter<BusinessComponent, EarnIncome> _filter;
		
		public void Run()
		{
			foreach (int i in _filter)
			{
				ref var business = ref _filter.Get1(i);
				ref var earner = ref _filter.Get2(i);

				earner.TimeSpend += Time.deltaTime;

				earner.Progress = earner.TimeSpend  / earner.TimeToWait; 
				
				business.IncomeProgressUpdateCallback?.Invoke(earner.Progress);
				
				if (earner.TimeSpend >= earner.TimeToWait)
				{
					earner.TimeSpend  = 0;

					business.BalancePresenter.ReceiveMoney(business.BusinessIncome);
				}
			}
		}
	}
}
