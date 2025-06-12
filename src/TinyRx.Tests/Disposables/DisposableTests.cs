using System;
using NUnit.Framework;
using TinyRx;

namespace TinyRx.Tests.Disposables {
    [TestFixture]
    public class DisposableTests {
        [Test]
        public void 正常系_Disposeでアクションが実行されること() {
            // Arrange
            var isDisposed = false;
            var disposable = Disposable.Create(() => isDisposed = true);

            // Act
            disposable.Dispose();

            // Assert
            Assert.That(isDisposed, Is.True);
        }

        [Test]
        public void 正常系_Disposeは複数回呼び出しても例外が発生しないこと() {
            // Arrange
            var disposable = Disposable.Create(() => { });

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                disposable.Dispose();
                disposable.Dispose();
            });
        }

        [Test]
        public void 正常系_Emptyは何もしないこと() {
            // Arrange
            var disposable = Disposable.Empty;

            // Act & Assert
            Assert.DoesNotThrow(() => disposable.Dispose());
        }

        [Test]
        public void 正常系_CompositeDisposableで複数のDisposableを管理できること() {
            // Arrange
            var isDisposed1 = false;
            var isDisposed2 = false;
            var disposable1 = Disposable.Create(() => isDisposed1 = true);
            var disposable2 = Disposable.Create(() => isDisposed2 = true);
            var composite = new CompositeDisposable();
            composite.Add(disposable1);
            composite.Add(disposable2);

            // Act
            composite.Dispose();

            // Assert
            Assert.That(isDisposed1, Is.True);
            Assert.That(isDisposed2, Is.True);
        }

        [Test]
        public void 正常系_SerialDisposableで現在のDisposableを置き換えられること() {
            // Arrange
            var isDisposed1 = false;
            var isDisposed2 = false;
            var disposable1 = Disposable.Create(() => isDisposed1 = true);
            var disposable2 = Disposable.Create(() => isDisposed2 = true);
            var serial = new SerialDisposable();

            // Act
            serial.Disposable = disposable1;
            serial.Disposable = disposable2;
            serial.Dispose();

            // Assert
            Assert.That(isDisposed1, Is.True);
            Assert.That(isDisposed2, Is.True);
        }

        [Test]
        public void 正常系_SerialDisposableのDisposeは複数回呼び出しても例外が発生しないこと() {
            // Arrange
            var serial = new SerialDisposable();

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                serial.Dispose();
                serial.Dispose();
            });
        }

        [Test]
        public void AnonymousDisposableは指定したアクションを実行すること() {
            // Arrange
            var isDisposed = false;
            var disposable = new AnonymousDisposable(() => isDisposed = true);

            // Act
            disposable.Dispose();

            // Assert
            Assert.That(isDisposed, Is.True);
        }

        [Test]
        public void AnonymousDisposableは複数回呼び出しても1回だけ実行されること() {
            // Arrange
            var disposeCount = 0;
            var disposable = new AnonymousDisposable(() => disposeCount++);

            // Act
            disposable.Dispose();
            disposable.Dispose();
            disposable.Dispose();

            // Assert
            Assert.That(disposeCount, Is.EqualTo(1));
        }

        [Test]
        public void CompositeDisposableに追加したDisposableも管理されること() {
            // Arrange
            var disposeCount = 0;
            var disposable = new AnonymousDisposable(() => disposeCount++);
            var composite = new CompositeDisposable();

            // Act
            composite.Add(disposable);
            composite.Dispose();

            // Assert
            Assert.That(disposeCount, Is.EqualTo(1));
        }

        [Test]
        public void CancellationDisposableはCancellationTokenSourceを管理できること() {
            // Arrange
            var cts = new System.Threading.CancellationTokenSource();
            var disposable = new CancellationDisposable(cts);

            // Act
            disposable.Dispose();

            // Assert
            Assert.That(cts.IsCancellationRequested, Is.True);
        }
    }
} 