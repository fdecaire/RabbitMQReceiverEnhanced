using RabbitMQReceiver;

namespace RabbitMQReceiverEnhanced
{
	public interface IDistinctQueue
	{
		void Enqueue(Soldier soldier);
		Soldier Dequeue();
		int Count { get; }
	}
}
