namespace Library;

public class Singleton<T> where T : class, new()
{
    private static T instance = null;

    private Singleton() { }

    public static T GetInstance()
    {
        if (instance == null)
        {
            instance = new T();
        }
        return instance;
    }
}