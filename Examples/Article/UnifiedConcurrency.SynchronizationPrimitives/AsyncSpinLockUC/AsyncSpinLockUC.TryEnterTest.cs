﻿using System;
using System.Threading.Tasks;
using GreenSuperGreen.UnifiedConcurrency;
using NUnit.Framework;

// ReSharper disable RedundantExtendsListEntry

namespace UnifiedConcurrency.SynchronizationPrimitives
{
	public sealed class AsyncSpinLockUCTryEnter : ATestingJobAsync, ITestingJob
	{
		public AsyncSpinLockUCTryEnter(int count) : base(count) { }

		private IAsyncLockUC Lock { get; } = new AsyncSpinLockUC();

		protected override async Task<bool> ExclusiveAccess()
		{
			using (EntryBlockUC entry = await Lock.TryEnter())
			{
				if (!entry.HasEntry)
				{
					return true;//no entry, keep trying
				}
				return ProcessExclusively();
			}
		}
	}

	[TestFixture]
	public partial class UnifiedConcurrency
	{
		/// <summary> About 15 seconds </summary>
		[Test]
		public async Task AsyncSpinLockUCTryEnterTest()
		{
			using (ITestingJob job = new AsyncSpinLockUCTryEnter(10000))
			{
				await job.Execute(Environment.ProcessorCount);
			}
		}
	}
}
