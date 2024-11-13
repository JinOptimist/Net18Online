using System.ComponentModel.DataAnnotations;

namespace WebPortalEverthing.Models.CustomValidationAttrubites
{
    public class CorrectCostAttribute : ValidationAttribute
    {
        private int _min;
        private int _max;
        private CurrencyOption _option;

        public CorrectCostAttribute()
        {
            _min = 10;
            _max = 100;
            _option = CurrencyOption.Byn;
        }

        public CorrectCostAttribute(int min, int max)
        {
            _min = min;
            _max = max;
            _option = CurrencyOption.Byn;
        }

        public CorrectCostAttribute(int min, int max, CurrencyOption option)
        {
            _min = min;
            _max = max;
            _option = option;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Цена должна быть в диапазоне [{_min}, {_max}] {GetCurrency()}"
                : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value is not int)
            {
                return false;
            }

            var cost = (int)value;

            if (cost < _min)
            {
                return false;
            }

            if (cost > _max)
            {
                return false;
            }

            return true;
        }

        private string GetCurrency()
        {
            switch (_option)
            {
                case CurrencyOption.Dollar:
                    return "Доллар";
                case CurrencyOption.Euro:
                    return "Евро";
                case CurrencyOption.Byn:
                    return "Белорусский рубль";
            }

            throw new NotImplementedException();
        }
    }

    public enum CurrencyOption
    {
        Dollar,
        Euro,
        Byn
    }
}


