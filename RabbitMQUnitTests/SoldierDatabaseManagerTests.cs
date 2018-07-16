using System;
using RabbitMQReceiver.DAL;
using RabbitMQReceiverEnhanced;
using Xunit;

namespace RabbitMQUnitTests
{
    public class SoldierDatabaseManagerTests : IClassFixture<TestDataFixture>
	{
	    private readonly DatabaseContext _databaseContext;
	    private readonly TestDataFixture _fixture;

	    public SoldierDatabaseManagerTests(TestDataFixture fixture)
	    {
		    _fixture = fixture;
		    _databaseContext = fixture.Context;
	    }

	    private void ResetRecords()
	    {
		    _fixture.ResetData();
	    }

		[Fact]
	    public void FindOneSoldierNoSoldier()
	    {
			ResetRecords();

			var queue = new DistinctQueue();
		    var soldierDatabaseManager = new SoldierDatabaseManager(_databaseContext,queue);

			Assert.Null(soldierDatabaseManager.FindOneSoldier(Guid.NewGuid()));
	    }

		[Fact]
		public void FindOneSoldierOneMatch()
		{
			ResetRecords();

			var soldier = new SoldierRecord
			{
				X = 5,
				Y = 5
			};

			_databaseContext.SoldierRecords.Add(soldier);
			_databaseContext.SaveChanges();

			var queue = new DistinctQueue();
			var soldierDatabaseManager = new SoldierDatabaseManager(_databaseContext, queue);

			Assert.NotNull(soldierDatabaseManager.FindOneSoldier(soldier.Id));
		}
    }
}
