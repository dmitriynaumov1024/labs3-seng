using System;
using System.Text;

class AgeCalculatorModel
{
    static int CurrentYear = DateTime.Now.Year;
    static int NeededAge = 100;

    public event Action Change;

    private string 
        _userNameString,
        _userAgeString,
        _msgMultiplierString;

    public string UserNameString {
        get {
            return this._userNameString;
        }
        set {
            if (this._userNameString != value) {
                this._userNameString = value;
                this.Change.Invoke();
            }
        }
    }

    public string UserAgeString { 
        get {
            return this._userAgeString;
        }
        set {
            if (this._userAgeString != value) {
                this._userAgeString = value;
                this.Change.Invoke();
            }
        }
    }

    public string MultiplierString {
        get {
            return this._msgMultiplierString;
        }
        set {
            if (this._msgMultiplierString != value) {
                this._msgMultiplierString = value;
                this.Change.Invoke();
            }
        }
    }

    public string OutputMessage {
        get {
            if (this._userAgeString == null || this._userAgeString.Length == 0) {
                return String.Empty;
            }
            int age = 0, multiplier = 1;
            string username;
            if (!int.TryParse(_userAgeString, out age) || age < 0) {
                return "Error. Age should be a positive integer.";
            }
            int.TryParse(_msgMultiplierString, out multiplier);
            if (multiplier < 1 || multiplier > 100) multiplier = 1;
            if (_userNameString == null || _userNameString.Length == 0)
                username = "User";
            else 
                username = _userNameString;
            int resultYear = CurrentYear - age + NeededAge;
            string result = String.Format ("{0}, you will become {1} years old in {2} or {3}\n", 
                username, NeededAge, resultYear - 1, resultYear);
            if (multiplier == 1) {
                return result;
            }
            else {
                StringBuilder sb = new StringBuilder();
                for (int i=0; i<multiplier; i++) {
                    sb.Append(result);
                }
                return sb.ToString();
            }
        }
    }
}
