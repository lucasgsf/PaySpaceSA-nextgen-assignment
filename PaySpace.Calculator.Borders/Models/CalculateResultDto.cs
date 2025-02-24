namespace PaySpace.Calculator.Borders.Models
{
    public sealed class CalculateResultDto
    {
        /// <summary>
        /// Used Calculator Type
        /// </summary>
        public string Calculator { get; set; }

        /// <summary>
        /// Calculated Tax
        /// </summary>
        public decimal Tax { get; set; }
    }
}