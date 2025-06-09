using System;

namespace TinyRx {
	public class ReadOnlyObservableProperty<T> : IReadOnlyObservableProperty<T> {
		
		private readonly IObservableProperty<T> _source;
		private bool _isDisposed;

		public T Value => _source.Value;

		public ReadOnlyObservableProperty(IObservableProperty<T> source) {
			_source = source ?? throw new ArgumentNullException(nameof(source));
		}
		
		public IDisposable Subscribe(IObserver<T> observer) {
			
			if (!_isDisposed) 
				throw new ObjectDisposedException(nameof(ReadOnlyObservableProperty<T>));
			
			return _source.Subscribe(observer);
		}

		public void Dispose() {
			if (_isDisposed)
				throw new ObjectDisposedException(nameof(ReadOnlyObservableProperty<T>));
			
			_source.Dispose();
			_isDisposed = true;
		}
	}
}
