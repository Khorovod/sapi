using System;

namespace SimpleAPI.Models
{
    public interface ITime
    {
        DateTime DateTimeNow();
        string TimeNow();
    }

    public class Time : ITime
    {
        public string TimeNow() => DateTime.Now.ToShortTimeString();
        public DateTime DateTimeNow() => DateTime.Now;
    }
}
