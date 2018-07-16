using System;
using RabbitMQReceiver;
using Xunit;

namespace RabbitMQUnitTests
{
    public class SoldierTests
    {
	    [Fact]
	    public void CreateSoldierWithExistingId()
	    {
		    var id = Guid.NewGuid();
		    var soldier = new Soldier(id, 0)
		    {
			    X = 5,
			    Y = 5
		    };

		    Assert.Equal(id, soldier.Id);
	    }
	}
}
