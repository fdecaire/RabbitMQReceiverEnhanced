using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RabbitMQReceiver.DAL;

namespace RabbitMQUnitTests
{
    public class TestDataFixture : IDisposable
    {
	    public DatabaseContext Context { get; set; }

		public TestDataFixture()
	    {
		    var builder = new DbContextOptionsBuilder<DatabaseContext>()
			    .UseInMemoryDatabase("DemoData");
		    Context = new DatabaseContext(builder.Options);
		}

	    public void ResetData()
	    {
		    var allSoldierRecords = from s in Context.SoldierRecords select s;
		    Context.SoldierRecords.RemoveRange(allSoldierRecords);

		    ((DbContext)Context).SaveChanges();
		}

	    public void Dispose()
	    {

	    }
	}
}
