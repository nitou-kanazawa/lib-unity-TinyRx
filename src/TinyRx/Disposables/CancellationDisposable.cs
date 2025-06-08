using System;
using System.Threading;

namespace TinyRx {

	public sealed class CancellationDisposable : IDisposable {

		private readonly CancellationTokenSource _cts;

		public bool IsDisposed => _cts.IsCancellationRequested;

		public CancellationToken Token => _cts.Token;


		public CancellationDisposable(CancellationTokenSource cts) {
			if (cts == null)
				throw new ArgumentNullException(nameof(cts));

			_cts = cts;
		}

		public CancellationDisposable() : this(new CancellationTokenSource()) {

		}

		public void Dispose() {
			_cts.Cancel();
		}

	}

}
