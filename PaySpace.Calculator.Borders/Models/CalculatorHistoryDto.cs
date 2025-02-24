namespace PaySpace.Calculator.Borders.Models
{
    public sealed class CalculatorHistoryDto
    {
        /// <summary>
        /// Postal Code
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Calculation Timestamp
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Informed Income
        /// </summary>
        public decimal Income { get; set; }

        /// <summary>
        /// Calculated Tax
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// Calculator Type
        /// </summary>
        public string Calculator { get; set; }
    }
}