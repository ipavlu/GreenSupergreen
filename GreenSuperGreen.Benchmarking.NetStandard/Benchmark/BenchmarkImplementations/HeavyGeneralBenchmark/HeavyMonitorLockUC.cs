﻿using GreenSuperGreen.UnifiedConcurrency;

// ReSharper disable CheckNamespace

namespace GreenSuperGreen.Benchmarking
{
#pragma warning disable 618
	internal class HeavyMonitorLockUC : HeavyGeneralBenchmark<MonitorLockUC>
	{
		public HeavyMonitorLockUC(IBenchmarkConfiguration test) : base(test) { }
	}
#pragma warning restore 618
}