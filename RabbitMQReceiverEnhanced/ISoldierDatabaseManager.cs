using System.Collections.Generic;
using RabbitMQReceiver;

namespace RabbitMQReceiverEnhanced
{
	public interface ISoldierDatabaseManager
	{
		List<Soldier> ReadAllSoldiers();
		void QueueMonitor();
	}
}
