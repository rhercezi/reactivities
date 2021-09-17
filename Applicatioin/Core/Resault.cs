namespace Applicatioin.Core
{
    public class Resault<T>
    {
        public bool IsSucces { get; set; } 
        public T Value { get; set; }
        public string Error { get; set; }

        public static Resault<T> Succes(T value) => new Resault<T> {IsSucces=true, Value=value};
        public static Resault<T> Failure(string error) => new Resault<T> {IsSucces=false, Error=error};
    }
}