﻿using System;
using System.Threading.Tasks;
using GreenSuperGreen.UnifiedConcurrency;
using NUnit.Framework;

// ReSharper disable RedundantExtendsListEntry

namespace UnifiedConcurrency.SynchronizationPrimitives
{
	public sealed class LockTryEnter : ATestingJob, ITestingJob
	{
		public LockTryEnter(int count) : base(count) { }

		private ISimpleLockUC Lock { get; } = new LockUC();

		protected override bool ExclusiveAccess()
		{
			using (EntryBlockUC entry = Lock.TryEnter())
			{
				if (!entry.HasEntry) return true;//no entry, keep trying
				return ProcessExclusively();
			}
		}
	}

	[TestFixture]
	public partial class UnifiedConcurrency
	{
		[Test]
		public async Task LockTryEnterTest()
		{
			using (ITestingJob job = new LockEnter(100000))
			{
				await job.Execute(Environment.ProcessorCount);
			}
		}
	}
}
