using RabbitMQReceiver;
using RabbitMQReceiverEnhanced;
using Xunit;

namespace RabbitMQUnitTests
{
    public class DistinctQueueTests
    {
        [Fact]
        public void AddThreeItems()
        {
	        var queue = new DistinctQueue();

			queue.Enqueue(new Soldier(true)
			{
				X=45,
				Y=30
			});
			queue.Enqueue(new Soldier(true)
			{
				X=20,
				Y=98
			});
			queue.Enqueue(new Soldier(true)
			{
				X=28,
				Y=75
			});

			Assert.Equal(3,queue.Count);
        }

	    [Fact]
	    public void AddDuplicateSoldier()
	    {
			var queue = new DistinctQueue();

		    var tempSoldier = new Soldier(true)
		    {
			    X = 45,
			    Y = 30
		    };
			queue.Enqueue(tempSoldier);
		    queue.Enqueue(new Soldier(true)
		    {
			    X = 20,
			    Y = 98
		    });
			queue.Enqueue(tempSoldier);

			Assert.Equal(2,queue.Count);
		}
    }
}
