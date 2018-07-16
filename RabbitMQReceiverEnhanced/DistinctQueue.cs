using System.Collections.Generic;
using System.Linq;
using RabbitMQReceiver;

namespace RabbitMQReceiverEnhanced
{
	public class DistinctQueue : IDistinctQueue
	{
		private readonly List<Soldier> _queueList = new List<Soldier>();

		public void Enqueue(Soldier soldier)
		{
			var listItem = -1;

			if (soldier == null)
			{
				return;
			}

			lock (_queueList)
			{
				if (_queueList.Count > 0)
				{
					listItem = _queueList.ToList().FindIndex(x => x.Id == soldier.Id);
				}

				if (listItem == -1)
				{
					_queueList.Add(soldier);
				}
				else
				{
					_queueList[listItem].X = soldier.X;
					_queueList[listItem].Y = soldier.Y;

				}
			}
		}

		public Soldier Dequeue()
		{
			lock (_queueList)
			{
				if (_queueList.Count == 0)
				{
					return null;
				}

				var frontOfQueue = _queueList[0];
				_queueList.RemoveAt(0);
				return frontOfQueue;
			}
		}

		public int Count
		{
			get
			{
				lock (_queueList)
				{
					return _queueList.Count;
				}
			}
		}
	}
}
