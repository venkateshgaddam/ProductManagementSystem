namespace ProductManagementSystem.Common.Utils
{
    public interface ICcpAttribute<out T>
    {
        T Value { get; }
    }
}