# The Simplest AOP Implementation
C#, 最簡易的AOP實作, AOP, Aspect

# 實作說明
.Net Framework 4.x 以上。   
應用 Action 類別的特性。   
好用的 wave 才是好的AOP。   
讓交錯複雜的碼變得更易讀懂才符合AOP的精神。 

# 成果測試
#### 程式碼片段
````Csharp
int runTimes = 0;

WhileAspect(true, () =>
LogAspect(() =>
IgnoreAspect(() =>
RetryAspect(2, 3000, () =>
{
    Console.WriteLine($"times: {++runTimes}");
    Console.WriteLine("step 1");
    Console.WriteLine("step 2");
    if (runTimes < 3)
        throw new ApplicationException("例外測試");
    Console.WriteLine("step 3");
}))));
````
#### 輸出
<pre>
while true → go
BEGIN
times: 1
step 1
step 2
Exception 例外測試 → retry
times: 2
step 1
step 2
Exception 例外測試 → retry
times: 3
step 1
step 2
step 3
END
</pre>

#### 程式碼片段
````Csharp
int runTimes = 0;

WhileAspect(true, () =>
LogAspect(() =>
IgnoreAspect(() =>
CatchAspect<ApplicationException>(ex => {
    Console.WriteLine("I got you. → {0}", ex.Message);
    throw ex; // throw out or not
}, ()=>
RetryAspect(2, 3000, () =>
{
    Console.WriteLine($"times: {++runTimes}");
    Console.WriteLine("step 1");
    Console.WriteLine("step 2");
    //if(runTimes < 3) throw new ApplicationException("例外測試");
    throw new ApplicationException("例外測試");
    Console.WriteLine("step 3");
})))));
````
#### 輸出
<pre>
while true → go
BEGIN
times: 1
step 1
step 2
Exception 例外測試 → retry
times: 2
step 1
step 2
Exception 例外測試 → retry
times: 3
step 1
step 2
Catch ApplicationException
I got you. → 例外測試
Exception 例外測試 → ignore
END
</pre>

#### 程式碼片段
````Csharp
LogAspect(() =>
IgnoreAspect(() =>
CatchAspect<ApplicationException>(handleCatch, ()=>
{
    Console.WriteLine("step 1");
    Console.WriteLine("step 2");
    throw new ApplicationException("例外測試");
    Console.WriteLine("step 3");
})));

void handleCatch(ApplicationException ex) {
    Console.WriteLine("I got you. → {0}", ex.Message);
    throw ex; // throw out or not
}
````
#### 輸出
<pre>
BEGIN
step 1
step 2
Catch ApplicationException
I got you. → 例外測試
Exception 例外測試 → ignore
END
</pre>

