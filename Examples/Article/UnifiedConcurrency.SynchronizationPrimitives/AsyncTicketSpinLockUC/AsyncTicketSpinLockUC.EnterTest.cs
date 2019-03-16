﻿using System;
using System.Threading.Tasks;
using GreenSuperGreen.UnifiedConcurrency;
using NUnit.Framework;

// ReSharper disable RedundantExtendsListEntry

namespace UnifiedConcurrency.SynchronizationPrimitives
{
	public sealed class AsyncTicketSpinLockUCEnter : ATestingJobAsync, ITestingJob
	{
		public AsyncTicketSpinLockUCEnter(int count) : base(count) { }

		private IAsyncLockUC Lock { get; } = new AsyncTicketSpinLockUC();

		protected override async Task<bool> ExclusiveAccess()
		{
			using (EntryBlockUC entry = await Lock.Enter())
			{
				if (!entry.HasEntry) throw new Exception("should not happen");
				return ProcessExclusively();
			}
		}
	}

	[TestFixture]
	public partial class UnifiedConcurrency
	{
		[Test]
		public async Task AsyncTicketSpinLockUCEnterTest()
		{
			using (ITestingJob job = new AsyncTicketSpinLockUCEnter(10000))
			{
				await job.Execute(Environment.ProcessorCount);
			}
		}
	}
}
