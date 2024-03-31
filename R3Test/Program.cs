// See https://aka.ms/new-console-template for more information
using System;
using System.Diagnostics.CodeAnalysis;
using R3;

var numbers = new MySequenceOfNumbers();
var observer = new MyConsoleObserver<int>();
numbers.Subscribe(observer);


public class MyConsoleObserver<T> : Observer<T>
{
    protected override void OnNextCore(T value) => Console‌.WriteLine("Received value {0}", value);
    protected override void OnErrorResumeCore(Exception error) => Console‌.WriteLine("Sequence faulted with {0}", error);
    protected override void OnCompletedCore(Result result) => Console‌.WriteLine("Sequence terminated");
}

class MySequenceOfNumbers : Observable<int>
{
    protected override IDisposable SubscribeCore(Observer<int> observer)
    {
        observer.OnNext(1);
        observer.OnNext(2);
        observer.OnNext(3);
        observer.OnCompleted();
        return Disposable.Empty;
    }
}

