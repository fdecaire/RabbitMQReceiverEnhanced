using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using RabbitMQReceiver;
using RabbitMQReceiver.DAL;

namespace RabbitMQReceiverEnhanced
{
	public class SoldierDatabaseManager : ISoldierDatabaseManager
	{
		private readonly IDatabaseContext _databaseContext;
		private readonly IDistinctQueue _distinctQueue;

		public SoldierDatabaseManager(IDatabaseContext databaseContext, IDistinctQueue distinctQueue)
		{
			_databaseContext = databaseContext;
			_distinctQueue = distinctQueue;
		}

		public List<Soldier> ReadAllSoldiers()
		{
			var soldiers = new List<Soldier>();

			foreach (var item in _databaseContext.SoldierRecords)
			{
				soldiers.Add(new Soldier(item.Id, 0)
				{
					X = item.X,
					Y = item.Y
				});
			}

			return soldiers.ToList();
		}

		public SoldierRecord FindOneSoldier(Guid id)
		{
			return (from s in _databaseContext.SoldierRecords
					where s.Id == id
					select s)
				.FirstOrDefault();
		}

		public void QueueMonitor()
		{
			while (true)
			{
				while (_distinctQueue.Count > 0)
				{
					var soldier = _distinctQueue.Dequeue();
					var data = FindOneSoldier(soldier.Id);

					if (data != null)
					{
						data.X = soldier.X;
						data.Y = soldier.Y;
						_databaseContext.SaveChanges();
					}
				}

				Thread.Sleep(1000); // 1 second
			}
		}
	}
}
