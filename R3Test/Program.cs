// See https://aka.ms/new-console-template for more information
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using R3;

//Sample1();
//Sample2();
//ReplaySubjectWindowExample();
OnErrorAndOnCompleted();

void OnErrorAndOnCompleted()
{
    var subject = new Subject<int>();
    subject.Do(
        onNext: (v) => Console.WriteLine(v),
        onCompleted: (v) => Console.WriteLine($"Completed {v}")
        ).Subscribe();
    subject.OnCompleted();
    subject.OnNext(2);
}

void Sample1()
{
    var numbers = new MySequenceOfNumbers();
    var observer = new MyConsoleObserver<int>();
    numbers.Subscribe(observer);
}

void Sample2()
{
    var subject = new Subject<string>();
    // var subject = new ReplaySubject<string>();
    WriteSequenceToConsole(subject);
    subject.OnNext("a");
    subject.OnNext("b");
    subject.OnNext("c");
    Console.ReadKey();
}

void WriteSequenceToConsole(Observable<string> sequence)
{
    sequence.Subscribe(Console.WriteLine);
}

void ReplaySubjectWindowExample()
{
    var window = TimeSpan.FromMilliseconds(150);
    var subject = new ReplaySubject<string>(window);
    subject.OnNext("w");
    Thread.Sleep(TimeSpan.FromMilliseconds(100));
    subject.OnNext("x");
    Thread.Sleep(TimeSpan.FromMilliseconds(100));
    subject.OnNext("y");
    subject.Subscribe(Console.WriteLine);
    subject.OnNext("z");
}

class MyConsoleObserver<T> : Observer<T>
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

// -------------------------------------------

// -------------------------------------------
