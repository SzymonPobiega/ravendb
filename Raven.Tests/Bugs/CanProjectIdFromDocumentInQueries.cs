//-----------------------------------------------------------------------
// <copyright file="CanProjectIdFromDocumentInQueries.cs" company="Hibernating Rhinos LTD">
//     Copyright (c) Hibernating Rhinos LTD. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Abstractions.Indexing;
using Raven.Tests.Common;

using Xunit;
using Raven.Client.Indexes;

namespace Raven.Tests.Bugs
{
	public class CanProjectIdFromDocumentInQueries : RavenTest
	{
		[Fact]
		public void SelectIdFromDocumentWithIndexedQuery()
		{
			using (var store = NewDocumentStore())
			{

				store.DatabaseCommands.PutIndex(
					"AmazingIndex",
					new IndexDefinitionBuilder<Shipment>()
					{
						Map = docs => from doc in docs
									  select new
									  {
										  doc.Id
									  }
					}.ToIndexDefinition(store.Conventions));


				using (var session = store.OpenSession())
				{
					session.Store(new Shipment()
					{
						Id = "shipment1",
						Name = "Some shipment"
					});
					session.SaveChanges();

					var shipment = session.Query<Shipment>("AmazingIndex")
						.Customize(x=>x.WaitForNonStaleResults())
						.Select(x => new Shipment
						{
							Id = x.Id,
							Name = x.Name
						}).Take(1).SingleOrDefault();

					Assert.NotNull(shipment.Id);
				}
			}
		}

		[Fact]
		public void SelectIdFromDocumentWithDynamicQuery()
		{
			using (var store = NewDocumentStore())
			{
				using (var session = store.OpenSession())
				{
					session.Store(new Shipment()
					{
						Id = "shipment1",
						Name = "Some shipment"
					});
					session.SaveChanges();

					var shipment = session.Query<Shipment>()
						.Customize(x => x.WaitForNonStaleResults())
						.Select(x => new Shipment()
						{
							Id = x.Id,
							Name = x.Name
						}).SingleOrDefault();

					Assert.NotNull(shipment.Id);
				}
			}
		}

		public class Shipment
		{
			public string Id { get; set; }
			public string Name { get; set; }
		}
	}
}
