namespace Lab7.Monads;
// В следующей лабораторной будет представлена улучшенная схема монад. В данной лабе это не использовалось. 
public class Maybe<T>
{
    private readonly T _value;
    public bool HasValue { get; }
    private Maybe(T value) { _value = value; HasValue = true; }
    private Maybe() { HasValue = false; }

	public static Maybe<T> Just(T value) => new Maybe<T>(value);
    public static Maybe<T> Nothing() => new Maybe<T>();
       
    public Maybe<U> Bind<U>(Func<T, Maybe<U>> func)
    {
		if (!HasValue) return Maybe<U>.Nothing();
        return func(_value);
    } 
       
    public static Maybe<T> operator >>(
        Maybe<T> m,
        Func<T, Maybe<T>> f)
    {
        return m.Bind(f);
    }
       
}
   
