using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TinyRx {

	public class ObservableProperty<T> : IObservableProperty<T>, IReadOnlyObservebleProperty<T> {
		private readonly HashSet<IObserver<T>> _observers = new();
		private bool _isDisposed;

		// [SerializeField]
		private T _value;

		public T Value {
			get => _value;
			set => SetValue(value);
		}

		public ObservableProperty() { }

		public ObservableProperty(T initialValue) {
			_value = initialValue;
		}

		public void Dispose() {
			foreach (var observer in _observers) observer.OnCompleted();

			_observers.Clear();
			_isDisposed = true;
		}

		public IDisposable Subscribe(IObserver<T> observer) {
			// Assert.IsNotNull(observer)

			if (_isDisposed) {
				observer.OnCompleted();
				return Disposable.Empty;
			}

			observer.OnNext(_value);
			_observers.Add(observer);
			return Disposable.Create(() => OnObserverDispose(observer));
		}

		public void SetValueAndNotify(T value) {
			SetValue(value);
		}



		private void Notify(T value) {
			// Assert.IsFalse(_didDispose);

			foreach (var observer in _observers) observer.OnNext(value);
		}

		private void SetValue(T value, bool forceNotify = false) {
			// Assert.IsFalse(_didDispose);

			if (!forceNotify && EqualsInternal(Value, value))
				return;

			_value = value;
			Notify(value);
		}

		private void OnObserverDispose(IObserver<T> value) {
			if (_observers.Remove(value)) {
				value.OnCompleted();
			}
		}

		protected virtual bool EqualsInternal(T a, T b) {
			return EqualityComparer<T>.Default.Equals(a, b);
		}

	}
}



