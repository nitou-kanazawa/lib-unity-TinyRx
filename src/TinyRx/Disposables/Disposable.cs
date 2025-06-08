using System;

namespace TinyRx {

	public static class Disposable {
		public static readonly IDisposable Empty = EmptyDisposable.Singlton;

		public static IDisposable Create(Action disposeAction) {
			return new AnonymousDisposable(disposeAction);
		}

		public static IDisposable CreateWithState<TState>(TState state, Action<TState> disposeAction) {
			return new AnonymousDisposable<TState>(state, disposeAction);
		}

	}

	internal static class DisposableExtensions {


	}
}
