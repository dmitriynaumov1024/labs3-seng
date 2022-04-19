using System;

interface ILogger
{
    void Write(string format, params object[] args);
}
