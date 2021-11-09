namespace Programmania.Utilities
{
    public class ModalErrorService
    {
        private Microsoft.AspNetCore.Mvc.Localization.IViewLocalizer viewLocalizer;

        public ModalErrorService(Microsoft.AspNetCore.Mvc.Localization.IViewLocalizer viewLocalizer)
        {
            this.viewLocalizer = viewLocalizer;
            string a = viewLocalizer[ErrorType.NicknameValid.ToString()].Value;
        }

        public ErrorType Type { get; set; }

        public string Message { get; set; }

        public enum ErrorType
        {
            #region Valid
            NicknameValid,
            PasswordValid,
            FileValid,
            EmailValid
            #endregion
        }
    }
}
