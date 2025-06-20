using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace TinyRx.Tests.Observable {
    [TestFixture]
    public class AnonymousObservableTests {
        [Test]
        public void 正常系_Subscribeで値を受け取れること() {
            // Arrange
            var values = new List<int>();
            var observable = new AnonymousObservable<int>(observer => {
                observer.OnNext(1);
                observer.OnNext(2);
                observer.OnNext(3);
                observer.OnCompleted();
                return Disposable.Empty;
            });

            // Act
            observable.Subscribe(new Observer<int>(
                onNext: value => values.Add(value),
                onError: _ => Assert.Fail("エラーが発生してはいけない"),
                onCompleted: () => { }
            ));

            // Assert
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, values);
        }

        [Test]
        public void 正常系_Subscribeでエラーを受け取れること() {
            // Arrange
            var error = new Exception("テストエラー");
            var receivedError = default(Exception);
            var observable = new AnonymousObservable<int>(observer => {
                observer.OnError(error);
                return Disposable.Empty;
            });

            // Act
            observable.Subscribe(new Observer<int>(
                onNext: _ => Assert.Fail("値が通知されてはいけない"),
                onError: ex => receivedError = ex,
                onCompleted: () => Assert.Fail("完了が通知されてはいけない")
            ));

            // Assert
            Assert.That(receivedError, Is.SameAs(error));
        }

        [Test]
        public void 正常系_Subscribeで完了を受け取れること() {
            // Arrange
            var isCompleted = false;
            var observable = new AnonymousObservable<int>(observer => {
                observer.OnCompleted();
                return Disposable.Empty;
            });

            // Act
            observable.Subscribe(new Observer<int>(
                onNext: _ => Assert.Fail("値が通知されてはいけない"),
                onError: _ => Assert.Fail("エラーが通知されてはいけない"),
                onCompleted: () => isCompleted = true
            ));

            // Assert
            Assert.That(isCompleted, Is.True);
        }

        [Test]
        public void 正常系_Subscribeの戻り値のDisposableをDisposeできること() {
            // Arrange
            var isDisposed = false;
            var disposable = Disposable.Create(() => isDisposed = true);
            var observable = new AnonymousObservable<int>(observer => {
                observer.OnNext(1);
                return disposable;
            });

            // Act
            var subscription = observable.Subscribe(new Observer<int>(
                onNext: _ => { },
                onError: _ => { },
                onCompleted: () => { }
            ));
            subscription.Dispose();

            // Assert
            Assert.That(isDisposed, Is.True);
        }

        [Test]
        public void 異常系_Subscribeにnullを渡すと例外が発生すること() {
            // Arrange
            var observable = new AnonymousObservable<int>(observer => Disposable.Empty);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => observable.Subscribe(null));
        }
    }

}
