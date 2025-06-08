using System;

namespace TinyRx {

	internal sealed class EmptyDisposable : IDisposable {
		public static EmptyDisposable Singlton = new();

		public EmptyDisposable() { }

		public void Dispose() { }
	}

}
