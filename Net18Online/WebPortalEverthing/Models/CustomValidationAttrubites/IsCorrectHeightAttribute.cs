using System.ComponentModel.DataAnnotations;

namespace WebPortalEverthing.Models.CustomValidationAttrubites
{
    public class IsCorrectHeightAttribute : ValidationAttribute
    {
        private int _min;
        private int _max;
        private HeightOption _option;
        
        public IsCorrectHeightAttribute()
        {
            _min = 100;
            _max = 200;
            _option = HeightOption.Sm;
        }

        public IsCorrectHeightAttribute(int min, int max)
        {
            _min = min;
            _max = max;
            _option = HeightOption.Sm;
        }

        public IsCorrectHeightAttribute(int min, int max, HeightOption option)
        {
            _min = min;
            _max = max;
            _option = option;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Рост должен быть в диапазоне [{_min}, {_max}] {GetHeightOptionDisplayName()}"
                : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value is not int)
            {
                return false;
            }

            var height = (int)value;

            if (height < _min)
            {
                return false;
            }

            if (height > _max)
            {
                return false;
            }

            return true;
        }
    
        private string GetHeightOptionDisplayName()
        {
            switch (_option)
            {
                case HeightOption.Sm:
                    return "См";
                case HeightOption.Inch:
                    return "Дюйм";
            }

            throw new NotImplementedException();
        }
    }

    public enum HeightOption
    {
        Sm,
        Inch
    }
}
