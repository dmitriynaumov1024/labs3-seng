using System;
using System.Collections.Generic;
using System.Text;

class PasswordGeneratorModel
{
    private Random _random = new Random();

    static int MinLength = 1, 
               MaxLength = 80;

    static string 
        AlphabetChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz",
        Digits = "0123456789",
        Punctuation = "_-+.=,/:;*&?!$";
    

    // Password contains only random latin characters by default.
    // Optionally can contain digits and/or punctuation. 
    public string Password(int length, 
                           bool useDigits = false, 
                           bool usePunctuation = false) 
    {
        length = Clamp(length, MinLength, MaxLength);
        char[] result = new char[length];
        for (int i=0; i<result.Length; i++) {
            // Getting next random number
            int nextRandom = this._random.Next();
            // Selecting a random character pool
            string nextPool = AlphabetChars;
            if ((nextRandom % 9 > 5) && useDigits) nextPool = Digits;
            if ((nextRandom % 9 < 2) && usePunctuation) nextPool = Punctuation;
            // Putting a random character into result array
            result[i] = nextPool[nextRandom % nextPool.Length];
        }
        return new string(result);
    }

    static int Clamp(int number, int min, int max)
    {
        if (number > max) return max;
        if (number < min) return min;
        return number;
    }
}
