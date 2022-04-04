using System;
using System.Text;

class NumberModel
{
    private int? 
        _number1, 
        _number2;

    private bool suppressEvents = false;

    public event Action Change;

    public string Number1 {
        get {
            return this._number1.ToString();
        }
        set {
            int intValue;
            if (int.TryParse(value, out intValue)) {
                this._number1 = intValue;
                if(!this.suppressEvents) this.Change.Invoke();
            }
            else {
                this._number1 = null;
            }
        }
    }

    public string Number2 {
        get {
            return this._number2.ToString();
        }
        set {
            int intValue;
            if (int.TryParse(value, out intValue)) {
                this._number2 = intValue;
                if(!this.suppressEvents) this.Change.Invoke();
            }
            else {
                this._number2 = null;
            }
        }
    }

    public string OutputMessage {
        get {
            if (this._number1 == null || this._number2 == null) {
                return "Please enter 2 integers.";
            }
            int num1 = this._number1.Value;
            int num2 = this._number2.Value;
            int divisor = 4;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Number #1 ({0}) is {1}.\n", 
                num1, (num1 % 2 == 0) ? "even" : "odd");
            sb.AppendFormat("Number #1 ({0}) is {1} by {2}.\n", 
                num1, (num1 % divisor == 0) ? "divisible" : "not divisible", divisor);
            if (num1 == num2) {
                sb.Append("Numbers are equal.\n");
            }
            else if (num2 != 0 && num1 % num2 == 0) {
                sb.AppendFormat("{0} is divisible by {1}.\n", num1, num2);
            }
            else if (num1 != 0 && num2 % num1 == 0) {
                sb.AppendFormat("{0} is divisible by {1}.\n", num2, num1);
            }
            return sb.ToString();
        }
    }

    public NumberModel UseAtomic(Action<NumberModel> action)
    {
        this.suppressEvents = true;
        action(this);
        this.suppressEvents = false;
        this.Change.Invoke();
        return this;
    }
}
